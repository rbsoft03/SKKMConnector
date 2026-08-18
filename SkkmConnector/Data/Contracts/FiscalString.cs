using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Фискальная строка чека.
    /// </summary>
    internal class FiscalString
    {
        /// <summary>
        /// Наименование товара.
        /// </summary>
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        [JsonPropertyName("Quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Цена единицы товара с учетом скидок/наценок.
        /// </summary>
        [JsonPropertyName("PriceWithDiscount")]
        public decimal PriceWithDiscount { get; set; }

        /// <summary>
        /// Конечная сумма по позиции с учетом всех скидок/наценок.
        /// </summary>
        [JsonPropertyName("SumWithDiscount")]
        public decimal SumWithDiscount { get; set; }

        /// <summary>
        /// Сумма скидок и наценок (>0 — скидка, &lt;0 — наценка).
        /// </summary>
        [JsonPropertyName("DiscountSum")]
        public decimal DiscountSum { get; set; }

        /// <summary>
        /// Отдел, по которому ведется продажа.
        /// </summary>
        [JsonPropertyName("Department")]
        public int Department { get; set; }

        /// <summary>
        /// Ставка НДС: "none", "0", "10", "20", "22", "10/110", "20/120", "22/122".
        /// </summary>
        [JsonPropertyName("Tax")]
        public string? Tax { get; set; }

        /// <summary>
        /// Сумма НДС за предмет расчета.
        /// </summary>
        [JsonPropertyName("TaxSum")]
        public decimal TaxSum { get; set; }

        /// <summary>
        /// Признак способа расчета: 1 - предоплата полная ... 4 - полный расчет ...
        /// </summary>
        [JsonPropertyName("SignMethodCalculation")]
        public int? SignMethodCalculation { get; set; }

        /// <summary>
        /// Признак предмета расчета: 1 - товар, 3 - работа, 4 - услуга ...
        /// </summary>
        [JsonPropertyName("SignCalculationObject")]
        public int? SignCalculationObject { get; set; }

        /// <summary>
        /// Единица измерения предмета расчета.
        /// </summary>
        [JsonPropertyName("MeasurementUnit")]
        public string? MeasurementUnit { get; set; }

        [JsonPropertyName("ExciseAmount")]
        public decimal? ExciseAmount { get; set; }

        [JsonPropertyName("CountryOfOrigin")]
        public string? CountryOfOrigin { get; set; }

        [JsonPropertyName("CustomsDeclaration")]
        public string? CustomsDeclaration { get; set; }

        [JsonPropertyName("SignSubjectCalculationAgent")]
        public int? SignSubjectCalculationAgent { get; set; }

        [JsonPropertyName("AgentData")]
        public AgentData? AgentData { get; set; }

        [JsonPropertyName("Vendor")]
        public VendorData? Vendor { get; set; }

        [JsonPropertyName("GoodCodeData")]
        public GoodCodeData? GoodCodeData { get; set; }

        [JsonPropertyName("MeasureOfQuantity")]
        public int? MeasureOfQuantity { get; set; }

        [JsonPropertyName("FractionalQuantity")]
        public FractionalQuantity? FractionalQuantity { get; set; }

        [JsonPropertyName("AdditionalAttribute")]
        public string? AdditionalAttribute { get; set; }

        [JsonPropertyName("IndustryAttribute")]
        public IndustryAttribute? IndustryAttribute { get; set; }
    }
}
