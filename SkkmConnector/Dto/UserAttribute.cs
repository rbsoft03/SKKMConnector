namespace SkkmConnector;

/// <summary>
/// Дополнительный реквизит пользователя чека:
/// <para>
/// Name - Имя реквизита
/// </para>
/// <para>
/// Value - Значение реквизита
/// </para>
/// </summary>
public sealed class UserAttribute
{
    /// <summary>
    /// Имя реквизита.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Значение реквизита.
    /// </summary>
    public string? Value { get; set; }
}
