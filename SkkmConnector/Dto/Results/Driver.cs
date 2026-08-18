using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Сведения о драйвере ККМ
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Тип драйвера
        /// </summary>
        [JsonPropertyName("Type")]
        public string? Type { get; set; }

        /// <summary>
        /// Версия драйвера
        /// </summary>
        [JsonPropertyName("Version")]
        public string? Version { get; set; }

        /// <summary>
        /// Производитель драйвера
        /// </summary>
        [JsonPropertyName("Vendor")]
        public string? Vendor { get; set; }
    }
}
