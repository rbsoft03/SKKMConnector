namespace SkkmConnector;

/// <summary>
/// Признак агента (тег 1222 ФФД):
/// <para>
/// BankPaymentAgent - Банковский платежный агент
/// </para>
/// <para>
/// BankPaymentSubagent - Банковский платежный субагент
/// </para>
/// <para>
/// PaymentAgent - Платежный агент
/// </para>
/// <para>
/// PaymentSubagent - Платёжный субагент
/// </para>
/// <para>
/// Attorney - Поверенный
/// </para>
/// <para>
/// Commissioner - Комиссионер
/// </para>
/// <para>
/// Agent - Агент (иной тип).
/// </para>
/// </summary>
public enum AgentType
{
    /// <summary>
    /// Банковский платёжный агент.
    /// </summary>
    BankPaymentAgent = 0,

    /// <summary>
    /// Банковский платёжный субагент.
    /// </summary>
    BankPaymentSubagent = 1,

    /// <summary>
    /// Платёжный агент.
    /// </summary>
    PaymentAgent = 2,

    /// <summary>
    /// Платёжный субагент.
    /// </summary>
    PaymentSubagent = 3,

    /// <summary>
    /// Поверенный.
    /// </summary>
    Attorney = 4,

    /// <summary>
    /// Комиссионер.
    /// </summary>
    Commissioner = 5,

    /// <summary>
    /// Агент (иной тип).
    /// </summary>
    Agent = 6,
}
