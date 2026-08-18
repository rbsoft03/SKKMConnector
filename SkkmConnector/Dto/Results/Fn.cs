using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Данные регистрации фискального накопителя
    /// </summary>
    public class Fn
    {
        [JsonPropertyName("FiscalizationsCount")]
        public int FiscalizationsCount { get; set; }

        [JsonPropertyName("FiscalizationDateTime")]
        public DateTime FiscalizationDateTime { get; set; }

        [JsonPropertyName("RnNumber")]
        public string? RnNumber { get; set; }

        [JsonPropertyName("FnsUrl")]
        public string? FnsUrl { get; set; }

        /// <summary>
        /// Email отправителя электронных чеков.
        /// </summary>
        [JsonPropertyName("SenderEmail")]
        public string? SenderEmail { get; set; }

        /// <summary>
        /// Флаги систем налогообложения ККТ (битовая маска, см. коды СНО).
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Код причины перерегистрации / изменения параметров.
        /// </summary>
        [JsonPropertyName("ReasonCode")]
        public int ReasonCode { get; set; }

        [JsonPropertyName("FfdVersion")]
        public string? FfdVersion { get; set; }

        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("OrganizationName")]
        public string? OrganizationName { get; set; }

        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }

        [JsonPropertyName("ValidityDate")]
        public DateTime ValidityDate { get; set; }

        [JsonPropertyName("SaleAddress")]
        public string? SaleAddress { get; set; }

        [JsonPropertyName("SaleLocation")]
        public string? SaleLocation { get; set; }

        [JsonPropertyName("SignOfAgent")]
        public int SignOfAgent { get; set; }

        [JsonPropertyName("AutomaticNumber")]
        public string? AutomaticNumber { get; set; }

        [JsonPropertyName("Ofd")]
        public Ofd? Ofd { get; set; }

        [JsonPropertyName("Warnings")]
        public Warnings? Warnings { get; set; }

        [JsonPropertyName("Modes")]
        public FnModes? Modes { get; set; }
    }
}
