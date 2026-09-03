using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Элемент списка изображений.
/// </summary>
public class Picture
{
    /// <summary>
    /// Название изображения.
    /// </summary>
    [JsonPropertyName("PictureName")]
    public string? PictureName { get; set; }

    /// <summary>
    /// Выравнивание изображения.
    /// </summary>
    [JsonPropertyName("Alignment")]
    public PictureAlignment Alignment { get; set; }

    /// <summary>
    /// Изображение в Base64 (строка шаблона печати / печатной формы).
    /// </summary>
    [JsonPropertyName("PictureBase64")]
    public string? PictureBase64 { get; set; }

    /// <summary>
    /// Ширина изображения при печати, в точках.
    /// </summary>
    [JsonPropertyName("Width")]
    public int? Width { get; set; }

    /// <summary>
    /// Высота изображения при печати, в точках.
    /// </summary>
    [JsonPropertyName("Height")]
    public int? Height { get; set; }
}
