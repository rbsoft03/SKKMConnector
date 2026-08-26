using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Оплаты из ответа сервера (Payments)
/// </summary>
public sealed class CheckPayments
{
    /// <summary>
    /// Сумма наличной оплаты.
    /// </summary>
    [JsonPropertyName("Cash")]
    public decimal Cash { get; set; }

    /// <summary>
    /// Сумма безналичными средствами.
    /// </summary>
    [JsonPropertyName("Electronic")]
    public decimal Electronic { get; set; }

    /// <summary>
    /// Сумма предоплатой (зачётом аванса).
    /// </summary>
    [JsonPropertyName("PrePaid")]
    public decimal PrePaid { get; set; }

    /// <summary>
    /// Сумма постоплатой (в кредит).
    /// </summary>
    [JsonPropertyName("Credit")]
    public decimal Credit { get; set; }

    /// <summary>
    /// Сумма встречным предоставлением.
    /// </summary>
    [JsonPropertyName("Barter")]
    public decimal Barter { get; set; }
}
