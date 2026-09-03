using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Информация о задаче устройства.
/// </summary>
public sealed class DeviceTaskInfo
{
    /// <summary>
    /// Тип задания.
    /// </summary>
    [JsonPropertyName("TaskType")]
    public int TaskType { get; set; }

    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    [JsonPropertyName("DocId")]
    public string DocId { get; set; } = "";

    /// <summary>
    /// Дата создания / выполнения операции.
    /// </summary>
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

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
    /// Признак успешного завершения обработки.
    /// </summary>
    [JsonPropertyName("Processed")]
    public bool Processed { get; set; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    [JsonPropertyName("ClientVersion")]
    public string ClientVersion { get; set; } = "";

    /// <summary>
    /// Версия сервера.
    /// </summary>
    [JsonPropertyName("ServerVersion")]
    public string ServerVersion { get; set; } = "";

    /// <summary>
    /// Сведения об устройстве, обработавшем задание.
    /// </summary>
    [JsonPropertyName("DeviceInfo")]
    public Device? DeviceInfo { get; set; }

    /// <summary>
    /// XML-представление документа.
    /// </summary>
    [JsonPropertyName("Xml")]
    public string Xml { get; set; } = "";

    /// <summary>
    /// Сведения о приложении-источнике запроса.
    /// </summary>
    [JsonPropertyName("SenderInfo")]
    public SenderInfo? SenderInfo { get; set; }
}
