namespace SkkmConnector;

/// <summary>
/// Состояние задания в очереди.
/// </summary>
public sealed class QueueTaskState
{
    /// <summary>
    /// Название устройства.
    /// </summary>
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    public string DocId { get; set; } = "";

    /// <summary>
    /// Код состояния документа.
    /// </summary>
    public int DocState { get; set; }

    /// <summary>
    /// Код состояния очереди.
    /// </summary>
    public int QueueState { get; set; }

    /// <summary>
    /// Код результата.
    /// </summary>
    public int ResultCode { get; set; }

    /// <summary>
    /// Описание результата.
    /// </summary>
    public string ResultDescription { get; set; } = "";

    /// <summary>
    /// Позиция задания в очереди на момент запроса.
    /// </summary>
    public int NumberInQueue { get; set; }

    /// <summary>
    /// Дата и время последнего изменения статуса.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Фискальный признак документа (для успешно обработанных фискальных заданий).
    /// </summary>
    public string FiscalSign { get; set; } = "";

    /// <summary>
    /// Описание текущего этапа обработки задания.
    /// </summary>
    public string PrintStatusDescription { get; set; } = "";

    /// <summary>
    /// История обработки задания.
    /// </summary>
    public DocumentHistoryItem[] History { get; set; } = [];
}
