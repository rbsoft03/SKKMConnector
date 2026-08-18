using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Сведения о покупателе.
    /// </summary>
    internal class Customer
    {
        /// <summary>
        /// Наименование организации или ФИО.
        /// </summary>
        [JsonPropertyName("Info")]
        public string? Info { get; set; }

        /// <summary>
        /// ИНН организации или покупателя.
        /// </summary>
        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }

        /// <summary>
        /// Email для отправки электронного чека.
        /// </summary>
        [JsonPropertyName("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Телефон для отправки электронного чека.
        /// </summary>
        [JsonPropertyName("Phone")]
        public string? Phone { get; set; }
    }
}
