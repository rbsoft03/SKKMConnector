using System.Text.Json.Serialization;

namespace RBSoftSkkm.Internal
{
    /// <summary>
    /// Необнуляемые счётчики ККТ.
    /// </summary>
    internal class OverallTotals
    {
        /// <summary>
        /// Счётчики фискальных операций.
        /// </summary>
        [JsonPropertyName("Counters")]
        public ShiftCounters? Counters { get; set; }
    }
}
