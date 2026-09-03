namespace SkkmConnector;

/// <summary>
/// Элемент шаблона печати. Создайте объект и задайте <see cref="PrintLine"/>.
/// </summary>
public sealed class TemplateItem
{
    /// <summary>
    /// Строка печати: текст, штрихкод, изображение или разделительная линия.
    /// </summary>
    public PrintLine? PrintLine { get; set; }
}
