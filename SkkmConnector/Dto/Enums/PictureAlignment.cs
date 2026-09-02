namespace SkkmConnector;

/// <summary>
/// Выравнивание изображения при печати или загрузке в ККТ:
/// <para>
/// Left - По левому краю
/// </para>
/// <para>
/// Center - По центру
/// </para>
/// <para>
/// Right - По правому краю
/// </para>
/// </summary>
public enum PictureAlignment
{
    /// <summary>
    /// По левому краю.
    /// </summary>
    Left = 1,

    /// <summary>
    /// По центру.
    /// </summary>
    Center = 2,

    /// <summary>
    /// По правому краю.
    /// </summary>
    Right = 3,
}
