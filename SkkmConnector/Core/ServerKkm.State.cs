using System.Text.Json;

namespace SkkmConnector;

// Свойства-результаты последнего вызова.
public sealed partial class ServerKkm
{

    // Результат последнего вызова

    /// <summary>
    /// Успех последнего вызова. Ответ каждого метода.
    /// </summary>
    public bool Ok { get; private set; }

    /// <summary>
    /// Код ошибки сервера. Ответ. 0 - нет ошибки.
    /// </summary>
    public int ErrorCode { get; private set; }

    /// <summary>
    /// Текст ошибки сервера. Ответ. При успехе «OK» или пусто.
    /// </summary>
    public string ErrorDescription { get; private set; } = "";

    /// <summary>
    /// Поле Result последнего ответа сервера (документы, статусы задач).
    /// </summary>
    public JsonElement LastResult { get; private set; }

    /// <summary>
    /// Фискальные поля ответа (чек, коррекция, смена): ФП, номер ФД, смена, DocId.
    /// Ответ. Из него обновляются <see cref="FiscalSign"/>, <see cref="CheckNumber"/>, <see cref="ShiftNumber"/>.
    /// </summary>
    public FiscalResult? FiscalResult { get; private set; }

    /// <summary>
    /// Список устройств после <see cref="GetDeviceList"/>.
    /// </summary>
    public DeviceListResponse[] Devices { get; private set; } = [];

    /// <summary>
    /// Данные кассы после <see cref="Connect"/>.
    /// </summary>
    public DataKkt? Kkt { get; private set; }

    /// <summary>
    /// Состояние ККМ после <see cref="GetStatus"/> / <see cref="Connect"/>.
    /// </summary>
    public KktStatus? Status { get; private set; }

    /// <summary>
    /// Статус смены после <see cref="GetShiftStatus"/>.
    /// </summary>
    public ResponseCurrentStatus? ShiftStatus { get; private set; }

    /// <summary>
    /// Итоги смены после <see cref="GetTotals"/>.
    /// </summary>
    public ResShiftTotal? ShiftTotals { get; private set; }

    /// <summary>
    /// Остаток наличных после <see cref="GetCash"/>.
    /// </summary>
    public decimal CashBalance { get; private set; }

    /// <summary>
    /// Список картинок после <see cref="GetPictureList"/>.
    /// </summary>
    public List<Picture> Pictures { get; private set; } = [];

    /// <summary>
    /// Ширина строки чека в символах.
    /// </summary>
    public int LineLength { get; private set; }

    /// <summary>
    /// Ширина печатной области в пикселях после <see cref="GetLineLength"/>.
    /// </summary>
    public int LineLengthPixels { get; private set; }

    /// <summary>
    /// Необнуляемая сумма продаж после <see cref="GetOverAll"/>.
    /// </summary>
    public decimal NonZeroSum { get; private set; }

    /// <summary>
    /// Дата последней операции после <see cref="GetLastOperation"/>.
    /// </summary>
    public DateTime LastOperationDate { get; private set; }

    /// <summary>
    /// Тип последней операции после <see cref="GetLastOperation"/>.
    /// </summary>
    public int LastOperationType { get; private set; }

    /// <summary>
    /// Номер документа последней операции.
    /// </summary>
    public int LastOperationDocNumber { get; private set; }

    /// <summary>
    /// Номер смены последней операции.
    /// </summary>
    public int LastOperationShiftNumber { get; private set; }

    /// <summary>
    /// Сумма документа последней операции.
    /// </summary>
    public decimal LastOperationSum { get; private set; }

    /// <summary>
    /// Результат локальной проверки КМ после <see cref="RequestKM"/>.
    /// </summary>
    public RequestKmResult? MarkingCheck { get; private set; }

    /// <summary>
    /// Результат проверки КМ в ОИСМ после <see cref="GetProcessingKMResult"/>.
    /// </summary>
    public ProcessingKmResult? MarkingProcessing { get; private set; }

    /// <summary>
    /// Документ после GetCheck / GetCorrection120 / GetCorrection105 / GetReportX / GetReportZ /
    /// GetOpenShift / GetReportSettlement / GetCashIn / GetCashOut.
    /// </summary>
    public CheckDocument? Check { get; private set; }

    /// <summary>
    /// Список документов после GetChecksByShift / GetCorrection120List / GetCorrection105List / GetCashInList.
    /// </summary>
    public CheckDocument[] Checks { get; private set; } = [];

    /// <summary>
    /// Статус задания после <see cref="GetTaskStatus"/>.
    /// </summary>
    public ResponseTaskStatus? TaskStatus { get; private set; }

    /// <summary>
    /// Печатная форма после <see cref="GetPrintForm"/>.
    /// </summary>
    public PrintFormLine[] PrintForm { get; private set; } = [];

    /// <summary>
    /// Список отчётов после GetShiftList / GetOpenShiftList / GetReportXList / GetReportSettlementList.
    /// </summary>
    public ShiftListItem[] Shifts { get; private set; } = [];
}
