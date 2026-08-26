using System.Text.Json.Serialization;

namespace SkkmConnector
{
    public class DataKkt
    {
        /// <summary>
        /// Версия сервера ККМ.
        /// </summary>
        [JsonPropertyName("ServerVersion")]
        public string? ServerVersion { get; set; }

        /// <summary>
        /// Описание фискального накопителя 
        /// </summary>
        [JsonPropertyName("Fn")]
        public Fn? Fn { get; set; }

        /// <summary>
        /// Описание ККМ
        /// </summary>
        [JsonPropertyName("Device")]
        public Device? Device { get; set; }

        /// <summary>
        /// Описание драйвера ККМ 
        /// </summary>
        [JsonPropertyName("Driver")]
        public Driver? Driver { get; set; }

        /// <summary>
        /// Состояние ККТ 
        /// </summary>
        [JsonPropertyName("Status")]
        public KktStatus? Status { get; set; }
    }
}
