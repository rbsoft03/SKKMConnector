using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Краткая информация об операции.
/// </summary>
public sealed class OperationListItem
{
    [JsonPropertyName("DocId")]
    public string DocId { get; set; } = "";

    [JsonPropertyName("BaseDocId")]
    public string BaseDocId { get; set; } = "";

    [JsonPropertyName("RequestId")]
    public string RequestId { get; set; } = "";

    [JsonPropertyName("TerminalId")]
    public string TerminalId { get; set; } = "";

    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; } = "";

    [JsonPropertyName("PoolId")]
    public string PoolId { get; set; } = "";

    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("UpdateAt")]
    public DateTime UpdateAt { get; set; }

    [JsonPropertyName("TaskType")]
    public int TaskType { get; set; }

    [JsonPropertyName("TaskName")]
    public string TaskName { get; set; } = "";

    [JsonPropertyName("Sum")]
    public decimal Sum { get; set; }

    [JsonPropertyName("SessionNumber")]
    public int SessionNumber { get; set; }

    [JsonPropertyName("DocNumberInShift")]
    public int DocNumberInShift { get; set; }

    [JsonPropertyName("DocNumber")]
    public int DocNumber { get; set; }

    [JsonPropertyName("FnDate")]
    public DateTime FnDate { get; set; }

    [JsonPropertyName("FiscalSign")]
    public string FiscalSign { get; set; } = "";

    [JsonPropertyName("Fn")]
    public string Fn { get; set; } = "";

    [JsonPropertyName("ClientContact")]
    public string ClientContact { get; set; } = "";

    [JsonPropertyName("CashierName")]
    public string CashierName { get; set; } = "";

    [JsonPropertyName("RnKKT")]
    public string RnKKT { get; set; } = "";

    [JsonPropertyName("ZnKKT")]
    public string ZnKKT { get; set; } = "";

    [JsonPropertyName("ResultCode")]
    public int ResultCode { get; set; }

    [JsonPropertyName("ResultDescription")]
    public string ResultDescription { get; set; } = "";

    [JsonPropertyName("Processed")]
    public bool Processed { get; set; }
}

/// <summary>
/// Элемент истории операции.
/// </summary>
public sealed class OperationHistoryItem
{
    [JsonPropertyName("Time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("State")]
    public int State { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("Document")]
    public CheckDocument? Document { get; set; }
}

/// <summary>
/// Задача устройства.
/// </summary>
public sealed class DeviceTaskInfo
{
    [JsonPropertyName("TaskType")]
    public int TaskType { get; set; }

    [JsonPropertyName("DocId")]
    public string DocId { get; set; } = "";

    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("BaseDocId")]
    public string BaseDocId { get; set; } = "";

    [JsonPropertyName("RequestId")]
    public string RequestId { get; set; } = "";

    [JsonPropertyName("TerminalId")]
    public string TerminalId { get; set; } = "";

    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; } = "";

    [JsonPropertyName("PoolId")]
    public string PoolId { get; set; } = "";

    [JsonPropertyName("ResultCode")]
    public int ResultCode { get; set; }

    [JsonPropertyName("ResultDescription")]
    public string ResultDescription { get; set; } = "";

    [JsonPropertyName("Processed")]
    public bool Processed { get; set; }

    [JsonPropertyName("ClientVersion")]
    public string ClientVersion { get; set; } = "";

    [JsonPropertyName("ServerVersion")]
    public string ServerVersion { get; set; } = "";

    [JsonPropertyName("DeviceInfo")]
    public Device? DeviceInfo { get; set; }

    [JsonPropertyName("Xml")]
    public string Xml { get; set; } = "";

    [JsonPropertyName("SenderInfo")]
    public SenderInfo? SenderInfo { get; set; }
}

/// <summary>
/// Источник запроса операции.
/// </summary>
public sealed class SenderInfo
{
    [JsonPropertyName("AppName")]
    public string AppName { get; set; } = "";

    [JsonPropertyName("AppVersion")]
    public string AppVersion { get; set; } = "";
}

/// <summary>
/// Строка журнала кодов маркировки операции.
/// </summary>
public sealed class OperationKmRow
{
    [JsonPropertyName("Cis")]
    public string Cis { get; set; } = "";

    [JsonPropertyName("CheckedAt")]
    public DateTime CheckedAt { get; set; }

    [JsonPropertyName("PrintView")]
    public string PrintView { get; set; } = "";

    [JsonPropertyName("Message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("CheckStatus")]
    public int CheckStatus { get; set; }

    [JsonPropertyName("PositionName")]
    public string PositionName { get; set; } = "";

    [JsonPropertyName("DocIds")]
    public string[] DocIds { get; set; } = [];

    [JsonPropertyName("SalePrice")]
    public long SalePrice { get; set; }

    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; } = "";

    [JsonPropertyName("MarkId")]
    public string MarkId { get; set; } = "";

    [JsonPropertyName("KmVerificationMethod")]
    public int KmVerificationMethod { get; set; }

    [JsonPropertyName("KmCheckInitiator")]
    public int KmCheckInitiator { get; set; }
}
