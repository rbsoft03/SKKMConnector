using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Изображение в чеке:
/// <para>
/// Value - Картинка в Base64
/// </para>
/// <para>
/// Alignment - Выравнивание. Используйте enum <see cref="PictureAlignment"/>
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
    /// Выравнивание изображения. Используйте enum <see cref="PictureAlignment"/>.
    /// </summary>
    [JsonIgnore]
    public PictureAlignment Alignment { get; set; } = PictureAlignment.Center;

    /// <summary>
    /// Выравнивание в нумерации позиций чека: 0 — слева, 1 — по центру, 2 — справа.
    /// </summary>
    [JsonPropertyName("Alignment")]
    public int AlignmentValue => (int)Alignment - 1;

    /// <summary>
    /// Ширина изображения.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Высота изображения.
    /// </summary>
    public int? Height { get; set; }
}
