using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Разбивка суммы операций по видам оплаты (наличные, безналичные, аванс, кредит, встречное предоставление).
    /// </summary>
    public class DocDataPayments
    {
        /// <summary>
        /// Общая сумма оплат.
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }

        /// <summary>
        /// Оплачено наличными.
        /// </summary>
        [JsonPropertyName("Cash")]
        public decimal Cash { get; set; }

        /// <summary>
        /// Оплачено безналичными.
        /// </summary>
        [JsonPropertyName("Electronically")]
        public decimal Electronically { get; set; }

        /// <summary>
        /// Оплачено авансом (предоплата).
        /// </summary>
        [JsonPropertyName("Prepaid")]
        public decimal Prepaid { get; set; }

        /// <summary>
        /// Оплачено в кредит (постоплата).
        /// </summary>
        [JsonPropertyName("Credit")]
        public decimal Credit { get; set; }

        /// <summary>
        /// Оплачено встречным предоставлением (бартер).
        /// </summary>
        [JsonPropertyName("Barter")]
        public decimal Barter { get; set; }
    }
}
