namespace RBSoftSkkm;

/// <summary>
/// Текстовая строка чека:
/// <para>
/// Text - Текст
/// </para>
/// <para>
/// Font - Шрифт
/// </para>
/// <para>
/// Alignment - Выравнивание
/// </para>
/// </summary>
public sealed class TextLine : Position
{
    /// <summary>
    /// Текст строки (левая часть).
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// Текст строки (правая часть)
    /// </summary>
    public string? LineRight { get; set; }

    /// <summary>
    /// Шрифт. Если не задан — Normal.
    /// </summary>
    public PrintFont? Font { get; set; }

    /// <summary>
    /// Выравнивание. Если не задано — Left.
    /// </summary>
    public PrintAlignment? Alignment { get; set; }

    /// <summary>
    /// Перенос строк. false — строка обрезается; true — переносится.
    /// Для двухколоночной строки (LineRight) по умолчанию выключается.
    /// </summary>
    public bool Wrap { get; set; } = true;
}
