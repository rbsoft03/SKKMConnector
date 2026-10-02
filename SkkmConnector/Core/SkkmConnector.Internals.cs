using System.Globalization;
using System.Text.Json;
using RBSoftSkkm.Internal;

namespace RBSoftSkkm;

// Транспортная инфраструктура: выбор соединения, вызов и разбор ответа.
public sealed partial class SkkmConnector
{
    private string DeviceQuery => $"device={Uri.EscapeDataString(DeviceName)}";
    private string IdQuery => $"id={Uri.EscapeDataString(DocumentId)}";
    private string DocIdQuery => $"docId={Uri.EscapeDataString(DocumentId)}";

    private static readonly JsonSerializerOptions ResultJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private KkmTransport Transport()
    {
        var (host, port) = ParseAddress(Address);
        _http.Host = host;
        _http.Port = port;
        _http.Token = Token;
        _http.TerminalId = TerminalId;
        _http.Timeout = Timeout;
        return _http;
    }

    /// <summary>
    /// Разбирает <see cref="Address"/> на хост и порт.
    /// </summary>
    private static (string Host, int Port) ParseAddress(string address)
    {
        const int defaultPort = 4398;
        if (string.IsNullOrWhiteSpace(address))
            return ("localhost", defaultPort);

        var value = address.Trim();
        var separator = value.LastIndexOf(':');
        if (separator < 0)
            return (value, defaultPort);

        var host = value[..separator];
        var portText = value[(separator + 1)..];
        if (int.TryParse(portText, out var port) && port is >= 1 and <= 65535)
            return (host.Length == 0 ? "localhost" : host, port);

        return (value, defaultPort);
    }

    /// <summary>
    /// Сбрасывает ВСЕ данные ответа (плоские поля и объекты), чтобы результат
    /// каждого запроса отражал только сам себя и не оставался от предыдущего вызова.
    /// Не трогает вход запроса (для него — <see cref="Clear"/>) и данные подключения.
    /// </summary>
    private void ResetResponse()
    {
        // Плоские скаляры ответа
        FiscalResult = null;
        FiscalSign = "";
        FnNumber = "";
        ShiftNumber = 0;
        CheckNumber = 0;
        CheckNumberInShift = 0;
        RnNumber = "";
        FnsUrl = "";
        DocumentId = "";
        ServerDateTime = "";
        FiscalDateTime = "";
        DeviceDateTime = "";
        ComputerTime = default;
        DeviceTime = default;
        CurrentShiftState = null;
        _backlog = null;
        FnValidityDate = "";
        FnDaysResources = 0;
        IsFnPresent = false;
        IsFiscal = false;
        FnWarnings = null;
        CashBalance = 0;
        NonZeroSum = 0;
        ShiftDocumentsCount = 0;
        ShiftClosingCheckNumber = 0;
        LineLength = 0;
        LineLengthPixels = 0;
        DeviceModel = "";
        DeviceSerialNumber = "";
        DeviceFirmwareVersion = "";
        DeviceConfigurationVersion = "";
        DeviceFfdVersion = "";
        FnFfdVersion = "";
        DeviceClass = default;
        DeviceTimeZone = 0;

        // Объекты ответа
        Check = null;
        Checks = [];
        Shifts = [];
        Status = null;
        ShiftStatus = null;
        ShiftTotals = null;
        OverallTotals = null;
        Kkt = null;
        Devices = [];
        Pictures = [];
        PictureBase64Result = "";
        TaskStatus = null;
        PrintForm = [];
        Pools = [];
        Queue = [];
        QueueTask = null;
        Operation = null;
        OperationHistory = [];
        OperationTlv = "";
        OperationKm = [];
        RelatedOperations = [];
        Operations = [];
        PrintTemplate = null;
        Templates = [];
        CheckTemplate = null;
        CheckTemplateDocument = null;
        CheckTemplates = [];
        FiscalizationDocument = null;
        Fiscalizations = [];
        MarkingCheck = null;
        MarkingProcessing = null;
        MarkingVerify = null;
        ServerVersion = "";
        ServerProduct = "";
    }

    private void Apply<T>(ResponseResult<T> result)
    {
        // Каждый ответ отражает только сам себя: стираем данные предыдущего запроса,
        // чтобы поля/объекты ответа не «залипали» между разными вызовами.
        ResetResponse();

        Ok = result.Success;
        ErrorCode = result.Code;
        ErrorDescription = result.Description ?? "";
        Result = ToJsonElement(result.Result);

        if (Result.ValueKind == JsonValueKind.String)
        {
            DocumentId = "";
            var id = Result.GetString();
            if (Ok && !string.IsNullOrEmpty(id))
                DocumentId = id!;
            return;
        }

        ExtractFiscalResult(Result);
        if (LooksLikeDocument(Result))
            ApplyDocument(ReadResult<CheckDocument>());
    }

    private static JsonElement ToJsonElement<T>(T? value)
    {
        if (value is JsonElement element)
            return element;
        if (value is null)
            return default;
        return JsonSerializer.SerializeToElement(value, ResultJsonOptions);
    }

    /// <summary>
    /// Разбор Result
    /// Имена полей читаются без учёта регистра.
    /// </summary>
    private void ExtractFiscalResult(JsonElement result)
    {
        if (result.ValueKind != JsonValueKind.Object)
            return;

        FiscalResult? fiscal;
        try
        {
            fiscal = result.Deserialize<FiscalResult>(ResultJsonOptions);
        }
        catch (JsonException)
        {
            fiscal = null;
        }

        fiscal ??= new FiscalResult();
        OverlayFiscalFromJson(result, fiscal);

        var hasFiscal =
            !string.IsNullOrEmpty(fiscal.FiscalSign)
            || fiscal.FiscalNumber > 0
            || fiscal.ShiftNumber > 0
            || !string.IsNullOrEmpty(fiscal.DocId)
            || !string.IsNullOrEmpty(fiscal.FnNumber)
            || !string.IsNullOrEmpty(fiscal.RnNumber)
            || fiscal.CashSum.HasValue
            || fiscal.CashDrawer != null
            || fiscal.Backlog != null
            || fiscal.OutputParameters != null
            || fiscal.ShiftTotal != null
            || fiscal.OverallTotals != null
            || fiscal.ShiftState.HasValue
            || !string.IsNullOrEmpty(fiscal.DateTime)
            || !string.IsNullOrEmpty(fiscal.FiscalDateTime)
            || !string.IsNullOrEmpty(fiscal.FnsUrl);

        if (!hasFiscal)
            return;

        FiscalResult = fiscal;

        if (!string.IsNullOrEmpty(fiscal.DocId))
            DocumentId = fiscal.DocId!;
        if (fiscal.ShiftNumber > 0)
            ShiftNumber = fiscal.ShiftNumber;
        if (fiscal.FiscalNumber > 0)
            CheckNumber = fiscal.FiscalNumber;
        if (fiscal.ShiftState.HasValue)
            CurrentShiftState = fiscal.ShiftState;
        if (!string.IsNullOrEmpty(fiscal.FnsUrl))
            FnsUrl = fiscal.FnsUrl!;
        if (!string.IsNullOrEmpty(fiscal.FnNumber))
        {
            FnNumber = fiscal.FnNumber!;
            IsFnPresent = true;
        }
        else if (fiscal.FnNumber != null)
            IsFnPresent = false;
        if (!string.IsNullOrEmpty(fiscal.RnNumber))
        {
            RnNumber = fiscal.RnNumber!;
            IsFiscal = true;
        }
        else if (fiscal.RnNumber != null)
            IsFiscal = false;
        if (!string.IsNullOrEmpty(fiscal.FiscalSign))
            FiscalSign = fiscal.FiscalSign!;
        if (!string.IsNullOrEmpty(fiscal.DateTime))
            ServerDateTime = fiscal.DateTime!;
        if (!string.IsNullOrEmpty(fiscal.FiscalDateTime))
        {
            FiscalDateTime = fiscal.FiscalDateTime!;
            DeviceDateTime = fiscal.FiscalDateTime!;
        }

        if (fiscal.CashDrawer != null)
            CashBalance = fiscal.CashDrawer.Sum;
        else if (fiscal.CashSum.HasValue)
            CashBalance = fiscal.CashSum.Value;

        if (fiscal.ShiftTotal != null)
            ShiftTotals = fiscal.ShiftTotal;
        if (fiscal.OverallTotals != null)
            OverallTotals = fiscal.OverallTotals;

        ApplyBacklog(fiscal.Backlog);
        ApplyOutputParameters(fiscal.OutputParameters);
        ApplyDeviceInfo(fiscal.DeviceInfo);
    }

    /// <summary>
    /// Раскладывает сведения об устройстве
    /// </summary>
    private void ApplyDeviceInfo(Device? device)
    {
        if (device == null)
            return;

        if (!string.IsNullOrEmpty(device.Model))
            DeviceModel = device.Model!;
        if (!string.IsNullOrEmpty(device.SerialNumber))
            DeviceSerialNumber = device.SerialNumber!;
        if (!string.IsNullOrEmpty(device.FirmwareVersion))
            DeviceFirmwareVersion = device.FirmwareVersion!;
        if (!string.IsNullOrEmpty(device.ConfigurationVersion))
            DeviceConfigurationVersion = device.ConfigurationVersion!;
        if (!string.IsNullOrEmpty(device.FfdVersion))
            DeviceFfdVersion = device.FfdVersion!;
        if (!string.IsNullOrEmpty(device.FnFfdVersion))
            FnFfdVersion = device.FnFfdVersion!;
        if (device.DeviceClass != default)
            DeviceClass = device.DeviceClass;
        if (device.TimeZone > 0)
            DeviceTimeZone = device.TimeZone;
        IsFiscal = device.IsFiscal;
        if (device.LineLength > 0)
            LineLength = device.LineLength;
        if (device.LineLengthPixels > 0)
            LineLengthPixels = device.LineLengthPixels;
    }

    /// <summary>
    /// Раскладывает статус ККТ 
    /// </summary>
    private void ApplyStatus(KktStatus? status)
    {
        Status = status;
        if (status == null)
            return;

        ShiftNumber = status.ShiftNumber;
        CheckNumber = status.DocNumber;
        CurrentShiftState = status.ShiftState;
        ComputerTime = status.ComputerTime;
        DeviceTime = status.DeviceTime;
        LineLength = status.LineLength;
        LineLengthPixels = status.LineLengthPixels;
        IsFnPresent = status.IsFnPresent;
        IsFiscal = status.IsFiscal;
        FnWarnings = status.Warnings;
        _backlog = status.Backlog;
    }

    /// <summary>
    /// Раскладывает сведения о фискальном накопителе 
    /// </summary>
    private void ApplyFnInfo(Fn? fn)
    {
        if (fn == null)
            return;

        FnNumber = fn.SerialNumber ?? "";
        RnNumber = fn.RnNumber ?? "";
        FnsUrl = fn.FnsUrl ?? "";
        FnWarnings ??= fn.Warnings;
        if (string.IsNullOrEmpty(FnFfdVersion))
            FnFfdVersion = fn.FfdVersion ?? "";
        if (fn.ValidityDate != default)
        {
            FnValidityDate = fn.ValidityDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            FnDaysResources = DaysLeft(fn.ValidityDate);
        }
    }

    private static int DaysLeft(DateTime validUntil)
        => Math.Max(0, (validUntil.Date - DateTime.Today).Days);

    /// <summary>
    /// Дочитывает фискальные поля из JSON, если десериализация
    /// их пропустила из‑за другого регистра или имени (DocNumber, Fn, DocumentHeader).
    /// </summary>
    private static void OverlayFiscalFromJson(JsonElement result, FiscalResult fiscal)
    {
        fiscal.FiscalSign ??= ReadString(result, "fiscalSign", "FiscalSign");
        fiscal.DocId ??= ReadString(result, "docId", "DocId");
        fiscal.FnNumber ??= ReadString(result, "fnNumber", "FnNumber", "Fn");
        fiscal.RnNumber ??= ReadString(result, "rnNumber", "RnNumber");
        fiscal.FnsUrl ??= ReadString(result, "fnsUrl", "FnsUrl");
        fiscal.DateTime ??= ReadString(result, "datetime", "DateTime", "Date");
        fiscal.FiscalDateTime ??= ReadString(result, "fiscalDatetime", "FiscalDateTime", "FiscalDate");
        fiscal.DeviceName ??= ReadString(result, "deviceName", "DeviceName");

        if (fiscal.FiscalNumber <= 0)
            fiscal.FiscalNumber = ReadInt(result, "fiscalNumber", "FiscalNumber", "DocNumber");
        if (fiscal.ShiftNumber <= 0)
            fiscal.ShiftNumber = ReadInt(result, "shiftNumber", "ShiftNumber");
        fiscal.CashSum ??= ReadDecimal(result, "CashSum", "cashSum");

        if (TryGetProperty(result, out var header, "DocumentHeader", "documentHeader"))
        {
            fiscal.FiscalSign ??= ReadString(header, "FiscalSign", "fiscalSign");
            fiscal.FnNumber ??= ReadString(header, "Fn", "FnNumber", "fnNumber");
            fiscal.RnNumber ??= ReadString(header, "RnNumber", "rnNumber");
            fiscal.FnsUrl ??= ReadString(header, "FnsUrl", "fnsUrl");
            if (fiscal.FiscalNumber <= 0)
                fiscal.FiscalNumber = ReadInt(header, "DocNumber", "FiscalNumber");
            if (fiscal.ShiftNumber <= 0)
                fiscal.ShiftNumber = ReadInt(header, "ShiftNumber");
        }
    }

    private static bool LooksLikeDocument(JsonElement result)
        => result.ValueKind == JsonValueKind.Object
           && HasProperty(result, "DocumentHeader", "CheckItems", "TaskType", "FiscalDate", "DrawerNumber");

    private static bool HasProperty(JsonElement obj, params string[] names)
    {
        foreach (var name in names)
        {
            if (TryGetProperty(obj, out _, name))
                return true;
        }
        return false;
    }

    private static bool TryGetProperty(JsonElement obj, out JsonElement value, params string[] names)
    {
        foreach (var property in obj.EnumerateObject())
        {
            foreach (var name in names)
            {
                if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    value = property.Value;
                    return true;
                }
            }
        }

        value = default;
        return false;
    }

    private static string? ReadString(JsonElement obj, params string[] names)
    {
        if (!TryGetProperty(obj, out var value, names))
            return null;
        return value.ValueKind switch
        {
            JsonValueKind.String => value.GetString(),
            JsonValueKind.Null or JsonValueKind.Undefined or JsonValueKind.Object or JsonValueKind.Array => null,
            _ => value.ToString()
        };
    }

    private static int ReadInt(JsonElement obj, params string[] names)
    {
        if (!TryGetProperty(obj, out var value, names))
            return 0;
        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
            return number;
        return int.TryParse(value.ToString(), out var parsed) ? parsed : 0;
    }

    private static decimal? ReadDecimal(JsonElement obj, params string[] names)
    {
        if (!TryGetProperty(obj, out var value, names))
            return null;
        if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var number))
            return number;
        return decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : null;
    }

    private void ApplyBacklog(Backlog? backlog)
    {
        if (backlog == null)
            return;

        _backlog = backlog;
    }

    private void ApplyOutputParameters(FiscalOutputParameters? output)
    {
        if (output == null)
            return;

        if (output.ShiftState.HasValue)
            CurrentShiftState = output.ShiftState;
        if (output.NumberOfChecks > 0)
            CheckNumberInShift = output.NumberOfChecks;
        if (output.NumberOfDocuments > 0)
            ShiftDocumentsCount = output.NumberOfDocuments;
        if (output.ShiftClosingCheckNumber > 0)
            ShiftClosingCheckNumber = output.ShiftClosingCheckNumber;
        if (!string.IsNullOrEmpty(output.DateTime))
        {
            FiscalDateTime = output.DateTime!;
            DeviceDateTime = output.DateTime!;
        }
        if (output.ShiftNumber > 0)
            ShiftNumber = output.ShiftNumber;
        if (output.CheckNumber > 0)
            CheckNumber = output.CheckNumber;
        CashBalance = output.CashBalance;
        if (!string.IsNullOrEmpty(output.FnValidityDate))
            FnValidityDate = output.FnValidityDate!;
        if (output.ResourcesFn > 0)
            FnDaysResources = output.ResourcesFn;
        else if (!string.IsNullOrEmpty(FnValidityDate)
                 && DateTime.TryParse(FnValidityDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var validUntil))
            FnDaysResources = DaysLeft(validUntil);

        ApplyBacklog(output.Backlog);

        if (output.FnWarnings != null)
            FnWarnings = output.FnWarnings;
    }

    /// <summary>
    /// Шаблон печати из ответа
    /// </summary>
    private void ApplyTemplate(PrintTemplate? template)
    {
        PrintTemplate = template;
        if (template == null)
            return;

        if (!string.IsNullOrEmpty(template.Name))
            TemplateName = template.Name;
        TemplateType = template.Type;
        RestorePositionsFromPrintLines(template.Lines);
    }

    /// <summary>
    /// Шаблон чека из ответа
    /// </summary>
    private void ApplyCheckTemplate(CheckTemplate? template)
    {
        CheckTemplate = template;
        CheckTemplateDocument = template?.Document;
        if (!string.IsNullOrEmpty(template?.Name))
            TemplateName = template!.Name;
        if (template?.Document != null)
            RestoreCheckFromTemplate(template.Document);
    }

    private T? ReadResult<T>()
    {
        if (Result.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
            return default;

        try
        {
            return Result.Deserialize<T>(ResultJsonOptions);
        }
        catch (JsonException)
        {
            return default;
        }
    }

    private void ApplyOperation(DeviceTaskInfo? operation)
    {
        Operation = operation;
        if (!string.IsNullOrEmpty(operation?.DocId))
            DocumentId = operation.DocId;
    }

    private void ApplyDocument(CheckDocument? document)
    {
        Check = document;
        if (document == null)
            return;

        ApplyDeviceInfo(document.DeviceInfo);

        if (document.ShiftTotal != null)
            ShiftTotals = document.ShiftTotal;
        if (document.OverallTotals != null)
            OverallTotals = document.OverallTotals;

        var header = document.DocumentHeader;
        var fiscalSign = FirstNonEmpty(document.FiscalSign, header?.FiscalSign);
        var fn = FirstNonEmpty(document.Fn, header?.Fn);
        var shiftNumber = document.ShiftNumber > 0 ? document.ShiftNumber : header?.ShiftNumber ?? 0;
        var docNumber = document.DocNumber > 0 ? document.DocNumber : header?.DocNumber ?? 0;

        if (!string.IsNullOrEmpty(fiscalSign))
            FiscalSign = fiscalSign!;
        if (docNumber > 0)
            CheckNumber = docNumber;
        if (shiftNumber > 0)
            ShiftNumber = shiftNumber;
        if (!string.IsNullOrEmpty(document.DocId))
            DocumentId = document.DocId!;
        if (document.DocNumberInShift > 0)
            CheckNumberInShift = document.DocNumberInShift;
        if (document.CashSum.HasValue)
            CashBalance = document.CashSum.Value;
        if (document.Lines is { Length: > 0 })
            PrintForm = (document.Lines ?? []).Select(x => new PrintLine(x)).ToList();
        if (!string.IsNullOrEmpty(fn))
        {
            FnNumber = fn!;
            IsFnPresent = true;
        }
        if (!string.IsNullOrEmpty(header?.RnNumber))
        {
            RnNumber = header!.RnNumber!;
            IsFiscal = true;
        }
        if (!string.IsNullOrEmpty(header?.FnsUrl))
            FnsUrl = header!.FnsUrl!;

        FiscalResult = new FiscalResult
        {
            DateTime = document.Date != default
                ? document.Date.ToString("o", CultureInfo.InvariantCulture)
                : FiscalResult?.DateTime,
            DeviceName = document.DeviceName ?? FiscalResult?.DeviceName,
            DocId = document.DocId ?? FiscalResult?.DocId,
            FnsUrl = header?.FnsUrl ?? FiscalResult?.FnsUrl,
            FnNumber = fn ?? FiscalResult?.FnNumber,
            RnNumber = header?.RnNumber ?? FiscalResult?.RnNumber,
            FiscalDateTime = document.FiscalDate != default
                ? document.FiscalDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)
                : FiscalResult?.FiscalDateTime,
            FiscalSign = fiscalSign ?? FiscalResult?.FiscalSign,
            ShiftNumber = shiftNumber > 0 ? shiftNumber : FiscalResult?.ShiftNumber ?? 0,
            FiscalNumber = docNumber > 0 ? docNumber : FiscalResult?.FiscalNumber ?? 0,
            CashSum = document.CashSum ?? FiscalResult?.CashSum,
            CashDrawer = FiscalResult?.CashDrawer,
            Backlog = FiscalResult?.Backlog,
            OutputParameters = FiscalResult?.OutputParameters,
            ShiftTotal = FiscalResult?.ShiftTotal,
            OverallTotals = FiscalResult?.OverallTotals,
            ShiftState = FiscalResult?.ShiftState
        };
        if (!string.IsNullOrEmpty(FiscalResult.DateTime))
            ServerDateTime = FiscalResult.DateTime!;
        if (!string.IsNullOrEmpty(FiscalResult.FiscalDateTime))
        {
            FiscalDateTime = FiscalResult.FiscalDateTime!;
            DeviceDateTime = FiscalResult.FiscalDateTime!;
        }
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (var value in values)
        {
            if (!string.IsNullOrEmpty(value))
                return value;
        }
        return null;
    }

    private CancellationTokenSource BeginCall()
    {
        var cts = new CancellationTokenSource();
        lock (_callLock)
            _callCts = cts;
        return cts;
    }

    private void EndCall(CancellationTokenSource cts)
    {
        lock (_callLock)
        {
            if (ReferenceEquals(_callCts, cts))
                _callCts = null;
        }
        cts.Dispose();
    }

    private async Task<bool> Get(string path)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Get(path, cts.Token));
            return Ok;
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task<bool> Post(string path, object? body = null)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Post(path, body, cts.Token));
            return Ok;
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task<bool> Put(string path, object? body = null)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Put(path, body, cts.Token));
            return Ok;
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task<bool> Delete(string path)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Delete(path, cts.Token));
            return Ok;
        }
        finally
        {
            EndCall(cts);
        }
    }

    private string DateQuery(DateTime from, DateTime to)
    {
        var fromText = from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var toText = to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        return $"from={fromText}&to={toText}";
    }

    /// <summary>
    /// GET документа по <see cref="DocumentId"/>.
    /// </summary>
    private async Task<bool> GetDocumentById(string path)
    {
        await Get($"{path}?{IdQuery}");
        ApplyDocument(ReadResult<CheckDocument>());
        return Ok;
    }

    /// <summary>
    /// GET списка документов по кассе.
    /// </summary>
    private async Task<bool> GetCheckList(string path)
    {
        await Get($"{path}?{DeviceQuery}");
        Checks = (ReadResult<CheckDocument[]>() ?? []).Select(x => new Check(x)).ToList();
        return Ok;
    }

    /// <summary>
    /// GET списка отчётов за период <see cref="ShiftsFrom"/>..<see cref="ShiftsTo"/>.
    /// </summary>
    private async Task<bool> GetReportList(string path, string? extraQuery = null)
    {
        var query = $"{DeviceQuery}&{DateQuery(ShiftsFrom, ShiftsTo)}";
        if (!string.IsNullOrWhiteSpace(extraQuery))
            query += $"&{extraQuery}";
        await Get($"{path}?{query}");
        Shifts = ReadResult<ShiftListItem[]>() ?? [];
        return Ok;
    }
}
