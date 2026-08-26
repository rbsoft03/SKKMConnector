using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Описание ККМ (KktInfo / DeviceInfo).
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Часовая зона: 0 — авто; 1 — МСК-1 / UTC+2; … 11 — МСК+9 / UTC+12.
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
        /// Тип устройства: 1 — принтер; 2 — чековый принтер; 3 — фискальный регистратор; 4 — онлайн-ККТ по 54-ФЗ;
        /// 5 — эквайринговый терминал; 6 — ТСД; 7 — весы; 8 — весы с печатью этикеток; 9 — сканер штрихкодов.
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
