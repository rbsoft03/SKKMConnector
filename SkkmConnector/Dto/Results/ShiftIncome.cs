using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Итог внесений или выемок за смену: количество операций и сумма.
    /// </summary>
    public class ShiftIncome
    {
        /// <summary>
        /// Количество операций (внесений или выемок).
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
