using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Описание драйвера ККМ 
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Тип драйвера.
        /// </summary>
        [JsonPropertyName("Type")]
        public string? Type { get; set; }

        /// <summary>
        /// Версия драйвера
        /// </summary>
        [JsonPropertyName("Version")]
        public string? Version { get; set; }

        /// <summary>
        /// Данные поставщика.
        /// </summary>
        [JsonPropertyName("Vendor")]
        public string? Vendor { get; set; }
    }
}
