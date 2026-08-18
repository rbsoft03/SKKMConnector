using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ GET ping.
    /// </summary>
    internal class PingResponse
    {
        [JsonPropertyName("product")]
        public string? Product { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }
}
