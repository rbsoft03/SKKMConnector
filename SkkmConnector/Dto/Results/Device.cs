using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Описание ККМ 
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Часовая зона
        /// </summary>
        [JsonPropertyName("TimeZone")]
        public int TimeZone { get; set; }

        /// <summary>
        /// Фискальный режим.
        /// </summary>
        [JsonPropertyName("IsFiscal")]
        public bool IsFiscal { get; set; }

        /// <summary>
        /// Ширина чековой ленты.
        /// </summary>
        [JsonPropertyName("LineLength")]
        public int LineLength { get; set; }

        /// <summary>
        /// Ширина чековой ленты в пикселях.
        /// </summary>
        [JsonPropertyName("LineLengthPixels")]
        public int LineLengthPixels { get; set; }

        /// <summary>
        /// Версия ФФД.
        /// </summary>
        [JsonPropertyName("FfdVersion")]
        public string? FfdVersion { get; set; }

        /// <summary>
        /// Версия ФФД ФН.
        /// </summary>
        [JsonPropertyName("FnFfdVersion")]
        public string? FnFfdVersion { get; set; }

        /// <summary>
        /// Тип устройства
        /// </summary>
        [JsonPropertyName("DeviceClass")]
        public int DeviceClass { get; set; }

        /// <summary>
        /// Название модели.
        /// </summary>
        [JsonPropertyName("Model")]
        public string? Model { get; set; }

        /// <summary>
        /// Заводской номер ККТ.
        /// </summary>
        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Версия прошивки.
        /// </summary>
        [JsonPropertyName("FirmwareVersion")]
        public string? FirmwareVersion { get; set; }

        /// <summary>
        /// Версия конфигурации прошивки устройства.
        /// </summary>
        [JsonPropertyName("ConfigurationVersion")]
        public string? ConfigurationVersion { get; set; }

        /// <summary>
        /// Массив лицензий ККТ.
        /// </summary>
        [JsonPropertyName("KktLicenses")]
        public KktLicense[]? KktLicenses { get; set; }
    }
}
