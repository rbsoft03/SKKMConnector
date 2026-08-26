using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Счетчики фискальных операций за кассовую смену
    /// </summary>
    public class ShiftCounters
    {
        /// <summary>
        /// Общая сумма коррекций за смену.
        /// </summary>
        [JsonPropertyName("SumCorrection")]
        public decimal SumCorrection { get; set; }

        /// <summary>
        /// Количество коррекций за смену.
        /// </summary>
        [JsonPropertyName("NumberCorrections")]
        public int NumberCorrections { get; set; }

        /// <summary>
        /// Приход: количество и суммы чеков продажи.
        /// </summary>
        [JsonPropertyName("Sales")]
        public DocData? Sales { get; set; }

        /// <summary>
        /// Возврат прихода: количество и суммы.
        /// </summary>
        [JsonPropertyName("SalesReturn")]
        public DocData? SalesReturn { get; set; }

        /// <summary>
        /// Коррекция прихода: количество и суммы.
        /// </summary>
        [JsonPropertyName("SalesCorrection")]
        public DocData? SalesCorrection { get; set; }

        /// <summary>
        /// Коррекция возврата прихода: количество и суммы.
        /// </summary>
        [JsonPropertyName("SalesReturnCorrection")]
        public DocData? SalesReturnCorrection { get; set; }

        /// <summary>
        /// Расход: количество и суммы.
        /// </summary>
        [JsonPropertyName("Purchases")]
        public DocData? Purchases { get; set; }

        /// <summary>
        /// Возврат расхода: количество и суммы.
        /// </summary>
        [JsonPropertyName("PurchasesReturn")]
        public DocData? PurchasesReturn { get; set; }

        /// <summary>
        /// Коррекция расхода: количество и суммы.
        /// </summary>
        [JsonPropertyName("PurchasesCorrection")]
        public DocData? PurchasesCorrection { get; set; }

        /// <summary>
        /// Коррекция возврата расхода: количество и суммы.
        /// </summary>
        [JsonPropertyName("PurchasesReturnCorrection")]
        public DocData? PurchasesReturnCorrection { get; set; }
    }
}
