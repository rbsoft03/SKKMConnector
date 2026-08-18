using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ GET kkt/counters/overall
    /// </summary>
    internal class OverallTotals
    {
        [JsonPropertyName("Counters")]
        public ShiftCounters? Counters { get; set; }
    }
}
