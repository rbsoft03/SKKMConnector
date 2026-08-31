using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Строка печатной формы 
/// </summary>
public sealed class PrintFormLine
{
    /// <summary>
    /// Тип строки: 0 — фискальная; 1 — текстовая; 2 — штрихкод; 3 — изображение; 4 — разделительная линия.
    /// Если не указано — 1 (текстовая).
    /// </summary>
    [JsonPropertyName("Type")]
    public int Type { get; set; }

    /// <summary>
    /// Текст строки (левая часть).
    /// </summary>
    [JsonPropertyName("Line")]
    public string? Line { get; set; }

    /// <summary>
    /// Текст строки (правая часть).
    /// </summary>
    [JsonPropertyName("LineRight")]
    public string? LineRight { get; set; }

    /// <summary>
    /// Выравнивание: 0 — по левому краю; 1 — по центру; 2 — по правому краю; 3 — по ширине. Если не указано — слева.
    /// </summary>
    [JsonPropertyName("Alignment")]
    public int Alignment { get; set; }

    /// <summary>
    /// Шрифт: 0 — обычный; 1 — жирный; 2 — мелкий; 3 — средний; 4 — крупный; 5–9 — H1–H5. Если не указано — 0.
    /// </summary>
    [JsonPropertyName("Font")]
    public int Font { get; set; }

    /// <summary>
    /// Признак, что шрифт задан явно во входящих данных или при создании строки.
    /// </summary>
    [JsonPropertyName("IsFontSpecified")]
    public bool IsFontSpecified { get; set; }

    /// <summary>
    /// Ширина. Если не указано — 0 (по содержимому).
    /// </summary>
    [JsonPropertyName("Width")]
    public int Width { get; set; }

    /// <summary>
    /// Масштаб. Если не указано — 100%.
    /// </summary>
    [JsonPropertyName("Scale")]
    public int Scale { get; set; }

    /// <summary>
    /// Признак переноса строк: false — строка обрезается; true — переносится. Если не указано — true.
    /// </summary>
    [JsonPropertyName("Wrap")]
    public bool Wrap { get; set; }

    /// <summary>
    /// Разделительная линия.
    /// </summary>
    [JsonPropertyName("SeparatorLine")]
    public SeparatorLine? SeparatorLine { get; set; }

    /// <summary>
    /// Изображение.
    /// </summary>
    [JsonPropertyName("Picture")]
    public Picture? Picture { get; set; }

    /// <summary>
    /// Штрихкод.
    /// </summary>
    [JsonPropertyName("Barcode")]
    public PrintFormBarcode? Barcode { get; set; }

    /// <summary>
    /// Строки, выводимые справа или слева от штрихкода.
    /// </summary>
    [JsonPropertyName("BarcodeLines")]
    public string[]? BarcodeLines { get; set; }

    /// <summary>
    /// Признак создания строки из печатного шаблона.
    /// </summary>
    [JsonPropertyName("IsCreateFromTemplate")]
    public bool IsCreateFromTemplate { get; set; }
}
