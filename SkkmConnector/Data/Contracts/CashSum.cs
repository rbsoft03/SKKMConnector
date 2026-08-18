using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ GET cash — остаток наличных в денежном ящике
    /// </summary>
    internal class CashSum
    {
        /// <summary>
        /// Сумма наличных в денежном ящике
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }
    }
}
