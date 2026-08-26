namespace SkkmConnector;

/// <summary>
/// Сведения об оплате безналичными
/// </summary>
public sealed class ElectronicPayment
{
    /// <summary>
    /// Сумма оплаты безналичными
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Признак способа оплаты безналичными
    /// </summary>
    public int? PaymentMethod { get; set; }

    /// <summary>
    /// Идентификаторы безналичной оплаты
    /// </summary>
    public string? Identifiers { get; set; }

    /// <summary>
    /// Дополнительные сведения о безналичной оплате
    /// </summary>
    public string? AdditionalInformation { get; set; }
}
