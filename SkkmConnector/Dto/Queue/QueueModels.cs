namespace SkkmConnector;

/// <summary>
/// Элемент очереди печати.
/// </summary>
public sealed class QueueItem
{
    public string DocId { get; set; } = "";
    public string DeviceName { get; set; } = "";
    public string PoolId { get; set; } = "";
    public bool SentToPrint { get; set; }
    public DateTime Time { get; set; }
    public DateTime PrintedTime { get; set; }
    public bool Printed { get; set; }
    public decimal Sum { get; set; }
    public string ErrorDescription { get; set; } = "";
    public int Session { get; set; }
    public int DocNumber { get; set; }
}

/// <summary>
/// Состояние задания в очереди.
/// </summary>
public sealed class QueueTaskState
{
    public string DeviceName { get; set; } = "";
    public string DocId { get; set; } = "";
    public int DocState { get; set; }
    public int QueueState { get; set; }
    public int ResultCode { get; set; }
    public string ResultDescription { get; set; } = "";
    public int NumberInQueue { get; set; }
    public DateTime Date { get; set; }
    public string FiscalSign { get; set; } = "";
    public string PrintStatusDescription { get; set; } = "";
    public DocumentHistoryItem[] History { get; set; } = [];
}

/// <summary>
/// Запись истории обработки документа.
/// </summary>
public sealed class DocumentHistoryItem
{
    public DateTime Time { get; set; }
    public int State { get; set; }
    public string Description { get; set; } = "";
    public string Info { get; set; } = "";
}
