using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Базовый результат операции: код, описание, успех.
    /// </summary>
    internal class ResponseResultBase
    {
        /// <summary>
        /// Код результата (0 - успех).
        /// </summary>
        [JsonPropertyName("Code")]
        public int Code { get; set; }

        /// <summary>
        /// Описание результата или ошибки.
        /// </summary>
        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        /// <summary>
        /// Признак успешного выполнения.
        /// </summary>
        [JsonPropertyName("Success")]
        public bool Success { get; set; }
    }
}
