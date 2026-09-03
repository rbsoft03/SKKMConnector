using System.Text.Json.Serialization;

namespace SkkmConnector;

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
    /// Код состояния.
    /// </summary>
    [JsonPropertyName("State")]
    public int State { get; set; }

    /// <summary>
    /// Описание события.
    /// </summary>
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";

    /// <summary>
    /// Состояние документа на этом шаге.
    /// </summary>
    [JsonPropertyName("Document")]
    public CheckDocument? Document { get; set; }
}
