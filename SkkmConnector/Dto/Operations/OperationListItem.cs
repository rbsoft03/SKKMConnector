using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Краткая информация об операции в списке.
/// </summary>
public sealed class OperationListItem
{
    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    [JsonPropertyName("DocId")]
    public string DocId { get; set; } = "";

    /// <summary>
    /// Идентификатор документа-основания.
    /// </summary>
    [JsonPropertyName("BaseDocId")]
    public string BaseDocId { get; set; } = "";

    /// <summary>
    /// Идентификатор запроса.
    /// </summary>
    [JsonPropertyName("RequestId")]
    public string RequestId { get; set; } = "";

    /// <summary>
    /// Идентификатор терминала.
    /// </summary>
    [JsonPropertyName("TerminalId")]
    public string TerminalId { get; set; } = "";

    /// <summary>
    /// Имя устройства.
    /// </summary>
    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Идентификатор пула.
    /// </summary>
    [JsonPropertyName("PoolId")]
    public string PoolId { get; set; } = "";

    /// <summary>
    /// Дата операции.
    /// </summary>
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Дата создания записи.
    /// </summary>
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата последнего обновления.
    /// </summary>
    [JsonPropertyName("UpdateAt")]
    public DateTime UpdateAt { get; set; }

    /// <summary>
    /// Тип задания.
    /// </summary>
    [JsonPropertyName("TaskType")]
    public int TaskType { get; set; }

    /// <summary>
    /// Наименование типа задания.
    /// </summary>
    [JsonPropertyName("TaskName")]
    public string TaskName { get; set; } = "";

    /// <summary>
    /// Сумма операции.
    /// </summary>
    [JsonPropertyName("Sum")]
    public decimal Sum { get; set; }

    /// <summary>
    /// Номер смены.
    /// </summary>
    [JsonPropertyName("SessionNumber")]
    public int SessionNumber { get; set; }

    /// <summary>
    /// Номер документа в смене.
    /// </summary>
    [JsonPropertyName("DocNumberInShift")]
    public int DocNumberInShift { get; set; }

    /// <summary>
    /// Номер фискального документа.
    /// </summary>
    [JsonPropertyName("DocNumber")]
    public int DocNumber { get; set; }

    /// <summary>
    /// Дата документа по ФН.
    /// </summary>
    [JsonPropertyName("FnDate")]
    public DateTime FnDate { get; set; }

    /// <summary>
    /// Фискальный признак документа.
    /// </summary>
    [JsonPropertyName("FiscalSign")]
    public string FiscalSign { get; set; } = "";

    /// <summary>
    /// Номер фискального накопителя.
    /// </summary>
    [JsonPropertyName("Fn")]
    public string Fn { get; set; } = "";

    /// <summary>
    /// Контакт покупателя.
    /// </summary>
    [JsonPropertyName("ClientContact")]
    public string ClientContact { get; set; } = "";

    /// <summary>
    /// Имя кассира.
    /// </summary>
    [JsonPropertyName("CashierName")]
    public string CashierName { get; set; } = "";

    /// <summary>
    /// Регистрационный номер ККТ.
    /// </summary>
    [JsonPropertyName("RnKKT")]
    public string RnKKT { get; set; } = "";

    /// <summary>
    /// Заводской номер ККТ.
    /// </summary>
    [JsonPropertyName("ZnKKT")]
    public string ZnKKT { get; set; } = "";

    /// <summary>
    /// Код результата (0 — успех).
    /// </summary>
    [JsonPropertyName("ResultCode")]
    public int ResultCode { get; set; }

    /// <summary>
    /// Описание результата.
    /// </summary>
    [JsonPropertyName("ResultDescription")]
    public string ResultDescription { get; set; } = "";

    /// <summary>
    /// Признак успешной обработки операции.
    /// </summary>
    [JsonPropertyName("Processed")]
    public bool Processed { get; set; }
}
