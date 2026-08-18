using System.Text.Json;

namespace SkkmConnector;

// Свойства-результаты последнего вызова и отладочные данные обмена.
public sealed partial class ServerKkm
{

    // Результат последнего вызова

    /// <summary>
    /// Успех последнего вызова.
    /// </summary>
    public bool Ok { get; private set; }

    /// <summary>
    /// Код ошибки последнего вызова.
    /// </summary>
    public int ErrorCode { get; private set; }

    /// <summary>
    /// Описание ошибки последнего вызова.
    /// </summary>
    public string ErrorDescription { get; private set; } = "";

    /// <summary>
    /// Отладочно: HTTP-статус последнего обмена.
    /// </summary>
    public int LastStatusCode { get; private set; }

    /// <summary>
    /// Отладочно: длительность последнего обмена, мс.
    /// </summary>
    public long LastDurationMs { get; private set; }

    /// <summary>
    /// Отладочно: краткое описание последнего запроса.
    /// </summary>
    public string? LastRequestInfo { get; private set; }

    /// <summary>
    /// Отладочно: тело последнего запроса.
    /// </summary>
    public string? LastRequestBody { get; private set; }

    /// <summary>
    /// Отладочно: тело последнего ответа.
    /// </summary>
    public string? LastResponseBody { get; private set; }

    /// <summary>
    /// Отладочно: заголовки последнего запроса.
    /// </summary>
    public IReadOnlyList<KeyValuePair<string, string>> LastRequestHeaders { get; private set; }
        = Array.Empty<KeyValuePair<string, string>>();

    /// <summary>
    /// Поле Result последнего ответа (если это JSON-объект).
    /// </summary>
    public JsonElement LastResult { get; private set; }

    /// <summary>
    /// Результат последней фискальной операции (печать чека, коррекции, смена и т.п.).
    /// Заполняется автоматически из ответа сервера; из него же обновляются <see cref="FiscalSign"/>,
    /// <see cref="CheckNumber"/> и <see cref="ShiftNumber"/>.
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
    /// Ширина строки чека в символах (GetLineLength / GetStatus / Connect).
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
}
