using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Элемент шаблона печати. Создайте объект и задайте <see cref="PrintTemplateLine"/>.
/// </summary>
public sealed class TemplateItem
{
    /// <summary>
    /// Строка печати: текст, штрихкод, изображение или разделительная линия.
    /// Wire-имя поля — "PrintLine" (как у сервера), C#-имя — PrintTemplateLine.
    /// </summary>
    [JsonPropertyName("PrintLine")]
    public PrintTemplateLine? PrintTemplateLine { get; set; }
}
