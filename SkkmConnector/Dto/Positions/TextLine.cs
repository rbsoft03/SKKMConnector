namespace SkkmConnector;

/// <summary>
/// Текстовая строка
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
