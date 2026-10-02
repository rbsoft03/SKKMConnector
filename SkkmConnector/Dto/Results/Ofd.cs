using System.Text.Json.Serialization;

namespace RBSoftSkkm
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

        /// <summary>
        /// Адрес сервера ОФД.
        /// </summary>
        [JsonPropertyName("Host")]
        public string? Host { get; set; }

        /// <summary>
        /// Порт сервера ОФД.
        /// </summary>
        [JsonPropertyName("Port")]
        public int Port { get; set; }
    }
}
