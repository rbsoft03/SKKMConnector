namespace SkkmConnector;

/// <summary>
/// Оплаты
/// </summary>
public sealed class Payments
{
    /// <summary>
    /// Сумма наличной оплаты
    /// </summary>
    public decimal Cash { get; set; }

    /// <summary>
    /// Сумма безналичными средствами
    /// </summary>
    public decimal ElectronicPayment { get; set; }

    /// <summary>
    /// Сумма предоплатой (зачетом аванса)
    /// </summary>
    public decimal AdvancePayment { get; set; }

    /// <summary>
    /// Сумма постоплатой (в кредит)
    /// </summary>
    public decimal Credit { get; set; }

    /// <summary>
    /// Сумма встречным предоставлением
    /// </summary>
    public decimal CashProvision { get; set; }
}
