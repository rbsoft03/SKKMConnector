using System.Text.Json.Serialization;

namespace RBSoftSkkm;

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

    /// <summary>
    /// Номер первой строки печати изображения.
    /// </summary>
    [JsonPropertyName("StartLineNumber")]
    public int? StartLineNumber { get; set; }

    /// <summary>
    /// Номер последней строки печати изображения.
    /// </summary>
    [JsonPropertyName("EndLineNumber")]
    public int? EndLineNumber { get; set; }

    /// <summary>
    /// Изображение загружено в память ККТ.
    /// </summary>
    [JsonPropertyName("IsUploaded")]
    public bool? IsUploaded { get; set; }

    /// <summary>
    /// Изображение перезаписывается в памяти ККТ.
    /// </summary>
    [JsonPropertyName("Override")]
    public bool? Override { get; set; }
}
