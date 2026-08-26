namespace SkkmConnector;

/// <summary>
/// Сведения о кассире (продавце)
/// </summary>
public sealed class Cashier
{
    /// <summary>
    /// Имя кассира
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ИНН кассира
    /// </summary>
    public string? Vatin { get; set; }
}
