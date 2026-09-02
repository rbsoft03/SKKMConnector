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
        PaymentType = CheckType.Sale;
        IsProcessed = false;
        TaxVariant = TaxSystem.ОСН;
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
        PictureAlignment = PictureAlignment.Center;
        MarkingCode = "";
        PlannedStatus = MarkingPlannedStatus.Sold;
        MarkingQuantity = 1;
        MeasureOfQuantity = MeasureOfQuantity.Piece;
        FractionalQuantityNumerator = 0;
        FractionalQuantityDenominator = 0;
        NotSendToServer = false;
        WaitForResult = false;
        RequestKmGuid = "";
        ConfirmationType = KmConfirmationType.Included;
        ShiftsFrom = DateTime.Today.AddDays(-7);
        ShiftsTo = DateTime.Today;
        DocumentId = "";
        FiscalSign = "";
        ShiftNumber = 0;
        CheckNumber = 0;
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
        var extra = ReportType > 0 ? $"reportType={ReportType}" : null;
        await GetReportList("shift/z/list", extra);
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
            Alignment = (int)PictureAlignment
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
                PlannedStatus = (int)PlannedStatus,
                Quantity = MarkingQuantity,
                MeasureOfQuantity = (int)MeasureOfQuantity,
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
            ConfirmationType = (int)ConfirmationType
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

    /// <summary>
    /// Версия сервера ККМ.
    /// </summary>
    public async Task GetVersion()
    {
        await Get("version");
        if (LastResult.ValueKind == JsonValueKind.String)
            ServerVersion = LastResult.GetString() ?? "";
        else
            ServerVersion = LastResult.ToString();
    }

    /// <summary>
    /// Получение токена авторизации по логину и паролю.
    /// Нужны <see cref="AuthUserName"/> и <see cref="AuthPassword"/> (по умолчанию Admin / Admin).
    /// </summary>
    public async Task GetUserToken()
    {
        if (string.IsNullOrWhiteSpace(AuthUserName) || string.IsNullOrWhiteSpace(AuthPassword))
        {
            Ok = false;
            ErrorCode = -1;
            ErrorDescription = "Укажите AuthUserName и AuthPassword для получения токена.";
            return;
        }

        await Get("user/token", useBasicAuth: true);
        UserToken = ReadResult<UserToken>();
        if (!string.IsNullOrWhiteSpace(UserToken?.TokenId))
            Token = UserToken.TokenId;
    }

    /// <summary>
    /// Список пользователей сервера ККМ.
    /// </summary>
    public async Task GetUserList()
    {
        await Get("user/list");
        Users = ReadResult<ServiceUser[]>() ?? [];
    }

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    public async Task AddUser()
    {
        await Post("user", new UserProfileRequest { User = ServiceUser });
    }

    /// <summary>
    /// Изменение пользователя.
    /// </summary>
    public async Task UpdateUser()
    {
        await Put($"user?id={Uri.EscapeDataString(UserId)}", ServiceUser);
    }

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    public async Task DeleteUser()
    {
        await Delete($"user?id={Uri.EscapeDataString(UserId)}");
    }

    /// <summary>
    /// Получение настроек службы печати.
    /// </summary>
    public async Task GetServiceSettings()
    {
        await Get("service/settings");
        ServiceSettingsResult = ReadResult<ServiceSettings>();
    }

    /// <summary>
    /// Сохранение настроек службы печати.
    /// </summary>
    public async Task SaveServiceSettings()
    {
        await Post("service/settings", new ServiceSettingsRequest { ServiceSettings = ServiceSettings });
    }

    /// <summary>
    /// Добавление кассы на сервер.
    /// </summary>
    public async Task AddDevice()
    {
        var settings = DeviceSettings ?? new DeviceSettings();
        settings.DeviceName = string.IsNullOrWhiteSpace(settings.DeviceName) ? DeviceName : settings.DeviceName;
        await Post("kkt", new DeviceSettingsRequest { DeviceName = settings.DeviceName, Settings = settings });
    }

    /// <summary>
    /// Изменение настроек кассы.
    /// </summary>
    public async Task UpdateDevice()
    {
        var settings = DeviceSettings ?? new DeviceSettings();
        settings.DeviceName = string.IsNullOrWhiteSpace(settings.DeviceName) ? DeviceName : settings.DeviceName;
        await Put("kkt", new DeviceSettingsRequest { DeviceName = settings.DeviceName, Settings = settings });
    }

    /// <summary>
    /// Удаление кассы с сервера.
    /// </summary>
    public async Task DeleteDevice()
    {
        await Delete($"kkt?device={Uri.EscapeDataString(DeviceName)}");
    }

    /// <summary>
    /// Перезагрузка кассы.
    /// </summary>
    public async Task RebootDevice()
    {
        await Post("kkt/reboot", CheckBase());
    }

    /// <summary>
    /// Настройка шрифтов шаблона кассы.
    /// </summary>
    public async Task SetDeviceFont()
    {
        var settings = DeviceSettings;
        await Post("kkt/font/setting", new DeviceFontSettingsRequest
        {
            DeviceName = DeviceName,
            TemplateSettingH1 = settings?.TemplateSettingH1,
            TemplateSettingH2 = settings?.TemplateSettingH2,
            TemplateSettingH3 = settings?.TemplateSettingH3,
            TemplateSettingH4 = settings?.TemplateSettingH4,
            TemplateSettingH5 = settings?.TemplateSettingH5
        });
    }

    /// <summary>
    /// Список пулов устройств.
    /// </summary>
    public async Task GetPoolList()
    {
        await Get("pool/list");
        Pools = ReadResult<string[]>() ?? [];
    }

    /// <summary>
    /// Список касс в пуле.
    /// </summary>
    public async Task GetDeviceListByPool()
    {
        await Get($"kkt/list/byPool?pool={Uri.EscapeDataString(PoolName)}");
        Devices = ReadResult<DeviceListResponse[]>() ?? [];
    }

    /// <summary>
    /// Асинхронное открытие смены.
    /// </summary>
    public async Task OpenShiftAsync()
    {
        await Post("shift/open/async", CheckBase());
    }

    /// <summary>
    /// Асинхронное закрытие смены.
    /// </summary>
    public async Task CloseShiftAsync()
    {
        await Post("shift/z/async", CheckBase());
    }

    /// <summary>
    /// Асинхронный X-отчёт.
    /// </summary>
    public async Task ReportXAsync()
    {
        await Post("shift/x/async", CheckBase());
    }

    /// <summary>
    /// Асинхронный отчёт о состоянии расчётов.
    /// </summary>
    public async Task ReportSettlementAsync()
    {
        await Post("report/settlement/async", CheckBase());
    }

    /// <summary>
    /// Асинхронное внесение наличных.
    /// </summary>
    public async Task CashInAsync()
    {
        await Post("cashin/async", CashBody());
    }

    /// <summary>
    /// Асинхронная выемка наличных.
    /// </summary>
    public async Task CashOutAsync()
    {
        await Post("cashout/async", CashBody());
    }

    /// <summary>
    /// Список чеков за период или смену.
    /// </summary>
    public async Task GetCheckList()
    {
        var query = $"{DeviceQuery}&{DateQuery(ShiftsFrom, ShiftsTo)}";
        if (ShiftNumber > 0)
            query += $"&shift={ShiftNumber}";
        await Get($"check/list?{query}");
        Checks = ReadResult<CheckDocument[]>() ?? [];
    }

    /// <summary>
    /// Печать копии чека по данным фискального накопителя.
    /// </summary>
    public async Task PrintCheckCopyFn()
    {
        await Post("check/copy/fn", new CheckCopyFnParameters
        {
            DeviceName = DeviceName,
            FnNumber = FnNumber,
            FiscalSign = FiscalSign,
            DocNumber = CheckNumber
        });
    }

    /// <summary>
    /// Получение слипа по идентификатору документа.
    /// </summary>
    public async Task GetSlip()
    {
        await GetDocumentById("slip");
    }

    /// <summary>
    /// Список слипов по кассе.
    /// </summary>
    public async Task GetSlipList()
    {
        await GetCheckList("slip/list");
    }

    /// <summary>
    /// Получение картинки по имени.
    /// </summary>
    public async Task GetPicture()
    {
        await Get($"picture?{DeviceQuery}&id={Uri.EscapeDataString(PictureId)}");
        if (Ok && LastResult.ValueKind == JsonValueKind.String)
            PictureBase64Result = LastResult.GetString() ?? "";
    }

    /// <summary>
    /// Удаление картинки.
    /// </summary>
    public async Task DeletePicture()
    {
        await Delete($"picture?{DeviceQuery}&id={Uri.EscapeDataString(PictureId)}");
    }

    /// <summary>
    /// Создание шаблона печати.
    /// </summary>
    public async Task AddTemplate()
    {
        await Post("template", TemplateParameters);
    }

    /// <summary>
    /// Изменение шаблона печати.
    /// </summary>
    public async Task UpdateTemplate()
    {
        await Put("template", TemplateParameters);
    }

    /// <summary>
    /// Удаление шаблона печати.
    /// </summary>
    public async Task DeleteTemplate()
    {
        await Delete($"template?id={Uri.EscapeDataString(TemplateName)}");
    }

    /// <summary>
    /// Список шаблонов печати.
    /// </summary>
    public async Task GetTemplateList()
    {
        await Get("template/list");
        Templates = ReadTemplateList();
    }

    /// <summary>
    /// Получение шаблона печати по имени.
    /// </summary>
    public async Task GetTemplate()
    {
        await Get($"template?name={Uri.EscapeDataString(TemplateName)}");
        PrintTemplate = ReadResult<PrintTemplate>();
    }

    /// <summary>
    /// Создание шаблона чека.
    /// </summary>
    public async Task AddCheckTemplate()
    {
        await Post("checkTemplate", CheckTemplateBody());
    }

    /// <summary>
    /// Изменение шаблона чека.
    /// </summary>
    public async Task UpdateCheckTemplate()
    {
        await Put("checkTemplate", CheckTemplateBody());
    }

    /// <summary>
    /// Удаление шаблона чека.
    /// </summary>
    public async Task DeleteCheckTemplate()
    {
        await Delete($"checkTemplate?id={Uri.EscapeDataString(TemplateName)}");
    }

    /// <summary>
    /// Список шаблонов чека.
    /// </summary>
    public async Task GetCheckTemplateList()
    {
        await Get("checkTemplate/list");
        CheckTemplates = ReadResult<CheckTemplateListItem[]>() ?? [];
    }

    /// <summary>
    /// Получение шаблона чека по имени.
    /// </summary>
    public async Task GetCheckTemplate()
    {
        await Get($"checkTemplate?id={Uri.EscapeDataString(TemplateName)}");
        CheckTemplate = ReadResult<CheckTemplate>();
    }

    /// <summary>
    /// Состояние очереди печати.
    /// </summary>
    public async Task GetQueue()
    {
        await Get("queue");
        Queue = ReadResult<QueueItem[]>() ?? [];
    }

    /// <summary>
    /// Состояние задания в очереди.
    /// </summary>
    public async Task GetQueueTask()
    {
        await Get($"queue/task?taskId={Uri.EscapeDataString(QueueTaskId)}");
        QueueTask = ReadResult<QueueTaskState>();
    }

    /// <summary>
    /// История обработки задания в очереди.
    /// </summary>
    public async Task GetQueueTaskHistory()
    {
        await Get($"queue/task/history?taskId={Uri.EscapeDataString(QueueTaskId)}");
        QueueTask = ReadResult<QueueTaskState>();
        if (QueueTask != null)
            OperationHistory = QueueTask.History
                .Select(h => new OperationHistoryItem
                {
                    Time = h.Time,
                    State = h.State,
                    Description = h.Description
                })
                .ToArray();
    }

    /// <summary>
    /// Отмена задания в очереди.
    /// </summary>
    public async Task CancelQueueTask()
    {
        await Delete($"queue/task?taskId={Uri.EscapeDataString(QueueTaskId)}");
    }

    /// <summary>
    /// Проверка кода маркировки через внешний сервис.
    /// </summary>
    public async Task VerifyMarking()
    {
        await Post("marking/km/verify", new MarkingCodesRequest
        {
            DeviceName = DeviceName,
            Codes = MarkingCodes.ToList()
        });
        MarkingVerify = ReadResult<MarkingVerifyResult>();
    }

    /// <summary>
    /// Проверка кода маркировки через ТС ПИоТ.
    /// </summary>
    public async Task VerifyMarkingTsPiot()
    {
        await Post("marking/km/tspiot/verify", new MarkingCodesRequest
        {
            DeviceName = DeviceName,
            Codes = MarkingCodes.ToList()
        });
        MarkingVerify = ReadResult<MarkingVerifyResult>();
    }

    /// <summary>
    /// Проверка кода маркировки через ЛМ ЧЗ.
    /// </summary>
    public async Task VerifyMarkingLmcz()
    {
        await Post("marking/km/lmcz/verify", new MarkingCodesRequest
        {
            DeviceName = DeviceName,
            Codes = MarkingCodes.ToList()
        });
        MarkingVerify = ReadResult<MarkingVerifyResult>();
    }

    /// <summary>
    /// Фискализация кассы.
    /// </summary>
    public async Task Fiscalization()
    {
        await Post("fiscalization", FiscalizationBody());
    }

    /// <summary>
    /// Асинхронная фискализация кассы.
    /// </summary>
    public async Task FiscalizationAsync()
    {
        await Post("fiscalization/async", FiscalizationBody());
    }

    /// <summary>
    /// Результат фискализации по идентификатору документа.
    /// </summary>
    public async Task GetFiscalization()
    {
        await GetDocumentById("fiscalization");
        FiscalizationDocument = ReadResult<FiscalizationDocument>();
    }

    /// <summary>
    /// Список операций фискализации по кассе.
    /// </summary>
    public async Task GetFiscalizationList()
    {
        await Get($"fiscalization/list?{DeviceQuery}");
        Fiscalizations = ReadResult<FiscalizationDocument[]>() ?? [];
    }

    /// <summary>
    /// Последняя операция из базы. <c>tasktype</c> — <see cref="PaymentType"/> (<see cref="CheckType"/>), <c>isProcessed</c> — <see cref="IsProcessed"/>.
    /// </summary>
    public async Task GetOperationLast()
    {
        var processed = IsProcessed ? "true" : "false";
        await Get($"operation/last?tasktype={(int)PaymentType}&isProcessed={processed}");
        ApplyOperation(ReadResult<DeviceTaskInfo>());
    }

    /// <summary>
    /// Операция по идентификатору документа.
    /// </summary>
    public async Task GetOperation()
    {
        await Get($"operation?{DocIdQuery}");
        ApplyOperation(ReadResult<DeviceTaskInfo>());
    }

    /// <summary>
    /// История операции по идентификатору документа.
    /// </summary>
    public async Task GetOperationHistory()
    {
        await Get($"operation/history?{DocIdQuery}");
        OperationHistory = ReadResult<OperationHistoryItem[]>() ?? [];
    }

    /// <summary>
    /// TLV-данные операции.
    /// </summary>
    public async Task GetOperationTlv()
    {
        await Get($"operation/tlv?{DocIdQuery}");
        if (Ok && LastResult.ValueKind == JsonValueKind.String)
            OperationTlv = LastResult.GetString() ?? "";
    }

    /// <summary>
    /// Данные маркировки операции.
    /// </summary>
    public async Task GetOperationKm()
    {
        await Get($"operation/km?{DocIdQuery}");
        OperationKm = ReadResult<OperationKmRow[]>() ?? [];
    }

    /// <summary>
    /// Связанные операции.
    /// </summary>
    public async Task GetOperationRelated()
    {
        await Get($"operation/related?{DocIdQuery}");
        RelatedOperations = ReadResult<DeviceTaskInfo[]>() ?? [];
    }

    /// <summary>
    /// Список операций за период.
    /// </summary>
    public async Task GetOperationList()
    {
        await Get($"operation/list?{DateQuery(ShiftsFrom, ShiftsTo)}");
        Operations = ReadResult<OperationListItem[]>() ?? [];
    }

    private FiscalizationRequest FiscalizationBody()
    {
        var source = FiscalizationParameters;
        var body = new FiscalizationRequest
        {
            DeviceName = DeviceName,
            Cashier = Cashier,
            RnNumber = source?.RnNumber,
            TaxationSystems = source?.TaxationSystems,
            Vatin = source?.Vatin,
            CompanyName = source?.CompanyName,
            Fn = source?.Fn,
            FfdVersionKkt = source?.FfdVersionKkt,
            FfdVersionFn = source?.FfdVersionFn,
            RegistrationLabelCodes = source?.RegistrationLabelCodes,
            OfdAddress = source?.OfdAddress,
            OfdPort = source?.OfdPort,
            AutomaticNumber = source?.AutomaticNumber,
            SenderEmail = source?.SenderEmail,
            ReasonCode = source?.ReasonCode,
            IsmHost = source?.IsmHost,
            IsmPort = source?.IsmPort,
            FnsUrl = source?.FnsUrl,
            OfdVatin = source?.OfdVatin,
            OfdName = source?.OfdName,
            AgentTypes = source?.AgentTypes,
            IsBsoSign = source?.IsBsoSign,
            IsMarking = source?.IsMarking,
            IsPawnshop = source?.IsPawnshop,
            IsAssurance = source?.IsAssurance,
            IsAutomatic = source?.IsAutomatic,
            IsVending = source?.IsVending,
            IsAutomaticPrinter = source?.IsAutomaticPrinter,
            IsOnline = source?.IsOnline,
            IsLottery = source?.IsLottery,
            IsGambling = source?.IsGambling,
            IsExcisable = source?.IsExcisable,
            IsService = source?.IsService,
            IsEncrypted = source?.IsEncrypted,
            IsOffline = source?.IsOffline,
            IsCateringServices = source?.IsCateringServices,
            IsWholesaleTrade = source?.IsWholesaleTrade,
            SaleAddress = source?.SaleAddress,
            SaleLocation = source?.SaleLocation
        };
        FillBase(body);
        return body;
    }
}
