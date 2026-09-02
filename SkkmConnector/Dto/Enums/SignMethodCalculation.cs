namespace SkkmConnector;

/// <summary>
/// Признак способа расчёта (тег 1214 ФФД):
/// <para>
/// NotApplicable - Не применяется
/// </para>
/// <para>
/// FullPrepayment - Предоплата полная
/// </para>
/// <para>
/// PartialPrepayment - Предоплата частичная
/// </para>
/// <para>
/// Advance - Аванс
/// </para>
/// <para>
/// FullPayment - Полная оплата
/// </para>
/// <para>
/// PartialPaymentAndCredit - Частичная оплата и кредит
/// </para>
/// <para>
/// CreditTransfer - Передача в кредит
/// </para>
/// <para>
/// CreditPayment - Оплата кредита
/// </para>
/// </summary>
public enum SignMethodCalculation
{
    /// <summary>
    /// Не применяется.
    /// </summary>
    NotApplicable = 0,

    /// <summary>
    /// Предоплата полная.
    /// </summary>
    FullPrepayment = 1,

    /// <summary>
    /// Предоплата частичная.
    /// </summary>
    PartialPrepayment = 2,

    /// <summary>
    /// Аванс.
    /// </summary>
    Advance = 3,

    /// <summary>
    /// Полная оплата.
    /// </summary>
    FullPayment = 4,

    /// <summary>
    /// Частичная оплата и кредит.
    /// </summary>
    PartialPaymentAndCredit = 5,

    /// <summary>
    /// Передача в кредит.
    /// </summary>
    CreditTransfer = 6,

    /// <summary>
    /// Оплата кредита.
    /// </summary>
    CreditPayment = 7,
}
