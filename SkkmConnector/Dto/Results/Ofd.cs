using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Оператор фискальных данных (Ofd).
    /// </summary>
    public class Ofd
    {
        /// <summary>
        /// Имя ОФД.
        /// </summary>
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// ИНН ОФД.
        /// </summary>
        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }
    }
}
