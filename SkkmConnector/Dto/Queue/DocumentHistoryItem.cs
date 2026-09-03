namespace SkkmConnector;

/// <summary>
/// Запись истории обработки документа в очереди.
/// </summary>
public sealed class DocumentHistoryItem
{
    /// <summary>
    /// Время события.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Код состояния.
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// Описание события.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Дополнительная информация о событии.
    /// </summary>
    public string Info { get; set; } = "";
}
