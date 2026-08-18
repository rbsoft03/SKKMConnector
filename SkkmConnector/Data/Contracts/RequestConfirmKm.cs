using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело POST marking/km/confirm.
    /// </summary>
    internal class RequestConfirmKm
    {
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        [JsonPropertyName("GUID")]
        public string? GUID { get; set; }

        [JsonPropertyName("ConfirmationType")]
        public int ConfirmationType { get; set; }
    }
}
