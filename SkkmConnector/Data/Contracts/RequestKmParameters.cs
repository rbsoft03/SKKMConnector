using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса проверки кода маркировки.
    /// </summary>
    internal class RequestKmParameters
    {
        /// <summary>
        /// Имя кассы.
        /// </summary>
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Параметры проверяемого кода маркировки.
        /// </summary>
        [JsonPropertyName("RequestKM")]
        public RequestKm RequestKM { get; set; } = new();
    }
}
