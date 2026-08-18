using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Базовые параметры кассового документа: касса, идентификатор документа, кассир.
    /// Используется как тело запросов shift/open, shift/z, shift/x.
    /// </summary>
    internal class CheckbaseParameters
    {
        /// <summary>
        /// Имя кассы, зарегистрированной на сервере
        /// </summary>
        [JsonPropertyName("DeviceName")]
        [JsonPropertyOrder(-3)]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа (Guid). Защита от повторной печати:
        /// сервер отклонит повторную отправку документа с тем же DocId.
        /// </summary>
        [JsonPropertyName("DocId")]
        [JsonPropertyOrder(-2)]
        public string? DocId { get; set; }

        /// <summary>
        /// Кассир, оформляющий документ
        /// </summary>
        [JsonPropertyName("Cashier")]
        [JsonPropertyOrder(-1)]
        public Cashier? Cashier { get; set; }
    }
}
