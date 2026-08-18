using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Виды оплаты чека.
    /// </summary>
    internal class Payments
    {
        /// <summary>
        /// Сумма наличной оплаты
        /// </summary>
        [JsonPropertyName("Cash")]
        public decimal Cash { get; set; }

        /// <summary>
        /// Сумма безналичными средствами
        /// </summary>
        [JsonPropertyName("ElectronicPayment")]
        public decimal ElectronicPayment { get; set; }

        /// <summary>
        /// Сумма предоплатой (зачетом аванса)
        /// </summary>
        [JsonPropertyName("AdvancePayment")]
        public decimal AdvancePayment { get; set; }

        /// <summary>
        /// Сумма постоплатой (в кредит)
        /// </summary>
        [JsonPropertyName("Credit")]
        public decimal Credit { get; set; }

        /// <summary>
        /// Сумма встречным предоставлением
        /// </summary>
        [JsonPropertyName("CashProvision")]
        public decimal CashProvision { get; set; }
    }
}
