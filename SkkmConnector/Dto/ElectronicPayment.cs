namespace SkkmConnector;

/// <summary>
/// Сведения об одной безналичной оплате (тег 1234 и связанные):
/// <para>
/// Amount - Сумма оплаты безналичными
/// </para>
/// <para>
/// PaymentMethod - Признак способа оплаты. Используйте enum <see cref="ElectronicPaymentMethod"/>
/// </para>
/// <para>
/// Identifiers - Идентификаторы безналичной оплаты
/// </para>
/// <para>
/// AdditionalInformation - Дополнительные сведения
/// </para>
/// </summary>
public sealed class ElectronicPayment
{
    /// <summary>
    /// Сумма оплаты безналичными.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Признак способа оплаты безналичными. Используйте enum <see cref="ElectronicPaymentMethod"/>.
    /// </summary>
    public ElectronicPaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Идентификаторы безналичной оплаты.
    /// </summary>
    public string? Identifiers { get; set; }

    /// <summary>
    /// Дополнительные сведения о безналичной оплате.
    /// </summary>
    public string? AdditionalInformation { get; set; }
}
