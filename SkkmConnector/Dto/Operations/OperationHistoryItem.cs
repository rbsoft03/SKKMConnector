using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Элемент истории обработки операции.
/// </summary>
public sealed class OperationHistoryItem
{
    /// <summary>
    /// Время события.
    /// </summary>
    [JsonPropertyName("Time")]
    public DateTime Time { get; set; }

    /// <summary>
    /// Состояние документа в очереди печати.
    /// </summary>
    [JsonPropertyName("State")]
    public DocumentPrintState State { get; set; }

    /// <summary>
    /// Описание события.
    /// </summary>
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";

    /// <summary>
    /// Дополнительная информация о событии.
    /// </summary>
    [JsonPropertyName("Info")]
    public string Info { get; set; } = "";

    /// <summary>
    /// Состояние документа на этом шаге.
    /// </summary>
    [JsonPropertyName("Document")]
    public CheckDocument? Document { get; set; }
}
