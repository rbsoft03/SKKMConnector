using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса подтверждения кода маркировки.
    /// </summary>
    internal class RequestConfirmKm
    {
        /// <summary>
        /// Имя кассы.
        /// </summary>
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Идентификатор запроса проверки кода маркировки.
        /// </summary>
        [JsonPropertyName("GUID")]
        public string? GUID { get; set; }

        /// <summary>
        /// Тип подтверждения: 0 - включить в документ, 1 - не включать.
        /// </summary>
        [JsonPropertyName("ConfirmationType")]
        public int ConfirmationType { get; set; }
    }
}
