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

        [JsonPropertyName("Sales")]
        public DocData? Sales { get; set; }

        [JsonPropertyName("SalesReturn")]
        public DocData? SalesReturn { get; set; }

        [JsonPropertyName("SalesCorrection")]
        public DocData? SalesCorrection { get; set; }

        [JsonPropertyName("SalesReturnCorrection")]
        public DocData? SalesReturnCorrection { get; set; }

        [JsonPropertyName("Purchases")]
        public DocData? Purchases { get; set; }

        [JsonPropertyName("PurchasesReturn")]
        public DocData? PurchasesReturn { get; set; }

        [JsonPropertyName("PurchasesCorrection")]
        public DocData? PurchasesCorrection { get; set; }

        [JsonPropertyName("PurchasesReturnCorrection")]
        public DocData? PurchasesReturnCorrection { get; set; }
    }
}
