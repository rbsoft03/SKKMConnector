using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Параметры проверяемого кода маркировки.
    /// </summary>
    internal class RequestKm
    {
        /// <summary>
        /// Идентификатор запроса проверки.
        /// </summary>
        [JsonPropertyName("Guid")]
        public string? Guid { get; set; }

        /// <summary>
        /// Не отправлять запрос на сервер ОИСМ (только локальная проверка).
        /// </summary>
        [JsonPropertyName("NotSendToServer")]
        public bool NotSendToServer { get; set; }

        /// <summary>
        /// Ждать ответ ОИСМ.
        /// </summary>
        [JsonPropertyName("WaitForResult")]
        public bool WaitForResult { get; set; }

        /// <summary>
        /// Код маркировки в Base64.
        /// </summary>
        [JsonPropertyName("MarkingCode")]
        public string? MarkingCode { get; set; }

        /// <summary>
        /// Планируемый статус товара (тег 2003).
        /// </summary>
        [JsonPropertyName("PlannedStatus")]
        public int PlannedStatus { get; set; }

        /// <summary>
        /// Количество предмета расчёта.
        /// </summary>
        [JsonPropertyName("Quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Мера количества предмета расчёта (таблица 114 ФФД).
        /// </summary>
        [JsonPropertyName("MeasureOfQuantity")]
        public int MeasureOfQuantity { get; set; }

        /// <summary>
        /// Числитель дробного количества маркированного товара.
        /// </summary>
        [JsonPropertyName("FractionalQuantityNumerator")]
        public int? FractionalQuantityNumerator { get; set; }

        /// <summary>
        /// Знаменатель дробного количества маркированного товара.
        /// </summary>
        [JsonPropertyName("FractionalQuantityDenominator")]
        public int? FractionalQuantityDenominator { get; set; }
    }
}
