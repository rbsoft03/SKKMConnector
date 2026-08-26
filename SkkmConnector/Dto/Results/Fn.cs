using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Описание фискального накопителя (FnInfo).
    /// </summary>
    public class Fn
    {
        /// <summary>
        /// Количество проведённых фискализаций (регистраций/перерегистраций).
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
        /// Флаги систем налогообложения ККТ (битовая маска, см. коды СНО).
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Код причины перерегистрации / изменения параметров.
        /// </summary>
        [JsonPropertyName("ReasonCode")]
        public int ReasonCode { get; set; }

        /// <summary>
        /// Версия ФФД (например 1.05 или 1.2).
        /// </summary>
        [JsonPropertyName("FfdVersion")]
        public string? FfdVersion { get; set; }

        /// <summary>
        /// Заводской номер фискального накопителя.
        /// </summary>
        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Наименование организации (владельца ККТ).
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
        /// Адрес расчётов, зашитый при регистрации.
        /// </summary>
        [JsonPropertyName("SaleAddress")]
        public string? SaleAddress { get; set; }

        /// <summary>
        /// Место расчётов, зашитое при регистрации.
        /// </summary>
        [JsonPropertyName("SaleLocation")]
        public string? SaleLocation { get; set; }

        /// <summary>
        /// Признак агента, зашитый при регистрации (тег 1057).
        /// </summary>
        [JsonPropertyName("SignOfAgent")]
        public int SignOfAgent { get; set; }

        /// <summary>
        /// Номер автомата (для автоматических устройств расчётов).
        /// </summary>
        [JsonPropertyName("AutomaticNumber")]
        public string? AutomaticNumber { get; set; }

        /// <summary>
        /// Оператор фискальных данных (наименование, ИНН).
        /// </summary>
        [JsonPropertyName("Ofd")]
        public Ofd? Ofd { get; set; }

        /// <summary>
        /// Предупреждения ФН (срок, память, непереданные документы).
        /// </summary>
        [JsonPropertyName("Warnings")]
        public Warnings? Warnings { get; set; }

        /// <summary>
        /// Разрешённые режимы работы ККТ (шифрование, автономный, автоматический и т.п.).
        /// </summary>
        [JsonPropertyName("Modes")]
        public FnModes? Modes { get; set; }
    }
}
