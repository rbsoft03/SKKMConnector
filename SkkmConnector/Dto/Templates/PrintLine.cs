using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Строка печатного шаблона:
/// <para>
/// Type - Тип строки. Используйте enum <see cref="PrintLineType"/>
/// </para>
/// <para>
/// Line / LineRight - Текст (левая / правая часть)
/// </para>
/// <para>
/// Alignment - Выравнивание. Используйте enum <see cref="PrintAlignment"/>
/// </para>
/// <para>
/// Font - Шрифт. Используйте enum <see cref="PrintFont"/>
/// </para>
/// <para>
/// Width / Scale - Ширина и масштаб
/// </para>
/// <para>
/// Barcode / Picture - Штрихкод или картинка (по типу строки)
/// </para>
/// </summary>
public sealed class PrintLine
{
    /// <summary>
    /// Тип строки. Используйте enum <see cref="PrintLineType"/>. Если не указано — Text.
    /// </summary>
    public PrintLineType Type { get; set; } = PrintLineType.Text;

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
    /// Выравнивание. Используйте enum <see cref="PrintAlignment"/>. Если не указано — Left.
    /// </summary>
    public PrintAlignment Alignment { get; set; }

    /// <summary>
    /// Шрифт. Используйте enum <see cref="PrintFont"/>. Если не указано — Normal.
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
}
