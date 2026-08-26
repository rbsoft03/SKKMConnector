using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Параметры для печати чека или чека коррекции 1.2
    /// </summary>
    internal class CheckParameters : CheckbaseParameters
    {
        /// <summary>
        /// Тип чека
        /// </summary>
        [JsonPropertyName("PaymentType")]
        public int PaymentType { get; set; }

        /// <summary>
        /// Код системы налогообложения
        /// </summary>
        [JsonPropertyName("TaxVariant")]
        public int TaxVariant { get; set; }

        /// <summary>
        /// Сведения о покупателе (клиенте)
        /// </summary>
        [JsonPropertyName("Customer")]
        public Customer? Customer { get; set; }

        /// <summary>
        /// Место проведения расчетов
        /// </summary>
        [JsonPropertyName("SaleLocation")]
        public string? SaleLocation { get; set; }

        /// <summary>
        /// Адрес проведения расчетов
        /// </summary>
        [JsonPropertyName("SaleAddress")]
        public string? SaleAddress { get; set; }

        /// <summary>
        /// Адрес электронной почты отправителя чека
        /// </summary>
        [JsonPropertyName("SenderEmail")]
        public string? SenderEmail { get; set; }

        /// <summary>
        /// Признак применения ККТ при осуществлении расчета в безналичном порядке в сети «Интернет»
        /// </summary>
        [JsonPropertyName("OperationOnline")]
        public bool? OperationOnline { get; set; }

        /// <summary>
        /// Отраслевой реквизит чека
        /// </summary>
        [JsonPropertyName("IndustryAttribute")]
        public Industry? IndustryAttribute { get; set; }

        /// <summary>
        /// Дополнительный реквизит пользователя
        /// </summary>
        [JsonPropertyName("UserAttribute")]
        public UserAttribute? UserAttribute { get; set; }

        /// <summary>
        /// Операционный реквизит чека
        /// </summary>
        [JsonPropertyName("OperationalAttribute")]
        public OperationalAttribute? OperationalAttribute { get; set; }

        /// <summary>
        /// Сведения об оплате безналичными
        /// </summary>
        [JsonPropertyName("ElectronicPaymentInfo")]
        public List<ElectronicPayment>? ElectronicPaymentInfo { get; set; }

        /// <summary>
        /// Формирование чека только в электронном виде
        /// </summary>
        [JsonPropertyName("Electronically")]
        public bool Electronically { get; set; }

        /// <summary>
        /// Номер часовой зоны места расчётов.
        /// Если поле не указано, используется значение из поля «Часовая зона» в настройках ККТ.
        /// </summary>
        [JsonPropertyName("TimeZone")]
        public int? TimeZone { get; set; }

        /// <summary>
        /// Текст для печати перед товарной частью
        /// </summary>
        [JsonPropertyName("TextBefore")]
        public string? TextBefore { get; set; }

        /// <summary>
        /// Текст для печати после товарной части чека
        /// </summary>
        [JsonPropertyName("TextAfter")]
        public string? TextAfter { get; set; }

        /// <summary>
        /// Дополнительный реквизит чека (БСО), тег 1192
        /// </summary>
        [JsonPropertyName("AdditionalAttribute")]
        public string? AdditionalAttribute { get; set; }

        /// <summary>
        /// Признак агента
        /// </summary>
        [JsonPropertyName("AgentSign")]
        public int? AgentSign { get; set; }

        /// <summary>
        /// Данные агента
        /// </summary>
        [JsonPropertyName("AgentData")]
        public Agent? AgentData { get; set; }

        /// <summary>
        /// Данные поставщика
        /// </summary>
        [JsonPropertyName("Vendor")]
        public Vendor? Vendor { get; set; }

        /// <summary>
        /// Оплаты
        /// </summary>
        [JsonPropertyName("Payments")]
        public Payments? Payments { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        [JsonPropertyName("Positions")]
        public ApiPosition[]? Positions { get; set; }
    }
}
