using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Итоги текущей кассовой смены
    /// </summary>
    public class ResShiftTotal
    {
        /// <summary>
        /// Номер смены.
        /// </summary>
        [JsonPropertyName("ShiftNumber")]
        public double ShiftNumber { get; set; }

        /// <summary>
        /// Денежный ящик: остаток наличных и число операций.
        /// </summary>
        [JsonPropertyName("CashDrawer")]
        public CashDrawer? CashDrawer { get; set; }

        /// <summary>
        /// Внесения за смену.
        /// </summary>
        [JsonPropertyName("ShiftIncome")]
        public ShiftIncome? ShiftIncome { get; set; }

        /// <summary>
        /// Выемки за смену.
        /// </summary>
        [JsonPropertyName("ShiftOutcome")]
        public ShiftIncome? ShiftOutcome { get; set; }

        /// <summary>
        /// Счетчики фискальных операций за смену
        /// </summary>
        [JsonPropertyName("Counters")]
        public ShiftCounters? Counters { get; set; }
    }
}
