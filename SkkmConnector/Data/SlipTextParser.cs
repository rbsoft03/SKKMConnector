using SkkmConnector;

namespace SkkmConnector.Internal;

/// <summary>
/// Разбор текста с разметкой в строки нефискального документа.
/// </summary>
internal static class SlipTextParser
{
    public static DocPosition[] Parse(string text)
    {
        var positions = new List<DocPosition>();

        foreach (var rawLine in text.Replace("\r\n", "\n").Split('\n'))
        {
            string line = rawLine;
            PrintAlignment? alignment = null;
            PrintFont? font = null;
            BarcodeType? barcodeType = null;
            bool isSeparatorLine = false;
            LineStyle lineStyle = LineStyle.Solid;

            if (line.StartsWith("[") && line.Contains("]"))
            {
                int close = line.IndexOf(']');
                var tags = line.Substring(1, close - 1)
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim())
                    .ToArray();
                line = line.Substring(close + 1);

                foreach (var tag in tags)
                    if (string.Equals(tag, "line", StringComparison.OrdinalIgnoreCase))
                        isSeparatorLine = true;

                foreach (var tag in tags)
                {
                    if (tag.Length == 0 || char.IsDigit(tag[0]))
                        continue;

                    if (isSeparatorLine && Enum.TryParse<LineStyle>(tag, ignoreCase: true, out var parsedLineStyle))
                        lineStyle = parsedLineStyle;
                    else if (Enum.TryParse<BarcodeType>(tag, ignoreCase: true, out var parsedBarcode))
                        barcodeType = parsedBarcode;
                    else if (Enum.TryParse<PrintAlignment>(tag, ignoreCase: true, out var parsedAlignment))
                        alignment = parsedAlignment;
                    else if (Enum.TryParse<PrintFont>(tag, ignoreCase: true, out var parsedFont))
                        font = parsedFont;
                }
            }

            if (isSeparatorLine)
            {
                positions.Add(new DocPosition
                {
                    SeparatorLine = new SeparatorLine { LineStyle = lineStyle }
                });
            }
            else if (barcodeType != null)
            {
                positions.Add(new DocPosition
                {
                    Barcode = new Barcode
                    {
                        Type = barcodeType.ToString(),
                        Value = line.Trim(),
                        Alignment = alignment?.ToString().ToLowerInvariant()
                    }
                });
            }
            else
            {
                positions.Add(new DocPosition
                {
                    TextString = new TextString
                    {
                        Text = line,
                        Font = font?.ToString(),
                        Alignment = alignment?.ToString().ToLowerInvariant()
                    }
                });
            }
        }

        return positions.ToArray();
    }
}
