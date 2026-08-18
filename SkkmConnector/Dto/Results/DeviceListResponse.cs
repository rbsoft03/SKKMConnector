using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Элемент ответа GET kkt/list 
    /// </summary>
    public class DeviceListResponse
    {
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        [JsonPropertyName("Driver")]
        public DeviceType Driver { get; set; }

        [JsonPropertyName("Pool")]
        public string? Pool { get; set; }

        [JsonPropertyName("DeviceStatusDescription")]
        public string? DeviceStatusDescription { get; set; }
    }
}
