using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Параметры проверяемого кода маркировки.
    /// </summary>
    internal class RequestKm
    {
        [JsonPropertyName("Guid")]
        public string? Guid { get; set; }

        [JsonPropertyName("NotSendToServer")]
        public bool NotSendToServer { get; set; }

        [JsonPropertyName("WaitForResult")]
        public bool WaitForResult { get; set; }

        [JsonPropertyName("MarkingCode")]
        public string? MarkingCode { get; set; }

        [JsonPropertyName("PlannedStatus")]
        public int PlannedStatus { get; set; }

        [JsonPropertyName("Quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("MeasureOfQuantity")]
        public int MeasureOfQuantity { get; set; }

        [JsonPropertyName("FractionalQuantityNumerator")]
        public int? FractionalQuantityNumerator { get; set; }

        [JsonPropertyName("FractionalQuantityDenominator")]
        public int? FractionalQuantityDenominator { get; set; }
    }
}
