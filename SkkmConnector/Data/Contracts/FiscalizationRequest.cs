using System.Text.Json.Serialization;

namespace SkkmConnector.Internal;

/// <summary>
/// Тело запроса фискализации ККТ.
/// </summary>
internal sealed class FiscalizationRequest : CheckbaseParameters
{
    /// <summary>
    /// Регистрационный номер ККТ.
    /// </summary>
    [JsonPropertyName("RnNumber")]
    public string? RnNumber { get; set; }

    /// <summary>
    /// Коды систем налогообложения через запятую.
    /// </summary>
    [JsonPropertyName("TaxationSystems")]
    public string? TaxationSystems { get; set; }

    /// <summary>
    /// ИНН организации.
    /// </summary>
    [JsonPropertyName("Vatin")]
    public string? Vatin { get; set; }

    /// <summary>
    /// Название организации.
    /// </summary>
    [JsonPropertyName("CompanyName")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// Заводской номер ФН.
    /// </summary>
    [JsonPropertyName("Fn")]
    public string? Fn { get; set; }

    /// <summary>
    /// Версия ФФД ККТ.
    /// </summary>
    [JsonPropertyName("FfdVersionKkt")]
    public string? FfdVersionKkt { get; set; }

    /// <summary>
    /// Версия ФФД ФН.
    /// </summary>
    [JsonPropertyName("FfdVersionFn")]
    public string? FfdVersionFn { get; set; }

    /// <summary>
    /// Коды причин изменения сведений о ККТ.
    /// </summary>
    [JsonPropertyName("RegistrationLabelCodes")]
    public string? RegistrationLabelCodes { get; set; }

    /// <summary>
    /// Адрес ОФД.
    /// </summary>
    [JsonPropertyName("OfdAddress")]
    public string? OfdAddress { get; set; }

    /// <summary>
    /// Порт ОФД.
    /// </summary>
    [JsonPropertyName("OfdPort")]
    public int? OfdPort { get; set; }

    /// <summary>
    /// Номер автоматического устройства для расчётов.
    /// </summary>
    [JsonPropertyName("AutomaticNumber")]
    public string? AutomaticNumber { get; set; }

    /// <summary>
    /// Email отправителя чека.
    /// </summary>
    [JsonPropertyName("SenderEmail")]
    public string? SenderEmail { get; set; }

    /// <summary>
    /// Код причины перерегистрации.
    /// </summary>
    [JsonPropertyName("ReasonCode")]
    public FiscalizationReasonCode? ReasonCode { get; set; }

    /// <summary>
    /// Хост ИСМ.
    /// </summary>
    [JsonPropertyName("IsmHost")]
    public string? IsmHost { get; set; }

    /// <summary>
    /// Порт ИСМ.
    /// </summary>
    [JsonPropertyName("IsmPort")]
    public int? IsmPort { get; set; }

    /// <summary>
    /// Адрес сайта ФНС.
    /// </summary>
    [JsonPropertyName("FnsUrl")]
    public string? FnsUrl { get; set; }

    /// <summary>
    /// ИНН ОФД.
    /// </summary>
    [JsonPropertyName("OfdVatin")]
    public string? OfdVatin { get; set; }

    /// <summary>
    /// Название ОФД.
    /// </summary>
    [JsonPropertyName("OfdName")]
    public string? OfdName { get; set; }

    /// <summary>
    /// Коды признаков агента через запятую.
    /// </summary>
    [JsonPropertyName("AgentTypes")]
    public string? AgentTypes { get; set; }

    /// <summary>
    /// Признак формирования АС БСО.
    /// </summary>
    [JsonPropertyName("IsBsoSign")]
    public bool? IsBsoSign { get; set; }

    /// <summary>
    /// Признак торговли маркированными товарами.
    /// </summary>
    [JsonPropertyName("IsMarking")]
    public bool? IsMarking { get; set; }

    /// <summary>
    /// Признак ломбардной деятельности.
    /// </summary>
    [JsonPropertyName("IsPawnshop")]
    public bool? IsPawnshop { get; set; }

    /// <summary>
    /// Признак страховой деятельности.
    /// </summary>
    [JsonPropertyName("IsAssurance")]
    public bool? IsAssurance { get; set; }

    /// <summary>
    /// Признак автоматического режима.
    /// </summary>
    [JsonPropertyName("IsAutomatic")]
    public bool? IsAutomatic { get; set; }

    /// <summary>
    /// Признак применения в торговом автомате.
    /// </summary>
    [JsonPropertyName("IsVending")]
    public bool? IsVending { get; set; }

    /// <summary>
    /// Признак установки принтера в автомате.
    /// </summary>
    [JsonPropertyName("IsAutomaticPrinter")]
    public bool? IsAutomaticPrinter { get; set; }

    /// <summary>
    /// Признак расчётов только в интернете.
    /// </summary>
    [JsonPropertyName("IsOnline")]
    public bool? IsOnline { get; set; }

    /// <summary>
    /// Признак проведения лотерей.
    /// </summary>
    [JsonPropertyName("IsLottery")]
    public bool? IsLottery { get; set; }

    /// <summary>
    /// Признак проведения азартных игр.
    /// </summary>
    [JsonPropertyName("IsGambling")]
    public bool? IsGambling { get; set; }

    /// <summary>
    /// Признак продажи подакцизных товаров.
    /// </summary>
    [JsonPropertyName("IsExcisable")]
    public bool? IsExcisable { get; set; }

    /// <summary>
    /// Признак расчётов за услуги.
    /// </summary>
    [JsonPropertyName("IsService")]
    public bool? IsService { get; set; }

    /// <summary>
    /// Признак шифрования данных.
    /// </summary>
    [JsonPropertyName("IsEncrypted")]
    public bool? IsEncrypted { get; set; }

    /// <summary>
    /// Признак автономного режима.
    /// </summary>
    [JsonPropertyName("IsOffline")]
    public bool? IsOffline { get; set; }

    /// <summary>
    /// Признак общественного питания.
    /// </summary>
    [JsonPropertyName("IsCateringServices")]
    public bool? IsCateringServices { get; set; }

    /// <summary>
    /// Признак оптовой торговли.
    /// </summary>
    [JsonPropertyName("IsWholesaleTrade")]
    public bool? IsWholesaleTrade { get; set; }

    /// <summary>
    /// Адрес расчётов.
    /// </summary>
    [JsonPropertyName("SaleAddress")]
    public string? SaleAddress { get; set; }

    /// <summary>
    /// Место расчётов.
    /// </summary>
    [JsonPropertyName("SaleLocation")]
    public string? SaleLocation { get; set; }
}
