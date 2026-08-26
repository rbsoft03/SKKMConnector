using SkkmConnector.Internal;
using System.Text.Json;

namespace SkkmConnector;

public sealed partial class ServerKkm
{
    /// <summary>
    /// Очистка входных данных перед новым запросом и результаты прошлого вызова.
    /// </summary>
    public void NewRequest()
    {
        PaymentType = (int)CheckType.Sale;
        TaxVariant = (int)TaxSystem.ОСН;
        Electronically = false;
        OperationOnline = false;
        TimeZone = null;
        TextBefore = "";
        TextAfter = "";
        SaleLocation = "";
        SaleAddress = "";
        SenderEmail = "";
        AdditionalAttribute = "";
        IndustryAttribute = null;
        UserAttribute = null;
        OperationalAttribute = null;
        ElectronicPayments.Clear();
        AgentSign = null;
        Agent = null;
        Vendor = null;
        Customer = null;
        Payments = new Payments();
        Positions.Clear();
        CorrectionData = null;
        Correction105Taxes = null;
        CashAmount = 0;
        TextForPrint = "";
        PictureName = "";
        PictureBase64 = "";
        PictureAlignment = 2;
        MarkingCode = "";
        PlannedStatus = 1;
        MarkingQuantity = 1;
        MeasureOfQuantity = 0;
        FractionalQuantityNumerator = 0;
        FractionalQuantityDenominator = 0;
        NotSendToServer = false;
        WaitForResult = false;
        RequestKmGuid = "";
        ConfirmationType = 0;
        ShiftsFrom = DateTime.Today.AddDays(-7);
        ShiftsTo = DateTime.Today;
        DocumentId = "";
        FiscalSign = "";
        ShiftNumber = 0;
        CheckNumber = 0;
        LastOperationDate = default;
        LastOperationType = 0;
        LastOperationDocNumber = 0;
        LastOperationShiftNumber = 0;
        LastOperationSum = 0;
        ShiftTotals = null;
        NonZeroSum = 0;
        Ok = false;
        ErrorCode = 0;
        ErrorDescription = "";
        LastResult = default;
        FiscalResult = null;
        MarkingCheck = null;
        MarkingProcessing = null;
        Check = null;
        Checks = [];
        TaskStatus = null;
        PrintForm = [];
        Shifts = [];
    }

    /// <summary>
    /// Проверка доступности сервера ККМ. Не требует передачи ключа доступа (api_key)
    /// </summary>
    public async Task Ping()
    {
        await Get("ping");
    }

    /// <summary>
    /// Получение списка зарегистрированных ККТ
    /// </summary>
    public async Task GetDeviceList()
    {
        await Get("kkt/list");
        Devices = ReadResult<DeviceListResponse[]>() ?? [];
    }

    /// <summary>
    /// Получение подробной информации об устройстве ККТ
    /// </summary>
    public async Task Connect()
    {
        await Get($"kkt?{DeviceQuery}");
        Kkt = ReadResult<DataKkt>();
        if (Kkt?.Status != null)
            Status = Kkt.Status;
        if (Kkt?.Device != null)
            LineLength = Kkt.Device.LineLength;
        else if (Kkt?.Status != null)
            LineLength = Kkt.Status.LineLength;
        if (!string.IsNullOrWhiteSpace(Kkt?.Fn?.SaleLocation))
            SaleLocation = Kkt!.Fn!.SaleLocation!;
    }

    /// <summary>
    /// Получение расширенного статуса ККТ
    /// </summary>
    public async Task GetStatus()
    {
        await Get($"kkt/status?{DeviceQuery}");
        Status = ReadResult<KktStatus>();
        if (Status == null)
            return;
        LineLength = Status.LineLength;
        ShiftNumber = Status.ShiftNumber;
        CheckNumber = Status.DocNumber;
    }

    /// <summary>
    /// Получение краткого статуса смены и очереди ОФД
    /// </summary>
    public async Task GetShiftStatus()
    {
        await Get($"kkt/shift/status?{DeviceQuery}");
        ShiftStatus = ReadResult<ResponseCurrentStatus>();
        if (ShiftStatus == null)
            return;
        ShiftNumber = ShiftStatus.ShiftNumber;
        CheckNumber = ShiftStatus.CheckNumber;
    }

    /// <summary>
    /// Открытие кассовой смены
    /// </summary>
    public async Task OpenShift()
    {
        await Post("shift/open", CheckBase());
    }

    /// <summary>
    /// Закрытие кассовой смены (Z-отчёт)
    /// </summary>
    public async Task CloseShift()
    {
        await Post("shift/z", CheckBase());
    }

    /// <summary>
    /// Формирование X-отчёта (без закрытия смены)
    /// </summary>
    public async Task ReportX()
    {
        await Post("shift/x", CheckBase());
    }

    /// <summary>
    /// Формирование отчёта о текущем состоянии расчётов
    /// </summary>
    public async Task ReportSettlement()
    {
        await Post("report/settlement", CheckBase());
    }

    /// <summary>
    /// Возвращает X-отчёт по идентификатору документа (docId)
    /// </summary>
    public async Task GetReportX()
    {
        await GetDocumentById("shift/x");
    }

    /// <summary>
    /// Возвращает Z-отчёт по идентификатору документа (docId)
    /// </summary>
    public async Task GetReportZ()
    {
        await GetDocumentById("shift/z");
    }

    /// <summary>
    /// Возвращает результат открытия смены по идентификатору документа (docId)
    /// </summary>
    public async Task GetOpenShift()
    {
        await GetDocumentById("shift/open");
    }

    /// <summary>
    /// Возвращает отчёт о состоянии расчётов по идентификатору документа (docId)
    /// </summary>
    public async Task GetReportSettlement()
    {
        await GetDocumentById("report/settlement");
    }

    /// <summary>
    /// Получение необнуляемых (накопительных) счётчиков ККТ
    /// </summary>
    public async Task GetOverAll()
    {
        await Get($"kkt/counters/overall?{DeviceQuery}");
        NonZeroSum = ReadResult<OverallTotals>()?.Counters?.Sales?.Sum ?? 0;
    }

    /// <summary>
    /// Получение максимальной ширины строки чека устройства
    /// </summary>
    public async Task GetLineLength()
    {
        await Get($"kkt/lineLength?{DeviceQuery}");
        var length = ReadResult<LineLengthV2>();
        if (length == null)
            return;
        LineLength = length.LineLength;
        LineLengthPixels = length.LineLengthPixels;
    }

    /// <summary>
    /// Получение последней операции из базы сервера
    /// </summary>
    public async Task GetLastOperation()
    {
        await Get("operation/last");
        var operation = ReadResult<LastOperationDto>();
        if (operation == null)
            return;
        LastOperationDate = operation.Date;
        LastOperationType = operation.TaskType;
        LastOperationDocNumber = operation.DocNumber;
        LastOperationShiftNumber = operation.ShiftNumber;
        LastOperationSum = operation.Sum;
    }

    /// <summary>
    /// Получение счётчиков за смену
    /// </summary>
    public async Task GetTotals()
    {
        await Get($"kkt/counters/shift?{DeviceQuery}");
        ShiftTotals = ReadResult<ResShiftTotal>();
    }

    /// <summary>
    /// Получение списка Z-отчётов за период
    /// </summary>
    public async Task GetShiftList()
    {
        await GetReportList("shift/z/list");
    }

    /// <summary>
    /// Получение списка открытий смен за период
    /// </summary>
    public async Task GetOpenShiftList()
    {
        await GetReportList("shift/open/list");
    }

    /// <summary>
    /// Получение списка X-отчётов за период
    /// </summary>
    public async Task GetReportXList()
    {
        await GetReportList("shift/x/list");
    }

    /// <summary>
    /// Список отчётов о состоянии расчётов по устройству за период
    /// </summary>
    public async Task GetReportSettlementList()
    {
        await GetReportList("report/settlement/list");
    }

    /// <summary>
    /// Печать кассового чека
    /// </summary>
    public async Task PrintCheck()
    {
        await Post("check", CheckBody());
    }

    /// <summary>
    /// Асинхронно поставить фискальный чек в очередь печати
    /// </summary>
    public async Task PrintCheckAsync()
    {
        await Post("check/async", CheckBody());
    }

    /// <summary>
    /// Печать чека коррекции для ФФД 1.2
    /// </summary>
    public async Task PrintCheckCorrection120()
    {
        await Post("correction120", Correction120Body());
    }

    /// <summary>
    /// Асинхронно печатает чек коррекции для ФФД 1.2
    /// </summary>
    public async Task PrintCheckCorrection120Async()
    {
        await Post("correction120/async", Correction120Body());
    }

    /// <summary>
    /// Печать чека коррекции для ФФД 1.0.5
    /// </summary>
    public async Task PrintCheckCorrection105()
    {
        await Post("correction105", Correction105Body());
    }

    /// <summary>
    /// Асинхронно ставит печать чека коррекции для ФФД 1.0.5.
    /// </summary>
    public async Task PrintCheckCorrection105Async()
    {
        await Post("correction105/async", Correction105Body());
    }

    /// <summary>
    /// Возвращает чек коррекции ФФД 1.2 по идентификатору документа (docId)
    /// </summary>
    public async Task GetCorrection120()
    {
        await GetDocumentById("correction120");
    }

    /// <summary>
    /// Получение списка чеков коррекции ФФД 1.2
    /// </summary>
    public async Task GetCorrection120List()
    {
        await GetCheckList("correction120/list");
    }

    /// <summary>
    /// Возвращает чек коррекции ФФД 1.0.5 по идентификатору документа (docId)
    /// </summary>
    public async Task GetCorrection105()
    {
        await GetDocumentById("correction105");
    }

    /// <summary>
    /// Получение списка чеков коррекции ФФД 1.0.5
    /// </summary>
    public async Task GetCorrection105List()
    {
        await GetCheckList("correction105/list");
    }

    /// <summary>
    /// Получение списка чеков за смену
    /// </summary>
    public async Task GetChecksByShift()
    {
        await Get($"check/list?{DeviceQuery}&shift={ShiftNumber}");
        Checks = ReadResult<CheckDocument[]>() ?? [];
    }

    /// <summary>
    /// Возвращает статус выполнения задания по идентификатору документа (docId)
    /// </summary>
    public async Task GetTaskStatus()
    {
        await Get($"task/status?{IdQuery}");
        TaskStatus = ReadResult<ResponseTaskStatus>();
        if (TaskStatus == null)
            return;
        if (!string.IsNullOrEmpty(TaskStatus.FiscalSign))
            FiscalSign = TaskStatus.FiscalSign!;
        if (TaskStatus.DocNumber > 0)
            CheckNumber = TaskStatus.DocNumber;
        if (TaskStatus.ShiftNumber > 0)
            ShiftNumber = TaskStatus.ShiftNumber;
        if (!string.IsNullOrEmpty(TaskStatus.DocId))
            DocumentId = TaskStatus.DocId!;
    }

    /// <summary>
    /// Возвращает результат операции по идентификатору документа (docId)
    /// </summary>
    public async Task GetCheck()
    {
        await GetDocumentById("check");
    }

    /// <summary>
    /// Получение фискального признака (ФП) по номеру фискального документа (ФД)
    /// </summary>
    public async Task GetFiscalSign()
    {
        await Get($"check/fiscalSign?docNumber={CheckNumber}&{DeviceQuery}");
        if (Ok && LastResult.ValueKind == JsonValueKind.String)
            FiscalSign = LastResult.GetString() ?? "";
    }

    /// <summary>
    /// Печать копии чека
    /// </summary>
    public async Task PrintCheckCopy()
    {
        if (string.IsNullOrWhiteSpace(DocumentId))
            await Post($"check/copy/last?{DeviceQuery}");
        else
            await Post("check/copy", new CheckbaseParameters { DeviceName = DeviceName, DocId = DocumentId });
    }

    /// <summary>
    /// Возвращает печатную форму документа по его идентификатору (docId)
    /// </summary>
    public async Task GetPrintForm()
    {
        await Get($"task/form?{IdQuery}");
        PrintForm = ReadResult<PrintFormLine[]>() ?? [];
    }

    /// <summary>
    /// Регистрация операции внесения наличных в денежный ящик
    /// </summary>
    public async Task CashIn()
    {
        await Post("cashin", CashBody());
    }

    /// <summary>
    /// Регистрация операции выемки наличных из денежного ящика
    /// </summary>
    public async Task CashOut()
    {
        await Post("cashout", CashBody());
    }

    /// <summary>
    /// Открытие денежного ящика
    /// </summary>
    public async Task OpenCashdrawer()
    {
        await Post("cash/open", CheckBase());
    }

    /// <summary>
    /// Получение остатка наличных в денежном ящике
    /// </summary>
    public async Task GetCash()
    {
        await Get($"cash?{DeviceQuery}");
        CashBalance = ReadResult<CashSum>()?.Sum ?? 0;
    }

    /// <summary>
    /// Возвращает результат операции внесения наличных по идентификатору операции (docId)
    /// </summary>
    public async Task GetCashIn()
    {
        await GetDocumentById("cashin");
    }

    /// <summary>
    /// Получение списка операций внесения наличных по имени устройства
    /// </summary>
    public async Task GetCashInList()
    {
        await GetCheckList("cashin/list");
    }

    /// <summary>
    /// Возвращает результат операции выемки наличных по идентификатору операции (docId)
    /// </summary>
    public async Task GetCashOut()
    {
        await GetDocumentById("cashout");
    }

    /// <summary>
    /// Загрузка изображения в выбранную ККТ
    /// </summary>
    public async Task SendPicture()
    {
        await Post("picture", new UploadPicture
        {
            DeviceName = DeviceName,
            PictureName = PictureName,
            Base64 = PictureBase64,
            Alignment = PictureAlignment
        });
    }

    /// <summary>
    /// Получение списка изображений
    /// </summary>
    public async Task GetPictureList()
    {
        await Get($"picture/list?{DeviceQuery}");
        Pictures = ReadResult<List<Picture>>() ?? [];
    }

    /// <summary>
    /// Открытие сессии регистрации (проверки) кодов маркировки на ККТ
    /// </summary>
    public async Task OpenSessionRegistrationKM()
    {
        await Post("marking/session/open", new CheckbaseParameters { DeviceName = DeviceName });
    }

    /// <summary>
    /// Закрытие сессии регистрации (проверки) кодов маркировки на ККТ
    /// </summary>
    public async Task CloseSessionRegistrationKM()
    {
        await Post("marking/session/close", new CheckbaseParameters { DeviceName = DeviceName });
    }

    /// <summary>
    /// Локальная проверка кода маркировки на ККТ (ФФД 1.2)
    /// </summary>
    public async Task RequestKM()
    {
        if (string.IsNullOrWhiteSpace(RequestKmGuid))
            RequestKmGuid = Guid.NewGuid().ToString();

        await Post("marking/km/request", new RequestKmParameters
        {
            DeviceName = DeviceName,
            RequestKM = new RequestKm
            {
                Guid = RequestKmGuid,
                NotSendToServer = NotSendToServer,
                WaitForResult = WaitForResult,
                MarkingCode = MarkingCode,
                PlannedStatus = PlannedStatus,
                Quantity = MarkingQuantity,
                MeasureOfQuantity = MeasureOfQuantity,
                FractionalQuantityNumerator = FractionalQuantityNumerator > 0 ? FractionalQuantityNumerator : null,
                FractionalQuantityDenominator = FractionalQuantityDenominator > 0 ? FractionalQuantityDenominator : null
            }
        });
        MarkingCheck = ReadResult<RequestKmResult>();
    }

    /// <summary>
    /// Получение результата проверки кода маркировки в ОИСМ
    /// </summary>
    public async Task GetProcessingKMResult()
    {
        await Get($"marking/km/result?{DeviceQuery}");
        MarkingProcessing = ReadResult<ProcessingKmResult>();
        if (!string.IsNullOrWhiteSpace(MarkingProcessing?.Guid))
            RequestKmGuid = MarkingProcessing!.Guid!;
    }

    /// <summary>
    /// Подтверждение, будет ли ранее проверенный код маркировки фактически включён в документ реализации. Действительно только в рамках открытой сессии регистрации
    /// </summary>
    public async Task ConfirmKM()
    {
        await Post("marking/km/confirm", new RequestConfirmKm
        {
            DeviceName = DeviceName,
            GUID = RequestKmGuid,
            ConfirmationType = ConfirmationType
        });
    }

    /// <summary>
    /// Печать нефискального документа
    /// </summary>
    public async Task PrintSlip()
    {
        await Post("slip", SlipBody());
    }

    /// <summary>
    /// Асинхронно поставить нефискальный документ в очередь печати
    /// </summary>
    public async Task PrintSlipAsync()
    {
        await Post("slip/async", SlipBody());
    }
}
