using System.Text.Json.Serialization;

namespace RBSoftSkkm
{
    /// <summary>
    /// Необнуляемые итоги ККТ за всё время работы (X- и Z-отчёт).
    /// </summary>
    public class OverallTotals
    {
        /// <summary>
        /// Итоги загружены из ФН.
        /// </summary>
        [JsonPropertyName("DataLoaded")]
        public bool DataLoaded { get; set; }

        /// <summary>
        /// Общая сумма.
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }

        /// <summary>
        /// Общее количество документов.
        /// </summary>
        [JsonPropertyName("Count")]
        public int Count { get; set; }

        /// <summary>
        /// Счётчики фискальных операций за всё время работы.
        /// </summary>
        [JsonPropertyName("Counters")]
        public ShiftCounters? Counters { get; set; }

        /// <summary>
        /// Наличность в денежном ящике (необнуляемые итоги).
        /// </summary>
        [JsonPropertyName("CashDrawer")]
        public CashDrawer? CashDrawer { get; set; }
    }
}
