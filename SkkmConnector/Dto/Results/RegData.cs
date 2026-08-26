using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Счётчик скидок или надбавок
    /// </summary>
    public class RegData
    {
        /// <summary>
        /// Количество операций (скидок или надбавок).
        /// </summary>
        [JsonPropertyName("Count")]
        public int Count { get; set; }

        /// <summary>
        /// Сумма операций.
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }
    }
}
