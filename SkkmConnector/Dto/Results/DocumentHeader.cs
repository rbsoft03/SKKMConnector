using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Заголовок фискального документа
/// </summary>
public sealed class DocumentHeader
{
    /// <summary>
    /// Название организации.
    /// </summary>
    [JsonPropertyName("OrganizationInfo")]
    public string? OrganizationInfo { get; set; }

    /// <summary>
    /// Заводской номер ККТ.
    /// </summary>
    [JsonPropertyName("SerialNumber")]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// ИНН организации.
    /// </summary>
    [JsonPropertyName("Vatin")]
    public string? Vatin { get; set; }

    /// <summary>
    /// Кассир.
    /// </summary>
    [JsonPropertyName("Cashier")]
    public string? Cashier { get; set; }

    /// <summary>
    /// Регистрационный номер ККТ.
    /// </summary>
    [JsonPropertyName("RnNumber")]
    public string? RnNumber { get; set; }

    /// <summary>
    /// Фискальный накопитель.
    /// </summary>
    [JsonPropertyName("Fn")]
    public string? Fn { get; set; }

    /// <summary>
    /// Адрес сайта уполномоченного органа (ФНС) в сети «Интернет».
    /// </summary>
    [JsonPropertyName("FnsUrl")]
    public string? FnsUrl { get; set; }

    /// <summary>
    /// Номер смены.
    /// </summary>
    [JsonPropertyName("ShiftNumber")]
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер фискального документа.
    /// </summary>
    [JsonPropertyName("DocNumber")]
    public int DocNumber { get; set; }

    /// <summary>
    /// Фискальный признак документа.
    /// </summary>
    [JsonPropertyName("FiscalSign")]
    public string? FiscalSign { get; set; }

    /// <summary>
    /// Наименование провайдера ОФД.
    /// </summary>
    [JsonPropertyName("OfdOrganizationName")]
    public string? OfdOrganizationName { get; set; }

    /// <summary>
    /// ИНН провайдера ОФД.
    /// </summary>
    [JsonPropertyName("OfdVatin")]
    public string? OfdVatin { get; set; }
}
