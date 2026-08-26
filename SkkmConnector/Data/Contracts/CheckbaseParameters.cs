using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Базовые параметры кассового документа
    /// </summary>
    internal class CheckbaseParameters
    {
        /// <summary>
        /// Имя кассы
        /// </summary>
        [JsonPropertyName("DeviceName")]
        [JsonPropertyOrder(-3)]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        [JsonPropertyName("DocId")]
        [JsonPropertyOrder(-2)]
        public string? DocId { get; set; }

        /// <summary>
        /// Кассир
        /// </summary>
        [JsonPropertyName("Cashier")]
        [JsonPropertyOrder(-1)]
        public Cashier? Cashier { get; set; }
    }
}
