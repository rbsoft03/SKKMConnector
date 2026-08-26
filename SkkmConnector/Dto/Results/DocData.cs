using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Счетчик документов
    /// </summary>
    public class DocData
    {
        /// <summary>
        /// Количество документов
        /// </summary>
        [JsonPropertyName("Count")]
        public int Count { get; set; }

        /// <summary>
        /// Сумма по документам
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }

        /// <summary>
        /// Разбивка суммы по видам оплаты 
        /// </summary>
        [JsonPropertyName("Payments")]
        public DocDataPayments? Payments { get; set; }

        /// <summary>
        /// Скидки: количество и сумма.
        /// </summary>
        [JsonPropertyName("Discount")]
        public RegData? Discount { get; set; }

        /// <summary>
        /// Надбавки (наценки): количество и сумма.
        /// </summary>
        [JsonPropertyName("Adding")]
        public RegData? Adding { get; set; }
    }
}
