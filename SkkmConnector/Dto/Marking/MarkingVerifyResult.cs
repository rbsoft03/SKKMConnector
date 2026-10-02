namespace RBSoftSkkm;

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

    /// <summary>
    /// Версия локального модуля проверки.
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Статус локального модуля проверки.
    /// </summary>
    public LocalModuleStatus Status { get; set; }

    /// <summary>
    /// Адрес сервиса проверки.
    /// </summary>
    public string? ServiceUrl { get; set; }

    /// <summary>
    /// Требуется загрузка данных из ГИС МТ.
    /// </summary>
    public bool RequiresDownload { get; set; }

    /// <summary>
    /// Статус репликации по товарным группам (ключ — товарная группа).
    /// </summary>
    public Dictionary<string, ReplicationItem>? ReplicationStatus { get; set; }

    /// <summary>
    /// Режим работы модуля.
    /// </summary>
    public string? OperationMode { get; set; }

    /// <summary>
    /// Имя модуля.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Метка времени последнего обновления данных.
    /// </summary>
    public long LastUpdate { get; set; }

    /// <summary>
    /// Метка времени последней синхронизации с ГИС МТ.
    /// </summary>
    public long LastSync { get; set; }

    /// <summary>
    /// Экземпляр модуля.
    /// </summary>
    public string? Inst { get; set; }

    /// <summary>
    /// ИНН.
    /// </summary>
    public string? Inn { get; set; }

    /// <summary>
    /// Версия базы данных модуля.
    /// </summary>
    public string? DbVersion { get; set; }

    /// <summary>
    /// Дата последнего обновления (строкой).
    /// </summary>
    public string? LastUpdateDate { get; set; }

    /// <summary>
    /// Дата последней синхронизации (строкой).
    /// </summary>
    public string? LastSyncDate { get; set; }

    /// <summary>
    /// Результаты проверки локальным модулем «Честного знака».
    /// </summary>
    public LocalTrueApiCheckResult[] LocalCheckResults { get; set; } = [];
}
