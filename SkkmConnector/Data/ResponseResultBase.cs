using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Базовый результат операции: код, описание, успех.
    /// </summary>
    internal class ResponseResultBase
    {
        [JsonPropertyName("Code")]
        public int Code { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Success")]
        public bool Success { get; set; }
    }
}
