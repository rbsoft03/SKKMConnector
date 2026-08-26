using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Статус задания
/// </summary>
public sealed class ResponseTaskStatus
{
    /// <summary>
    /// Имя устройства.
    /// </summary>
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    [JsonPropertyName("DocId")]
    public string? DocId { get; set; }

    /// <summary>
    /// Дата и время постановки задания в обработку.
    /// </summary>
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Статус отправки: 0 — задача новая, в очереди; 1 — отправлена на выполнение; 2 — удачно обработана; −1 — вернулась с ошибкой.
    /// </summary>
    [JsonPropertyName("SentToPrint")]
    public int SentToPrint { get; set; }

    /// <summary>
    /// Позиция задания в очереди на момент запроса. −1 — задание уже покинуло очередь.
    /// </summary>
    [JsonPropertyName("NumberInQueue")]
    public int NumberInQueue { get; set; }

    /// <summary>
    /// Размер очереди.
    /// </summary>
    [JsonPropertyName("QueueSize")]
    public int QueueSize { get; set; }

    /// <summary>
    /// Идентификатор пула. Если устройство не входит в пул — не заполняется.
    /// </summary>
    [JsonPropertyName("PoolId")]
    public string? PoolId { get; set; }

    /// <summary>
    /// Номер смены.
    /// </summary>
    [JsonPropertyName("ShiftNumber")]
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер чека.
    /// </summary>
    [JsonPropertyName("DocNumber")]
    public int DocNumber { get; set; }

    /// <summary>
    /// Тип чека: 0 — текст, 1 — приход, 2 — возврат прихода, … 22 — открытие денежного ящика.
    /// </summary>
    [JsonPropertyName("TaskType")]
    public int TaskType { get; set; }

    /// <summary>
    /// Фискальный признак документа. Заполняется только для фискальных документов.
    /// </summary>
    [JsonPropertyName("FiscalSign")]
    public string? FiscalSign { get; set; }

    /// <summary>
    /// Заголовок документа.
    /// </summary>
    [JsonPropertyName("DocumentHeader")]
    public DocumentHeader? DocumentHeader { get; set; }

    /// <summary>
    /// Код результата обработки задания.
    /// </summary>
    [JsonPropertyName("ResultCode")]
    public int ResultCode { get; set; }

    /// <summary>
    /// Описание результата обработки задания.
    /// </summary>
    [JsonPropertyName("ResultDescription")]
    public string? ResultDescription { get; set; }
}
