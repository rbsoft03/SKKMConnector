using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Итог внесений или выемок за смену
    /// </summary>
    public class ShiftIncome
    {
        /// <summary>
        /// Количество операций 
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
