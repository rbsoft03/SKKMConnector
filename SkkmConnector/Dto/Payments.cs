namespace SkkmConnector;

/// <summary>
/// Суммы оплаты по способам расчёта:
/// <para>
/// Cash - Наличными
/// </para>
/// <para>
/// ElectronicPayment - Безналичными
/// </para>
/// <para>
/// AdvancePayment - Предоплатой (зачётом аванса)
/// </para>
/// <para>
/// Credit - Постоплатой (в кредит)
/// </para>
/// <para>
/// CashProvision - Встречным предоставлением
/// </para>
/// Заполните одну или несколько сумм; итог должен соответствовать сумме позиций чека.
/// </summary>
public sealed class Payments
{
    /// <summary>
    /// Сумма наличной оплаты.
    /// </summary>
    public decimal Cash { get; set; }

    /// <summary>
    /// Сумма безналичными средствами.
    /// </summary>
    public decimal ElectronicPayment { get; set; }

    /// <summary>
    /// Сумма предоплатой (зачётом аванса).
    /// </summary>
    public decimal AdvancePayment { get; set; }

    /// <summary>
    /// Сумма постоплатой (в кредит).
    /// </summary>
    public decimal Credit { get; set; }

    /// <summary>
    /// Сумма встречным предоставлением.
    /// </summary>
    public decimal CashProvision { get; set; }
}
