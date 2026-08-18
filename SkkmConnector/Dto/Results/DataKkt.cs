using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Ответ GET kkt — полные сведения о кассе:
    /// сервер, данные регистрации ФН, устройство, драйвер, текущее состояние
    /// </summary>
    public class DataKkt
    {
        /// <summary>
        /// Версия сервера ККМ
        /// </summary>
        [JsonPropertyName("ServerVersion")]
        public string? ServerVersion { get; set; }

        /// <summary>
        /// Данные регистрации фискального накопителя
        /// </summary>
        [JsonPropertyName("Fn")]
        public Fn? Fn { get; set; }

        /// <summary>
        /// Сведения об устройстве
        /// </summary>
        [JsonPropertyName("Device")]
        public Device? Device { get; set; }

        /// <summary>
        /// Сведения о драйвере
        /// </summary>
        [JsonPropertyName("Driver")]
        public Driver? Driver { get; set; }

        /// <summary>
        /// Текущее состояние ККМ
        /// </summary>
        [JsonPropertyName("Status")]
        public KktStatus? Status { get; set; }
    }
}
