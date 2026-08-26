using SkkmConnector;

namespace SkkmConnector.Internal;

/// <summary>
/// Разбор текста с разметкой в строки нефискального документа.
/// Префиксы: [big], [center], [QR], [line], [line,dotted], [dotted].
/// </summary>
internal static class SlipTextParser
{
    public static DocPosition[] Parse(string text)
    {
        var positions = new List<DocPosition>();

        foreach (var rawLine in text.Replace("\r\n", "\n").Split('\n'))
            positions.Add(ParseLine(rawLine));

        return positions.ToArray();
    }

    /// <summary>
    /// Одна строка: префиксы в тексте превращаются в SeparatorLine / Barcode / TextString.
    /// </summary>
    public static DocPosition ParseLine(string line, string? font = null, string? alignment = null)
    {
        PrintAlignment? parsedAlignment = ParseEnum<PrintAlignment>(alignment);
        PrintFont? parsedFont = ParseEnum<PrintFont>(font);
        BarcodeType? barcodeType = null;
        LineStyle? lineStyle = null;
        var hasLineTag = false;

        if (line.StartsWith("[") && line.Contains(']'))
        {
            int close = line.IndexOf(']');
            var tags = line[1..close]
                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToArray();

            var recognized = false;

            foreach (var tag in tags)
            {
                if (string.Equals(tag, "line", StringComparison.OrdinalIgnoreCase))
                {
                    hasLineTag = true;
                    recognized = true;
                }
            }

            foreach (var tag in tags)
            {
                if (tag.Length == 0 || char.IsDigit(tag[0]))
                    continue;

                if (TryLineStyle(tag, hasLineTag, out var parsedLineStyle))
                    lineStyle = parsedLineStyle;
                else if (Enum.TryParse<BarcodeType>(tag, ignoreCase: true, out var parsedBarcode))
                    barcodeType = parsedBarcode;
                else if (Enum.TryParse<PrintAlignment>(tag, ignoreCase: true, out var parsedAlign))
                    parsedAlignment = parsedAlign;
                else if (Enum.TryParse<PrintFont>(tag, ignoreCase: true, out var parsedPrintFont))
                    parsedFont = parsedPrintFont;
                else
                    continue;

                recognized = true;
            }

            // Префикс в квадратных скобках срезаем только если внутри распознан тег
            // (center, dotted, QR, line…). Обычный текст вида "[Промо]" остаётся как есть.
            if (recognized)
                line = line[(close + 1)..];
        }

        if (hasLineTag || (lineStyle != null && line.Length == 0))
        {
            return new DocPosition
            {
                SeparatorLine = new SeparatorLine { Style = lineStyle ?? LineStyle.Solid }
            };
        }

        if (barcodeType != null)
        {
            return new DocPosition
            {
                Barcode = new BarcodeLine
                {
                    Type = barcodeType.ToString() ?? "",
                    Barcode = line.Trim(),
                    Alignment = parsedAlignment?.ToString().ToLowerInvariant()
                }
            };
        }

        return new DocPosition
        {
            TextString = new TextString
            {
                Text = line,
                Font = parsedFont?.ToString(),
                Alignment = parsedAlignment?.ToString().ToLowerInvariant()
            }
        };
    }

    /// <summary>
    /// [dotted] / [dashed] / [solid] / [double] - линия.
    /// [bold] - шрифт, линия только вместе с [line].
    /// </summary>
    private static bool TryLineStyle(string tag, bool hasLineTag, out LineStyle style)
    {
        if (!Enum.TryParse(tag, ignoreCase: true, out style))
            return false;

        if (style == LineStyle.Bold && !hasLineTag)
            return false;

        return true;
    }

    private static TEnum? ParseEnum<TEnum>(string? value) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return Enum.TryParse<TEnum>(value, ignoreCase: true, out var parsed) ? parsed : null;
    }
}
