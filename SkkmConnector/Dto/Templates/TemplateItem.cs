namespace SkkmConnector;

/// <summary>
/// Элемент шаблона печати.
/// </summary>
public sealed class TemplateItem
{
    /// <summary>
    /// Строка печати: текст, штрихкод, изображение или разделительная линия.
    /// </summary>
    public PrintLine? PrintLine { get; set; }
}
