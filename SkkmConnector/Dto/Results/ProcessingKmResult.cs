using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Результат проверки кода маркировки в ОИСМ
    /// </summary>
    public class ProcessingKmResult
    {
        /// <summary>
        /// Идентификатор запроса КМ 
        /// </summary>
        [JsonPropertyName("Guid")]
        public string? Guid { get; set; }

        /// <summary>
        /// Итог проверки кода маркировки.
        /// </summary>
        [JsonPropertyName("Result")]
        public bool Result { get; set; }

        /// <summary>
        /// Код результата проверки (тег 2106 ФФД).
        /// </summary>
        [JsonPropertyName("ResultCode")]
        public int ResultCode { get; set; }

        /// <summary>
        /// Статус информации о коде маркировки (тег 2109 ФФД).
        /// </summary>
        [JsonPropertyName("StatusInfo")]
        public int? StatusInfo { get; set; }

        /// <summary>
        /// Код обработки запроса (тег 2105 ФФД).
        /// </summary>
        [JsonPropertyName("HandleCode")]
        public int HandleCode { get; set; }

        /// <summary>
        /// Статус получения результата от ОИСМ: 0 — получен; 1 — ещё не получен; 2 — не может быть получен.
        /// </summary>
        [JsonPropertyName("RequestStatus")]
        public int RequestStatus { get; set; }
    }
}
