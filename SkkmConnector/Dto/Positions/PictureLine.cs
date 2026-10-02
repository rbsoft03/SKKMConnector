using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Изображение в чеке:
/// <para>
/// Value - Картинка в Base64
/// </para>
/// <para>
/// Alignment - Выравнивание.
/// </para>
/// <para>
/// Width / Height - Размер (при необходимости)
/// </para>
/// </summary>
public sealed class PictureLine : Position
{
    /// <summary>
    /// Изображение в Base64.
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// Выравнивание изображения.
    /// </summary>
    [JsonIgnore]
    public PictureAlignment Alignment { get; set; } = PictureAlignment.Center;

    /// <summary>
    /// Выравнивание изображения в формате Сервера ККМ: 1 — слева, 2 — по центру, 3 — справа.
    /// </summary>
    [JsonPropertyName("Alignment")]
    public int AlignmentValue => (int)Alignment;

    /// <summary>
    /// Ширина изображения.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Высота изображения.
    /// </summary>
    public int? Height { get; set; }
}
