using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Поля последней операции из GET operation/last.
    /// </summary>
    internal class LastOperationDto
    {
        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("TaskType")]
        public int TaskType { get; set; }

        [JsonPropertyName("DocNumber")]
        public int DocNumber { get; set; }

        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }
    }
}
