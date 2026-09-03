namespace SkkmConnector;

/// <summary>
/// Признак способа оплаты безналичными:
/// <para>
/// FullPrepayment - Предоплата 100%
/// </para>
/// <para>
/// PartialPrepayment - Предоплата
/// </para>
/// <para>
/// Advance - Аванс
/// </para>
/// <para>
/// FullPayment - Полный расчёт
/// </para>
/// <para>
/// PartialPaymentAndCredit - Частичный расчёт и кредит
/// </para>
/// <para>
/// CreditTransfer - Передача в кредит
/// </para>
/// <para>
/// CreditPayment - Оплата кредита
/// </para>
/// </summary>
public enum ElectronicPaymentMethod
{
    /// <summary>
    /// Предоплата 100%.
    /// </summary>
    FullPrepayment = 0,

    /// <summary>
    /// Предоплата.
    /// </summary>
    PartialPrepayment = 1,

    /// <summary>
    /// Аванс.
    /// </summary>
    Advance = 2,

    /// <summary>
    /// Полный расчёт.
    /// </summary>
    FullPayment = 3,

    /// <summary>
    /// Частичный расчёт и кредит.
    /// </summary>
    PartialPaymentAndCredit = 4,

    /// <summary>
    /// Передача в кредит.
    /// </summary>
    CreditTransfer = 5,

    /// <summary>
    /// Оплата кредита.
    /// </summary>
    CreditPayment = 6,
}
