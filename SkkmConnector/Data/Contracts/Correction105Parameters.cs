using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати чека коррекции ФФД 1.05.
    /// </summary>
    internal class Correction105Parameters : CheckbaseParameters
    {
        /// <summary>
        /// Тип чека
        /// </summary>
        [JsonPropertyName("PaymentType")]
        public int PaymentType { get; set; }

        /// <summary>
        /// Код системы налогообложения.
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Дополнительный реквизит чека (БСО), тег 1192
        /// </summary>
        [JsonPropertyName("AdditionalAttribute")]
        public string? AdditionalAttribute { get; set; }

        /// <summary>
        /// Данные коррекции.
        /// </summary>
        [JsonPropertyName("CorrectionData")]
        public CorrectionData? CorrectionData { get; set; }

        /// <summary>
        /// Список оплаты
        /// </summary>
        [JsonPropertyName("Payments")]
        public Payments? Payments { get; set; }

        /// <summary>
        /// Сумма расчёта по ставке НДС 0%.
        /// </summary>
        [JsonPropertyName("SumTax0")]
        public decimal? SumTax0 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 5%.
        /// </summary>
        [JsonPropertyName("SumTax5")]
        public decimal? SumTax5 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 7%.
        /// </summary>
        [JsonPropertyName("SumTax7")]
        public decimal? SumTax7 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 10%.
        /// </summary>
        [JsonPropertyName("SumTax10")]
        public decimal? SumTax10 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 18%.
        /// </summary>
        [JsonPropertyName("SumTax18")]
        public decimal? SumTax18 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 20%.
        /// </summary>
        [JsonPropertyName("SumTax20")]
        public decimal? SumTax20 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 22%.
        /// </summary>
        [JsonPropertyName("SumTax22")]
        public decimal? SumTax22 { get; set; }

        /// <summary>
        /// Сумма расчёта без НДС.
        /// </summary>
        [JsonPropertyName("SumTaxNone")]
        public decimal? SumTaxNone { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 5/105.
        /// </summary>
        [JsonPropertyName("SumTax105")]
        public decimal? SumTax105 { get; set; }

        /// <summary>
        /// Сумма НДС чека по ставке 7/107.
        /// </summary>
        [JsonPropertyName("SumTax107")]
        public decimal? SumTax107 { get; set; }

        /// <summary>
        /// Сумма НДС чека по расч. ставке 10/110.
        /// </summary>
        [JsonPropertyName("SumTax110")]
        public decimal? SumTax110 { get; set; }

        /// <summary>
        /// Сумма НДС чека по расч. ставке 18/118.
        /// </summary>
        [JsonPropertyName("SumTax118")]
        public decimal? SumTax118 { get; set; }

        /// <summary>
        /// Сумма НДС чека по расч. ставке 20/120.
        /// </summary>
        [JsonPropertyName("SumTax120")]
        public decimal? SumTax120 { get; set; }

        /// <summary>
        /// Сумма НДС чека по расч. ставке 22/122.
        /// </summary>
        [JsonPropertyName("SumTax122")]
        public decimal? SumTax122 { get; set; }
    }
}
