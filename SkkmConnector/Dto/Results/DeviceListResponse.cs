using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Элемент списка ККТ (GET kkt/list).
    /// </summary>
    public class DeviceListResponse
    {
        /// <summary>
        /// Имя устройства.
        /// </summary>
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Тип драйвера: 1 — Shtrih; 2 — 1C(4.7); 3 — Atol; 4 — RrElectro; 5 — 1C(5.0); 100 — Emulator.
        /// </summary>
        [JsonPropertyName("Driver")]
        public DeviceType Driver { get; set; }

        /// <summary>
        /// Имя пула, в который входит устройство.
        /// </summary>
        [JsonPropertyName("Pool")]
        public string? Pool { get; set; }

        /// <summary>
        /// Описание статуса устройства.
        /// </summary>
        [JsonPropertyName("DeviceStatusDescription")]
        public string? DeviceStatusDescription { get; set; }
    }
}
