namespace SkkmConnector;

/// <summary>
/// Изображение
/// </summary>
public sealed class PictureLine : Position
{
    /// <summary>
    /// Изображение в Base64
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// Выравнивание изображения
    /// </summary>
    public int Alignment { get; set; } = 2;

    /// <summary>
    /// Ширина изображения
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Высота изображения
    /// </summary>
    public int? Height { get; set; }
}
