using SkkmConnector.Internal;
using System.Text.Json;

namespace SkkmConnector;

public sealed partial class ServerKkm
{
    /// <summary>
    /// Очищает данные чека перед новым (позиции, оплаты, покупатель, поля коррекции). Подключение и кассир сохраняются.
    /// </summary>
    public void NewRequest()
    {
        PaymentType = (int)CheckType.Sale;
        TaxVariant = (int)TaxSystem.ОСН;
        Electronically = false;
        TextBefore = "";
        TextAfter = "";
        CustomerInfo = "";
        CustomerVatin = "";
        CustomerEmail = "";
        CustomerPhone = "";
        PayCash = 0;
        PayElectronic = 0;
        PayCredit = 0;
        PayAdvance = 0;
        PayProvision = 0;
        Positions.Clear();
        IsCorrection = false;
        CorrectionType = CorrectionTypes.Самостоятельно;
        CorrectionDescription = "";
        CorrectionDate = default;
        CorrectionNumber = "";
        CorrectedCheckNumber = "";
        CashAmount = 0;
        TextForPrint = "";
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
        LastStatusCode = 0;
        LastDurationMs = 0;
        LastRequestInfo = null;
        LastRequestBody = null;
        LastResponseBody = null;
        LastRequestHeaders = Array.Empty<KeyValuePair<string, string>>();
    }

    /// <summary>
    /// Добавляет фискальную строку в чек.
    /// </summary>
    /// <param name="name">Наименование. Обязательно для печати.</param>
    /// <param name="quantity">Количество. По умолчанию 1.</param>
    /// <param name="measurementUnit">Единица измерения. По умолчанию «шт».</param>
    /// <param name="price">Цена с учётом скидки.</param>
    /// <param name="sum">Сумма позиции. 0 — считается как количество × цена.</param>
    /// <param name="discountSum">Сумма скидки.</param>
    /// <param name="tax">Ставка НДС: none, 0, 10, 20, 22, 10/110, 20/120, 22/122.</param>
    /// <param name="taxSum">Сумма НДС.</param>
    /// <param name="department">Секция / отдел.</param>
    /// <param name="signCalculationObject">Признак предмета расчёта. По умолчанию 4 — услуга.</param>
    /// <param name="signMethodCalculation">Признак способа расчёта. По умолчанию 1 — предоплата полная.</param>
    /// <returns>Добавленная строка. Дальше можно заполнить маркировку, агента, акциз.</returns>
    public CheckLine AddFiscalLine(
        string name = "",
        decimal quantity = 1,
        string measurementUnit = "шт",
        decimal price = 0,
        decimal sum = 0,
        decimal discountSum = 0,
        string tax = "none",
        decimal taxSum = 0,
        int department = 0,
        int signCalculationObject = 4,
        int signMethodCalculation = 1)
    {
        var line = new CheckLine
        {
            IsFiscalLine = true,
            Name = name,
            Quantity = quantity,
            MeasurementUnit = measurementUnit,
            Price = price,
            Sum = sum,
            DiscountSum = discountSum,
            Tax = tax,
            TaxSum = taxSum,
            Department = department,
            SignCalculationObject = signCalculationObject,
            SignMethodCalculation = signMethodCalculation
        };
        Positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавляет нефискальную строку. На сумму чека не влияет.
    /// </summary>
    /// <param name="text">Текст на чеке.</param>
    /// <param name="barcode">Значение штрихкода.</param>
    /// <param name="barcodeType">Тип штрихкода: QR, EAN13, CODE128 и др.</param>
    public CheckLine AddNonFiscalLine(
        string text = "",
        string barcode = "",
        string barcodeType = "")
    {
        var line = new CheckLine
        {
            IsFiscalLine = false,
            Name = text,
            Barcode = barcode,
            BarcodeType = barcodeType
        };
        Positions.Add(line);
        return line;
    }

    /// <summary>
    /// Проверка связи с сервером ККМ (GET ping).
    /// </summary>
    public Task Ping() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<PingResponse>>("ping");
        Apply(result);
    });

    /// <summary>
    /// Список устройств на сервере. Результат — <see cref="Devices"/>.
    /// </summary>
    public Task GetDeviceList() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<DeviceListResponse[]>>("kkt/list");
        Devices = result.Success && result.Result != null ? result.Result : [];
        Apply(result);
    });

    /// <summary>
    /// Подключение к кассе. Результат — <see cref="Kkt"/>.
    /// </summary>
    public Task Connect() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<DataKkt>>($"kkt?device={Uri.EscapeDataString(DeviceName)}");
        Kkt = result.Success ? result.Result : null;
        if (Kkt?.Status != null)
            Status = Kkt.Status;
        if (Kkt?.Device != null)
            LineLength = Kkt.Device.LineLength;
        else if (Kkt?.Status != null)
            LineLength = Kkt.Status.LineLength;
        if (!string.IsNullOrWhiteSpace(Kkt?.Fn?.SaleLocation))
            SaleLocation = Kkt!.Fn!.SaleLocation!;
        Apply(result);
    });

    /// <summary>
    /// Текущее состояние ККМ. Результат — <see cref="Status"/>.
    /// </summary>
    public Task GetStatus() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<KktStatus>>($"kkt/status?device={Uri.EscapeDataString(DeviceName)}");
        Status = result.Success ? result.Result : null;
        if (Status != null)
        {
            LineLength = Status.LineLength;
            ShiftNumber = Status.ShiftNumber;
            CheckNumber = Status.DocNumber;
        }
        Apply(result);
    });

    /// <summary>
    /// Статус кассовой смены. Результат — <see cref="ShiftStatus"/>.
    /// </summary>
    public Task GetShiftStatus() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<ResponseCurrentStatus>>($"kkt/shift/status?device={Uri.EscapeDataString(DeviceName)}");
        ShiftStatus = result.Success ? result.Result : null;
        if (ShiftStatus != null)
        {
            ShiftNumber = ShiftStatus.ShiftNumber;
            CheckNumber = ShiftStatus.CheckNumber;
        }
        Apply(result);
    });

    /// <summary>
    /// Открыть смену.
    /// </summary>
    public Task OpenShift() => Exec(t => t.Post<ResponseResult<JsonElement>>("shift/open", BaseDocument()));

    /// <summary>
    /// Закрыть смену (Z-отчёт).
    /// </summary>
    public Task CloseShift() => Exec(t => t.Post<ResponseResult<JsonElement>>("shift/z", BaseDocument()));

    /// <summary>
    /// X-отчёт.
    /// </summary>
    public Task ReportX() => Exec(t => t.Post<ResponseResult<JsonElement>>("shift/x", BaseDocument()));

    /// <summary>
    /// Отчёт о текущем состоянии расчётов.
    /// </summary>
    public Task ReportSettlement() => Exec(t => t.Post<ResponseResult<JsonElement>>("report/settlement", BaseDocument()));

    /// <summary>
    /// Общие счётчики ККМ. Необнуляемая сумма — <see cref="NonZeroSum"/>.
    /// </summary>
    public Task GetOverAll() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<OverallTotals>>(
            $"kkt/counters/overall?device={Uri.EscapeDataString(DeviceName)}");
        NonZeroSum = result.Success ? result.Result?.Counters?.Sales?.Sum ?? 0 : 0;
        Apply(result);
    });

    /// <summary>
    /// Ширина строки чека. Результат — <see cref="LineLength"/>.
    /// </summary>
    public Task GetLineLength() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<LineLengthV2>>(
            $"kkt/lineLength?device={Uri.EscapeDataString(DeviceName)}");
        if (result.Success && result.Result != null)
        {
            LineLength = result.Result.LineLength;
            LineLengthPixels = result.Result.LineLengthPixels;
        }
        Apply(result);
    });

    /// <summary>
    /// Последняя операция из базы сервера ККМ.
    /// </summary>
    public Task GetLastOperation() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<LastOperationDto>>("operation/last");
        if (result.Success && result.Result != null)
        {
            LastOperationDate = result.Result.Date;
            LastOperationType = result.Result.TaskType;
            LastOperationDocNumber = result.Result.DocNumber;
            LastOperationShiftNumber = result.Result.ShiftNumber;
            LastOperationSum = result.Result.Sum;
        }
        Apply(result);
    });

    /// <summary>
    /// Итоги текущей смены. Результат — <see cref="ShiftTotals"/>.
    /// </summary>
    public Task GetTotals() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<ResShiftTotal>>($"kkt/counters/shift?device={Uri.EscapeDataString(DeviceName)}");
        ShiftTotals = result.Success ? result.Result : null;
        Apply(result);
    });

    /// <summary>
    /// Список смен за период <see cref="ShiftsFrom"/> и <see cref="ShiftsTo"/>.
    /// </summary>
    public Task GetShiftList() => Exec(t =>
        t.Get<ResponseResult<JsonElement>>(
            $"shift/z/list?device={Uri.EscapeDataString(DeviceName)}&from={ShiftsFrom:yyyy-MM-dd}&to={ShiftsTo:yyyy-MM-dd}"));

    /// <summary>
    /// Печатает кассовый чек по заполненным свойствам.
    /// Обязательно заполнить: <see cref="DeviceName"/> (имя кассы), <see cref="PaymentType"/> (тип чека, по умолчанию продажа),
    /// <see cref="TaxVariant"/> (система налогообложения, по умолчанию ОСН) и хотя бы одну фискальную позицию через <see cref="AddFiscalLine"/>.
    /// Суммы оплат (<see cref="PayCash"/>, <see cref="PayElectronic"/>, <see cref="PayAdvance"/>, <see cref="PayCredit"/>, <see cref="PayProvision"/>) в сумме должны покрывать итог позиций.
    /// Необязательно: покупатель (<see cref="CustomerInfo"/>, <see cref="CustomerEmail"/> и др.), тексты <see cref="TextBefore"/> и <see cref="TextAfter"/>, место расчётов <see cref="SaleLocation"/>.
    /// Итог вызова — в <see cref="Ok"/> и <see cref="ErrorDescription"/>.
    /// </summary>
    public Task PrintCheck() => Exec(t => t.Post<ResponseResult<JsonElement>>("check", CheckBody()));

    /// <summary>
    /// Асинхронная печать чека: тело ставится в очередь сервера, возвращается сразу.
    /// Свойства и требования те же, что у <see cref="PrintCheck"/>. Статус задачи потом смотрят через <see cref="GetTaskStatus"/>.
    /// </summary>
    public Task PrintCheckAsync() => Exec(t => t.Post<ResponseResult<JsonElement>>("check/async", CheckBody()));

    /// <summary>
    /// Печатает чек коррекции по ФФД 1.2.
    /// Заполняется как обычный чек (см. <see cref="PrintCheck"/>), плюс данные коррекции:
    /// <see cref="CorrectionType"/> (тип коррекции), <see cref="CorrectionDescription"/> (основание), <see cref="CorrectionDate"/> (дата),
    /// при необходимости <see cref="CorrectionNumber"/> (номер предписания) и <see cref="CorrectedCheckNumber"/> (номер исправляемого чека).
    /// </summary>
    public Task PrintCheckCorrection120() => Exec(t => t.Post<ResponseResult<JsonElement>>("correction120", CorrectionBody()));

    /// <summary>
    /// Асинхронная печать чека коррекции ФФД 1.2: тело ставится в очередь сервера.
    /// Свойства и требования те же, что у <see cref="PrintCheckCorrection120"/>.
    /// </summary>
    public Task PrintCheckCorrection120Async() => Exec(t => t.Post<ResponseResult<JsonElement>>("correction120/async", CorrectionBody()));

    /// <summary>
    /// Список чеков за смену <see cref="ShiftNumber"/>.
    /// </summary>
    public Task GetChecksByShift() => Exec(t =>
        t.Get<ResponseResult<JsonElement>>(
            $"check/list?device={Uri.EscapeDataString(DeviceName)}&shift={ShiftNumber}"));

    /// <summary>
    /// Статус задачи по <see cref="DocumentId"/>.
    /// </summary>
    public Task GetTaskStatus() => Exec(t =>
        t.Get<ResponseResult<JsonElement>>($"task/status?id={Uri.EscapeDataString(DocumentId)}"));

    /// <summary>
    /// Получение чека по <see cref="DocumentId"/>.
    /// </summary>
    public Task GetCheck() => Exec(t =>
        t.Get<ResponseResult<JsonElement>>($"check?id={Uri.EscapeDataString(DocumentId)}"));

    /// <summary>
    /// Копия чека: по <see cref="FiscalSign"/> или последняя.
    /// </summary>
    public Task PrintCheckCopy() => Exec(t =>
        string.IsNullOrWhiteSpace(FiscalSign)
            ? t.Post<ResponseResult<JsonElement>>($"check/copy/last?device={Uri.EscapeDataString(DeviceName)}")
            : t.Post<ResponseResult<JsonElement>>("check/copy", new CheckbaseParameters
            {
                DeviceName = DeviceName,
                DocId = FiscalSign
            }));

    /// <summary>
    /// Печатная форма задачи по <see cref="DocumentId"/>.
    /// </summary>
    public Task GetPrintForm() => Exec(t =>
        t.Get<ResponseResult<JsonElement>>($"task/form?id={Uri.EscapeDataString(DocumentId)}"));

    /// <summary>
    /// Внесение наличных (<see cref="CashAmount"/>).
    /// </summary>
    public Task CashIn() => Exec(t => t.Post<ResponseResult<JsonElement>>("cashin", CashDocument()));

    /// <summary>
    /// Выемка наличных (<see cref="CashAmount"/>).
    /// </summary>
    public Task CashOut() => Exec(t => t.Post<ResponseResult<JsonElement>>("cashout", CashDocument()));

    /// <summary>
    /// Открыть денежный ящик.
    /// </summary>
    public Task OpenCashdrawer() => Exec(t => t.Post<ResponseResult<JsonElement>>("cash/open", BaseDocument()));

    /// <summary>
    /// Остаток наличных. Результат — <see cref="CashBalance"/>.
    /// </summary>
    public Task GetCash() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<CashSum>>($"cash?device={Uri.EscapeDataString(DeviceName)}");
        CashBalance = result.Success && result.Result != null ? result.Result.Sum : 0;
        Apply(result);
    });

    /// <summary>
    /// Загрузить картинку на сервер.
    /// </summary>
    public Task SendPicture() => Exec(t => t.Post<ResponseResult<JsonElement>>("picture", new UploadPicture
    {
        DeviceName = DeviceName,
        PictureName = PictureName,
        Base64 = PictureBase64,
        Alignment = PictureAlignment
    }));

    /// <summary>
    /// Список картинок на сервере. Результат — <see cref="Pictures"/>.
    /// </summary>
    public Task GetPictureList() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<List<Picture>>>($"picture/list?device={Uri.EscapeDataString(DeviceName)}");
        Pictures = result.Success && result.Result != null ? result.Result : [];
        Apply(result);
    });

    /// <summary>
    /// Открытие сессии регистрации кодов маркировки.
    /// </summary>
    public Task OpenSessionRegistrationKM() => Exec(t =>
        t.Post<ResponseResult<JsonElement>>("marking/session/open", DeviceOnly()));

    /// <summary>
    /// Закрытие сессии регистрации кодов маркировки.
    /// </summary>
    public Task CloseSessionRegistrationKM() => Exec(t =>
        t.Post<ResponseResult<JsonElement>>("marking/session/close", DeviceOnly()));

    /// <summary>
    /// Локальная проверка кода маркировки (ФФД 1.2). Результат — <see cref="MarkingCheck"/>.
    /// </summary>
    public Task RequestKM() => Exec(async transport =>
    {
        if (string.IsNullOrWhiteSpace(RequestKmGuid))
            RequestKmGuid = Guid.NewGuid().ToString();

        var result = await transport.Post<ResponseResult<RequestKmResult>>("marking/km/request", new RequestKmParameters
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
        MarkingCheck = result.Success ? result.Result : null;
        Apply(result);
    });

    /// <summary>
    /// Результат проверки кода маркировки в ОИСМ. Результат — <see cref="MarkingProcessing"/>.
    /// </summary>
    public Task GetProcessingKMResult() => Exec(async transport =>
    {
        var result = await transport.Get<ResponseResult<ProcessingKmResult>>(
            $"marking/km/result?device={Uri.EscapeDataString(DeviceName)}");
        MarkingProcessing = result.Success ? result.Result : null;
        if (!string.IsNullOrWhiteSpace(MarkingProcessing?.Guid))
            RequestKmGuid = MarkingProcessing!.Guid!;
        Apply(result);
    });

    /// <summary>
    /// Подтверждение, что код маркировки включён в документ реализации.
    /// </summary>
    public Task ConfirmKM() => Exec(t => t.Post<ResponseResult<JsonElement>>("marking/km/confirm", new RequestConfirmKm
    {
        DeviceName = DeviceName,
        GUID = RequestKmGuid,
        ConfirmationType = ConfirmationType
    }));

    /// <summary>
    /// Синхронная печать слипа (<see cref="TextForPrint"/>).
    /// </summary>
    public Task PrintSlip() => Exec(t => t.Post<ResponseResult<JsonElement>>("slip", SlipBody()));

    /// <summary>
    /// Асинхронная печать слипа.
    /// </summary>
    public Task PrintSlipAsync() => Exec(t => t.Post<ResponseResult<JsonElement>>("slip/async", SlipBody()));
}
