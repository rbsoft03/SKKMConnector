using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело POST marking/km/request 
    /// </summary>
    internal class RequestKmParameters
    {
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        [JsonPropertyName("RequestKM")]
        public RequestKm RequestKM { get; set; } = new();
    }
}
