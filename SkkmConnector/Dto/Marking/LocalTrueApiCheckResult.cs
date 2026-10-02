namespace RBSoftSkkm;

/// <summary>
/// Результат проверки кодов локальным модулем «Честного знака».
/// </summary>
public sealed class LocalTrueApiCheckResult
{
    /// <summary>
    /// Версия локального модуля.
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Временная метка запроса.
    /// </summary>
    public long ReqTimestamp { get; set; }

    /// <summary>
    /// Идентификатор запроса.
    /// </summary>
    public string? ReqId { get; set; }

    /// <summary>
    /// Экземпляр модуля.
    /// </summary>
    public string? Inst { get; set; }

    /// <summary>
    /// Описание результата.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Сведения по проверенным кодам.
    /// </summary>
    public CodeMarkInfo[] Codes { get; set; } = [];

    /// <summary>
    /// Код результата.
    /// </summary>
    public int Code { get; set; }
}
