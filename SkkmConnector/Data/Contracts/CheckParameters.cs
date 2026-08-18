using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати чека: POST check / POST check/async
    /// </summary>
    internal class CheckParameters : CheckbaseParameters
    {
        /// <summary>
        /// Тип чека: 1 - продажа, 2 - возврат, 4 - покупка, 5 - возврат покупки
        /// </summary>
        [JsonPropertyName("PaymentType")]
        public int PaymentType { get; set; }

        /// <summary>
        /// Код системы налогообложения
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Сведения о покупателе
        /// </summary>
        [JsonPropertyName("Customer")]
        public Customer? Customer { get; set; }

        /// <summary>
        /// Место проведения расчётов.
        /// </summary>
        [JsonPropertyName("SaleLocation")]
        public string? SaleLocation { get; set; }

        /// <summary>
        /// Только электронный чек, без печати на бумаге
        /// </summary>
        [JsonPropertyName("Electronically")]
        public bool Electronically { get; set; }

        /// <summary>
        /// Текст перед товарной частью
        /// </summary>
        [JsonPropertyName("TextBefore")]
        public string? TextBefore { get; set; }

        /// <summary>
        /// Текст после товарной части
        /// </summary>
        [JsonPropertyName("TextAfter")]
        public string? TextAfter { get; set; }

        /// <summary>
        /// Дополнительный реквизит чека (тег 1192).
        /// Для чека коррекции сюда записывается ФП корректируемого чека
        /// </summary>
        [JsonPropertyName("AdditionalAttribute")]
        public string? AdditionalAttribute { get; set; }

        /// <summary>
        /// Виды оплаты
        /// </summary>
        [JsonPropertyName("Payments")]
        public Payments? Payments { get; set; }

        /// <summary>
        /// Позиции чека
        /// </summary>
        [JsonPropertyName("Positions")]
        public Position[]? Positions { get; set; }
    }
}
