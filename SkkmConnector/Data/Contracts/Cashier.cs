using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Кассир, оформляющий документ.
    /// </summary>
    internal class Cashier
    {
        /// <summary>
        /// ФИО кассира
        /// </summary>
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// ИНН кассира
        /// </summary>
        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }
    }
}
