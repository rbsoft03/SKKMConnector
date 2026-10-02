namespace RBSoftSkkm;

/// <summary>
/// Плоское представление одного результата локальной проверки маркировки
/// (<see cref="SkkmConnector.MarkingLocalCheckResults"/>). Обёртка только для чтения поверх
/// <see cref="LocalTrueApiCheckResult"/>. Коды (<see cref="Codes"/>, элемент <see cref="CodeMarkInfo"/>) уже плоские —
/// перебираются по индексу.
/// </summary>
public sealed class LocalCheckResult
{
    private readonly LocalTrueApiCheckResult _r;

    public LocalCheckResult(LocalTrueApiCheckResult source) => _r = source;

    /// <summary> Версия ответа. </summary>
    public string? Version => _r.Version;
    /// <summary> Метка времени запроса. </summary>
    public long ReqTimestamp => _r.ReqTimestamp;
    /// <summary> Идентификатор запроса. </summary>
    public string? ReqId => _r.ReqId;
    /// <summary> Идентификатор инсталляции. </summary>
    public string? Inst => _r.Inst;
    /// <summary> Описание результата. </summary>
    public string? Description => _r.Description;
    /// <summary> Код результата. </summary>
    public int Code => _r.Code;

    /// <summary> Количество проверенных кодов. </summary>
    public int CodesCount => _r.Codes?.Length ?? 0;
    /// <summary> Проверенные коды маркировки (перебираются по индексу). </summary>
    public IReadOnlyList<CodeMarkInfo> Codes => _r.Codes ?? [];
}
