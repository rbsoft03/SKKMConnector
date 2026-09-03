namespace SkkmConnector;

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
    /// Текст строки
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// Шрифт
    /// </summary>
    public string? Font { get; set; }

    /// <summary>
    /// Выравнивание
    /// </summary>
    public string? Alignment { get; set; }
}
