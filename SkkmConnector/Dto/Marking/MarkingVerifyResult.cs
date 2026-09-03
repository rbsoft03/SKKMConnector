namespace SkkmConnector;

/// <summary>
/// Результат проверки кода маркировки.
/// </summary>
public sealed class MarkingVerifyResult
{
    /// <summary>
    /// Код результата проверки.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Описание результата проверки.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Данные проверки кодов маркировки.
    /// </summary>
    public List<CodeMarkInfo> Codes { get; set; } = [];

    /// <summary>
    /// Идентификатор операции проверки.
    /// </summary>
    public string ReqId { get; set; } = "";

    /// <summary>
    /// Временная метка операции проверки.
    /// </summary>
    public long ReqTimestamp { get; set; }

    /// <summary>
    /// Признак офлайн-проверки.
    /// </summary>
    public bool IsCheckedOffline { get; set; }
}
