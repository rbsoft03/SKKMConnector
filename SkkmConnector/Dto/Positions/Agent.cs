namespace SkkmConnector;

/// <summary>
/// Данные агента в чеке:
/// <para>
/// PayingAgentOperation - Операция платёжного агента
/// </para>
/// <para>
/// PayingAgentPhone - Телефон(ы) платёжного агента
/// </para>
/// <para>
/// ReceivePaymentsOperatorPhone - Телефон(ы) оператора по приёму платежей
/// </para>
/// <para>
/// MoneyTransferOperatorPhone - Телефон(ы) оператора перевода
/// </para>
/// <para>
/// MoneyTransferOperatorName - Наименование оператора перевода
/// </para>
/// <para>
/// MoneyTransferOperatorAddress - Адрес оператора перевода
/// </para>
/// <para>
/// MoneyTransferOperatorVatin - ИНН оператора перевода
/// </para>
/// </summary>
public sealed class Agent
{
    /// <summary>
    /// Операция платёжного агента.
    /// </summary>
    public string? PayingAgentOperation { get; set; }

    /// <summary>
    /// Телефон платёжного агента.
    /// </summary>
    public string[]? PayingAgentPhone { get; set; }

    /// <summary>
    /// Телефон оператора по приёму платежей.
    /// </summary>
    public string[]? ReceivePaymentsOperatorPhone { get; set; }

    /// <summary>
    /// Телефон оператора перевода.
    /// </summary>
    public string[]? MoneyTransferOperatorPhone { get; set; }

    /// <summary>
    /// Наименование оператора перевода.
    /// </summary>
    public string? MoneyTransferOperatorName { get; set; }

    /// <summary>
    /// Адрес оператора перевода.
    /// </summary>
    public string? MoneyTransferOperatorAddress { get; set; }

    /// <summary>
    /// ИНН оператора перевода.
    /// </summary>
    public string? MoneyTransferOperatorVatin { get; set; }
}
