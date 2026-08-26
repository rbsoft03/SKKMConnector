using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Последняя операция из базы сервера.
    /// </summary>
    internal class LastOperationDto
    {
        /// <summary>
        /// Дата и время операции.
        /// </summary>
        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип операции.
        /// </summary>
        [JsonPropertyName("TaskType")]
        public int TaskType { get; set; }

        /// <summary>
        /// Номер фискального документа.
        /// </summary>
        [JsonPropertyName("DocNumber")]
        public int DocNumber { get; set; }

        /// <summary>
        /// Номер смены.
        /// </summary>
        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Сумма документа.
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }
    }
}
