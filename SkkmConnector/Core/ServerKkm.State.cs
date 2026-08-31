using System.Text.Json;

namespace SkkmConnector;

public sealed partial class ServerKkm
{
    /// <summary>Успех последнего вызова.</summary>
    public bool Ok { get; private set; }

    /// <summary>Код ошибки сервера. 0 — нет ошибки.</summary>
    public int ErrorCode { get; private set; }

    /// <summary>Текст ошибки сервера.</summary>
    public string ErrorDescription { get; private set; } = "";

    /// <summary>Поле Result последнего ответа сервера.</summary>
    public JsonElement LastResult { get; private set; }

    /// <summary>Фискальный блок ответа.</summary>
    public FiscalResult? FiscalResult { get; private set; }

    /// <summary>Список устройств.</summary>
    public DeviceListResponse[] Devices { get; private set; } = [];

    /// <summary>Данные кассы.</summary>
    public DataKkt? Kkt { get; private set; }

    /// <summary>Состояние ККМ.</summary>
    public KktStatus? Status { get; private set; }

    /// <summary>Статус смены.</summary>
    public ResponseCurrentStatus? ShiftStatus { get; private set; }

    /// <summary>Итоги смены.</summary>
    public ResShiftTotal? ShiftTotals { get; private set; }

    /// <summary>Остаток наличных.</summary>
    public decimal CashBalance { get; private set; }

    /// <summary>Список картинок.</summary>
    public List<Picture> Pictures { get; private set; } = [];

    /// <summary>Ширина строки чека в символах.</summary>
    public int LineLength { get; private set; }

    /// <summary>Ширина печатной области в пикселях.</summary>
    public int LineLengthPixels { get; private set; }

    /// <summary>Необнуляемая сумма продаж.</summary>
    public decimal NonZeroSum { get; private set; }

    /// <summary>Результат локальной проверки КМ.</summary>
    public RequestKmResult? MarkingCheck { get; private set; }

    /// <summary>Результат проверки КМ в ОИСМ.</summary>
    public ProcessingKmResult? MarkingProcessing { get; private set; }

    /// <summary>Документ.</summary>
    public CheckDocument? Check { get; private set; }

    /// <summary>Список документов.</summary>
    public CheckDocument[] Checks { get; private set; } = [];

    /// <summary>Статус задания.</summary>
    public ResponseTaskStatus? TaskStatus { get; private set; }

    /// <summary>Печатная форма.</summary>
    public PrintFormLine[] PrintForm { get; private set; } = [];

    /// <summary>Список отчётов.</summary>
    public ShiftListItem[] Shifts { get; private set; } = [];

    /// <summary>Версия сервера.</summary>
    public string ServerVersion { get; private set; } = "";

    /// <summary>Токен пользователя.</summary>
    public UserToken? UserToken { get; private set; }

    /// <summary>Список пользователей.</summary>
    public ServiceUser[] Users { get; private set; } = [];

    /// <summary>Настройки службы.</summary>
    public ServiceSettings? ServiceSettingsResult { get; private set; }

    /// <summary>Список пулов.</summary>
    public string[] Pools { get; private set; } = [];

    /// <summary>Очередь печати.</summary>
    public QueueItem[] Queue { get; private set; } = [];

    /// <summary>Состояние задания очереди.</summary>
    public QueueTaskState? QueueTask { get; private set; }

    /// <summary>Операция.</summary>
    public DeviceTaskInfo? Operation { get; private set; }

    /// <summary>История операции.</summary>
    public OperationHistoryItem[] OperationHistory { get; private set; } = [];

    /// <summary>TLV операции.</summary>
    public string OperationTlv { get; private set; } = "";

    /// <summary>Коды маркировки операции.</summary>
    public OperationKmRow[] OperationKm { get; private set; } = [];

    /// <summary>Связанные операции.</summary>
    public DeviceTaskInfo[] RelatedOperations { get; private set; } = [];

    /// <summary>Список операций.</summary>
    public OperationListItem[] Operations { get; private set; } = [];

    /// <summary>Шаблон печати.</summary>
    public PrintTemplate? PrintTemplate { get; private set; }

    /// <summary>Список шаблонов печати.</summary>
    public PrintTemplate[] Templates { get; private set; } = [];

    /// <summary>Шаблон чека.</summary>
    public CheckTemplate? CheckTemplate { get; private set; }

    /// <summary>Список шаблонов чека.</summary>
    public CheckTemplateListItem[] CheckTemplates { get; private set; } = [];

    /// <summary>Документ фискализации.</summary>
    public FiscalizationDocument? FiscalizationDocument { get; private set; }

    /// <summary>Список фискализаций.</summary>
    public FiscalizationDocument[] Fiscalizations { get; private set; } = [];

    /// <summary>Результат проверки маркировки.</summary>
    public MarkingVerifyResult? MarkingVerify { get; private set; }

    /// <summary>Картинка в Base64.</summary>
    public string PictureBase64Result { get; private set; } = "";
}
