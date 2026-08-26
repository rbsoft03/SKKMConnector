using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Описание фискального накопителя
    /// </summary>
    public class Fn
    {
        /// <summary>
        /// Количество проведённых фискализаций
        /// </summary>
        [JsonPropertyName("FiscalizationsCount")]
        public int FiscalizationsCount { get; set; }

        /// <summary>
        /// Дата и время последней фискализации.
        /// </summary>
        [JsonPropertyName("FiscalizationDateTime")]
        public DateTime FiscalizationDateTime { get; set; }

        /// <summary>
        /// Регистрационный номер ККТ (РНМ).
        /// </summary>
        [JsonPropertyName("RnNumber")]
        public string? RnNumber { get; set; }

        /// <summary>
        /// Адрес сайта ФНС, напечатанный на чеке.
        /// </summary>
        [JsonPropertyName("FnsUrl")]
        public string? FnsUrl { get; set; }

        /// <summary>
        /// Email отправителя электронных чеков.
        /// </summary>
        [JsonPropertyName("SenderEmail")]
        public string? SenderEmail { get; set; }

        /// <summary>
        /// Код систем налогообложения 
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Код причины перерегистрации / изменения параметров.
        /// </summary>
        [JsonPropertyName("ReasonCode")]
        public int ReasonCode { get; set; }

        /// <summary>
        /// Версия ФФД 
        /// </summary>
        [JsonPropertyName("FfdVersion")]
        public string? FfdVersion { get; set; }

        /// <summary>
        /// Заводской номер фискального накопителя.
        /// </summary>
        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Наименование организации 
        /// </summary>
        [JsonPropertyName("OrganizationName")]
        public string? OrganizationName { get; set; }

        /// <summary>
        /// ИНН владельца ККТ.
        /// </summary>
        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }

        /// <summary>
        /// Дата окончания срока действия ФН.
        /// </summary>
        [JsonPropertyName("ValidityDate")]
        public DateTime ValidityDate { get; set; }

        /// <summary>
        /// Адрес расчётов
        /// </summary>
        [JsonPropertyName("SaleAddress")]
        public string? SaleAddress { get; set; }

        /// <summary>
        /// Место расчётов
        /// </summary>
        [JsonPropertyName("SaleLocation")]
        public string? SaleLocation { get; set; }

        /// <summary>
        /// Признак агента (тег 1057).
        /// </summary>
        [JsonPropertyName("SignOfAgent")]
        public int SignOfAgent { get; set; }

        /// <summary>
        /// Номер автомата
        /// </summary>
        [JsonPropertyName("AutomaticNumber")]
        public string? AutomaticNumber { get; set; }

        /// <summary>
        /// Оператор фискальных данных
        /// </summary>
        [JsonPropertyName("Ofd")]
        public Ofd? Ofd { get; set; }

        /// <summary>
        /// Предупреждения ФН 
        /// </summary>
        [JsonPropertyName("Warnings")]
        public Warnings? Warnings { get; set; }

        /// <summary>
        /// Разрешённые режимы работы ККТ 
        /// </summary>
        [JsonPropertyName("Modes")]
        public FnModes? Modes { get; set; }
    }
}
