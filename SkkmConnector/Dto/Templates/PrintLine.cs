namespace SkkmConnector;

/// <summary>
/// Строка печатного шаблона.
/// </summary>
public sealed class PrintLine
{
    /// <summary>
    /// Тип строки: 0 — фискальная; 1 — текстовая; 2 — штрихкод; 3 — изображение; 4 — разделительная линия.
    /// Если не указано — 1 (текстовая).
    /// </summary>
    public int Type { get; set; } = 1;

    /// <summary>
    /// Ширина. Если не указано — 0 (по содержимому).
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Масштаб. Если не указано — 100%.
    /// </summary>
    public int Scale { get; set; }

    /// <summary>
    /// Текст строки (левая часть).
    /// </summary>
    public string? Line { get; set; }

    /// <summary>
    /// Текст строки (правая часть).
    /// </summary>
    public string? LineRight { get; set; }

    /// <summary>
    /// Выравнивание: 0 — по левому краю; 1 — по центру; 2 — по правому краю; 3 — по ширине.
    /// Если не указано — слева.
    /// </summary>
    public int Alignment { get; set; }

    /// <summary>
    /// Шрифт: 0 — обычный; 1 — жирный; 2 — мелкий; 3 — средний; 4 — крупный; 5–9 — H1–H5.
    /// Если не указано — 0.
    /// </summary>
    public int Font { get; set; }

    /// <summary>
    /// Перенос строк: false — строка обрезается; true — переносится. Если не указано — true.
    /// </summary>
    public bool Wrap { get; set; } = true;

    /// <summary>
    /// Штрихкод
    /// </summary>
    public PrintFormBarcode? Barcode { get; set; }

    /// <summary>
    /// Разделительная линия
    /// </summary>
    public SeparatorLine? SeparatorLine { get; set; }

    /// <summary>
    /// Изображение
    /// </summary>
    public Picture? Picture { get; set; }
}
