using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// DataKkt — подробная информация об устройстве ККТ (GET kkt).
    /// </summary>
    public class DataKkt
    {
        /// <summary>
        /// Версия сервера ККМ.
        /// </summary>
        [JsonPropertyName("ServerVersion")]
        public string? ServerVersion { get; set; }

        /// <summary>
        /// Описание фискального накопителя (FnInfo).
        /// </summary>
        [JsonPropertyName("Fn")]
        public Fn? Fn { get; set; }

        /// <summary>
        /// Описание ККМ (KktInfo / DeviceInfo).
        /// </summary>
        [JsonPropertyName("Device")]
        public Device? Device { get; set; }

        /// <summary>
        /// Описание драйвера ККМ (DriverInfo).
        /// </summary>
        [JsonPropertyName("Driver")]
        public Driver? Driver { get; set; }

        /// <summary>
        /// Состояние ККТ (KktStatus).
        /// </summary>
        [JsonPropertyName("Status")]
        public KktStatus? Status { get; set; }
    }
}
