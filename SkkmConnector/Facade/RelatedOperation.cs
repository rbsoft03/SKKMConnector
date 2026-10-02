namespace RBSoftSkkm;

/// <summary>
/// Плоское представление одной связанной операции (<see cref="SkkmConnector.RelatedOperations"/>).
/// Обёртка только для чтения поверх <see cref="DeviceTaskInfo"/>: разворачивает под-объекты
/// DeviceInfo/SenderInfo в плоские свойства. Имена совпадают с одиночным фасадом (без префикса Operation).
/// </summary>
public sealed class RelatedOperation
{
    private readonly DeviceTaskInfo _o;

    public RelatedOperation(DeviceTaskInfo source) => _o = source;

    // ─── Прямые поля операции ───
    /// <summary> Идентификатор документа операции (docId). </summary>
    public string? DocId => _o.DocId;
    /// <summary> Тип операции/документа. </summary>
    public CheckType TaskType => _o.TaskType;
    /// <summary> Дата/время операции. </summary>
    public DateTime Date => _o.Date;
    /// <summary> Базовый документ (для связанных операций). </summary>
    public string? BaseDocId => _o.BaseDocId;
    /// <summary> Идентификатор запроса. </summary>
    public string? RequestId => _o.RequestId;
    /// <summary> Имя кассы. </summary>
    public string? DeviceName => _o.DeviceName;
    /// <summary> Идентификатор терминала. </summary>
    public string? TerminalId => _o.TerminalId;
    /// <summary> Идентификатор пула. </summary>
    public string? PoolId => _o.PoolId;
    /// <summary> Код результата. </summary>
    public int ResultCode => _o.ResultCode;
    /// <summary> Описание результата. </summary>
    public string? ResultDescription => _o.ResultDescription;
    /// <summary> Операция обработана. </summary>
    public bool IsProcessed => _o.Processed;
    /// <summary> Версия клиента. </summary>
    public string? ClientVersion => _o.ClientVersion;
    /// <summary> Версия сервера. </summary>
    public string? ServerVersion => _o.ServerVersion;
    /// <summary> XML документа операции. </summary>
    public string Xml => _o.Xml;

    // ─── DeviceInfo ───
    /// <summary> Модель устройства. </summary>
    public string? DeviceModel => _o.DeviceInfo?.Model;
    /// <summary> Наименование модели устройства. </summary>
    public string? DeviceModelName => _o.DeviceInfo?.ModelName;
    /// <summary> Заводской номер устройства. </summary>
    public string? DeviceSerialNumber => _o.DeviceInfo?.SerialNumber;
    /// <summary> Версия прошивки устройства. </summary>
    public string? DeviceFirmwareVersion => _o.DeviceInfo?.FirmwareVersion;
    /// <summary> Версия конфигурации прошивки устройства. </summary>
    public string? DeviceConfigurationVersion => _o.DeviceInfo?.ConfigurationVersion;
    /// <summary> Версия ФФД устройства. </summary>
    public string? DeviceFfdVersion => _o.DeviceInfo?.FfdVersion;
    /// <summary> Версия ФФД ФН. </summary>
    public string? DeviceFnFfdVersion => _o.DeviceInfo?.FnFfdVersion;
    /// <summary> Класс устройства. </summary>
    public DeviceClass? DeviceClass => _o.DeviceInfo?.DeviceClass;
    /// <summary> Часовая зона устройства. </summary>
    public int DeviceTimeZone => _o.DeviceInfo?.TimeZone ?? 0;
    /// <summary> Фискальный режим устройства. </summary>
    public bool DeviceIsFiscal => _o.DeviceInfo?.IsFiscal ?? false;

    // ─── SenderInfo ───
    /// <summary> Приложение-отправитель. </summary>
    public string? SenderAppName => _o.SenderInfo?.AppName;
    /// <summary> Версия приложения-отправителя. </summary>
    public string? SenderAppVersion => _o.SenderInfo?.AppVersion;
}
