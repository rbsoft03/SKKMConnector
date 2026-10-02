using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Строка печатного шаблона:
/// <para>
/// Type - Тип строки.
/// </para>
/// <para>
/// Line / LineRight - Текст (левая / правая часть)
/// </para>
/// <para>
/// Alignment - Выравнивание.
/// </para>
/// <para>
/// Font - Шрифт.
/// </para>
/// <para>
/// Width / Scale - Ширина и масштаб
/// </para>
/// <para>
/// Barcode / Picture - Штрихкод или картинка (по типу строки)
/// </para>
/// </summary>
public sealed class PrintTemplateLine : IPrintLine
{
    /// <summary>
    /// Тип строки. Если не указано — Text.
    /// </summary>
    public PrintLineType Type { get; set; } = PrintLineType.Text;

    /// <summary>
    /// Ширина. Если не указано — 0 (по содержимому).
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Масштаб в процентах. По умолчанию — 100%.
    /// </summary>
    public int Scale { get; set; } = 100;

    /// <summary>
    /// Текст строки (левая часть).
    /// </summary>
    public string? Line { get; set; }

    /// <summary>
    /// Текст строки (правая часть).
    /// </summary>
    public string? LineRight { get; set; }

    /// <summary>
    /// Выравнивание. Если не указано — Left.
    /// </summary>
    public PrintAlignment Alignment { get; set; }

    /// <summary>
    /// Шрифт. Если не указано — Normal.
    /// </summary>
    public PrintFont Font { get; set; }

    /// <summary>
    /// Перенос строк: false — строка обрезается; true — переносится. Если не указано — true.
    /// </summary>
    public bool Wrap { get; set; } = true;

    /// <summary>
    /// Штрихкод.
    /// </summary>
    public PrintFormBarcode? Barcode { get; set; }

    /// <summary>
    /// Разделительная линия.
    /// </summary>
    public SeparatorLine? SeparatorLine { get; set; }

    /// <summary>
    /// Изображение.
    /// </summary>
    public Picture? Picture { get; set; }

    /// <summary>
    /// Строка создана из печатного шаблона.
    /// </summary>
    public bool IsCreateFromTemplate { get; set; }

    /// <summary>
    /// Шрифт задан явно во входящих данных или при создании строки.
    /// </summary>
    public bool IsFontSpecified { get; set; }

    /// <summary>
    /// Строки слева или справа от штрихкода (печатная форма).
    /// </summary>
    public string[]? BarcodeLines { get; set; }
}
