namespace SkkmConnector;

/// <summary>
/// Данные агента в чеке
/// </summary>
public sealed class Agent
{
    /// <summary>
    /// Операция платежного агента
    /// </summary>
    public string? PayingAgentOperation { get; set; }

    /// <summary>
    /// Телефон платежного агента
    /// </summary>
    public string[]? PayingAgentPhone { get; set; }

    /// <summary>
    /// Телефон оператора по приему платежей
    /// </summary>
    public string[]? ReceivePaymentsOperatorPhone { get; set; }

    /// <summary>
    /// Телефон оператора перевода
    /// </summary>
    public string[]? MoneyTransferOperatorPhone { get; set; }

    /// <summary>
    /// Наименование оператора перевода
    /// </summary>
    public string? MoneyTransferOperatorName { get; set; }

    /// <summary>
    /// Адрес оператора перевода
    /// </summary>
    public string? MoneyTransferOperatorAddress { get; set; }

    /// <summary>
    /// ИНН оператора перевода
    /// </summary>
    public string? MoneyTransferOperatorVatin { get; set; }
}
