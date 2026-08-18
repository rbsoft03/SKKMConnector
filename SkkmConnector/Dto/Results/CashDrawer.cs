using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Состояние денежного ящика: сумма наличных и число операций.
    /// </summary>
    public class CashDrawer
    {
        /// <summary>
        /// Сумма наличных в ящике.
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }

        /// <summary>
        /// Количество операций с наличными.
        /// </summary>
        [JsonPropertyName("Count")]
        public int Count { get; set; }
    }
}
