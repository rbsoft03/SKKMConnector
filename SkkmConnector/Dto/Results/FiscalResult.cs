using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Фискальный блок ответа сервера
/// </summary>
public class FiscalResult
{
    /// <summary>
    /// Время операции на сервере.
    /// </summary>
    [JsonPropertyName("datetime")]
    public string? DateTime { get; set; }

    /// <summary>
    /// Название устройства.
    /// </summary>
    [JsonPropertyName("deviceName")]
    public string? DeviceName { get; set; }

    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    [JsonPropertyName("docId")]
    public string? DocId { get; set; }

    /// <summary>
    /// Адрес сайта уполномоченного органа (ФНС) в сети «Интернет».
    /// </summary>
    [JsonPropertyName("fnsUrl")]
    public string? FnsUrl { get; set; }

    /// <summary>
    /// Номер фискального накопителя.
    /// </summary>
    [JsonPropertyName("fnNumber")]
    public string? FnNumber { get; set; }

    /// <summary>
    /// Регистрационный номер ККТ.
    /// </summary>
    [JsonPropertyName("rnNumber")]
    public string? RnNumber { get; set; }

    /// <summary>
    /// Дата и время документа по часам ФН.
    /// </summary>
    [JsonPropertyName("fiscalDatetime")]
    public string? FiscalDateTime { get; set; }

    /// <summary>
    /// Фискальный признак документа.
    /// </summary>
    [JsonPropertyName("fiscalSign")]
    public string? FiscalSign { get; set; }

    /// <summary>
    /// Номер смены.
    /// </summary>
    [JsonPropertyName("shiftNumber")]
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер фискального документа.
    /// </summary>
    [JsonPropertyName("fiscalNumber")]
    public int FiscalNumber { get; set; }

    /// <summary>
    /// Состояние смены. Используйте enum <see cref="ShiftState"/>.
    /// </summary>
    [JsonPropertyName("ShiftState")]
    public ShiftState? ShiftState { get; set; }

    /// <summary>
    /// Сумма наличных в ящике
    /// </summary>
    [JsonPropertyName("CashSum")]
    public decimal? CashSum { get; set; }

    /// <summary>
    /// Состояние денежного ящика.
    /// </summary>
    [JsonPropertyName("CashDrawer")]
    public CashDrawer? CashDrawer { get; set; }

    /// <summary>
    /// Очередь непереданных в ОФД документов.
    /// </summary>
    [JsonPropertyName("Backlog")]
    public Backlog? Backlog { get; set; }

    /// <summary>
    /// Дополнительные параметры вывода (статус ККТ вложенный в Result).
    /// </summary>
    [JsonPropertyName("OutputParameters")]
    public FiscalOutputParameters? OutputParameters { get; set; }
}
