namespace SkkmConnector;

/// <summary>
/// Сведения о кассире (продавце):
/// <para>
/// Name - ФИО кассира
/// </para>
/// <para>
/// Vatin - ИНН кассира (при наличии)
/// </para>
/// </summary>
public sealed class Cashier
{
    /// <summary>
    /// Имя кассира.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ИНН кассира.
    /// </summary>
    public string? Vatin { get; set; }
}
