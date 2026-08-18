using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Сведения об устройстве (модель, прошивка, конфигурация)
    /// </summary>
    public class Device
    {
        [JsonPropertyName("TimeZone")]
        public int TimeZone { get; set; }

        [JsonPropertyName("IsFiscal")]
        public bool IsFiscal { get; set; }

        [JsonPropertyName("LineLength")]
        public int LineLength { get; set; }

        [JsonPropertyName("Model")]
        public string? Model { get; set; }

        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("FirmwareVersion")]
        public string? FirmwareVersion { get; set; }

        [JsonPropertyName("ConfigurationVersion")]
        public string? ConfigurationVersion { get; set; }

        /// <summary>
        /// Лицензии ККТ. Пустой массив — лицензий нет.
        /// </summary>
        [JsonPropertyName("KktLicenses")]
        public KktLicense[]? KktLicenses { get; set; }
    }
}
