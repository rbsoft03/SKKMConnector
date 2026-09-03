using System.Globalization;
using System.Text.Json;
using SkkmConnector.Internal;

namespace SkkmConnector;

// Транспортная инфраструктура: выбор соединения, вызов и разбор ответа.
public sealed partial class ServerKkm
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
        _http.Host = Host;
        _http.Port = Port;
        _http.UseHttps = UseHttps;
        _http.Token = Token;
        _http.TerminalId = TerminalId;
        _http.BasicAuthUser = AuthUserName;
        _http.BasicAuthPassword = AuthPassword;
        _http.Timeout = Timeout;
        return _http;
    }

    private void Apply<T>(ResponseResult<T> result)
    {
        Ok = result.Success;
        ErrorCode = result.Code;
        ErrorDescription = result.Description ?? "";
        LastResult = ToJsonElement(result.Result);
        FiscalResult = null;

        FiscalSign = "";
        if (LastResult.ValueKind == JsonValueKind.String)
        {
            DocumentId = "";
            var id = LastResult.GetString();
            if (Ok && !string.IsNullOrEmpty(id))
                DocumentId = id!;
            return;
        }

        ExtractFiscalResult(LastResult);
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
    /// заполняет <see cref="FiscalResult"/> и плоские свойства.
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
            return;
        }

        if (fiscal == null)
            return;

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

        ApplyBacklog(fiscal.Backlog);
        ApplyOutputParameters(fiscal.OutputParameters);
    }

    private void ApplyBacklog(Backlog? backlog)
    {
        if (backlog == null)
            return;

        BacklogDocumentsCount = backlog.DocumentsCounter;
        if (backlog.DocumentsCounter > 0)
        {
            BacklogFirstDocumentNumber = backlog.DocumentFirstNumber;
            if (backlog.DocumentFirstDateTime != default)
                BacklogFirstDocumentDateTime = backlog.DocumentFirstDateTime;
        }
        else
        {
            BacklogFirstDocumentNumber = 0;
            BacklogFirstDocumentDateTime = null;
        }
    }

    private void ApplyOutputParameters(FiscalOutputParameters? output)
    {
        if (output == null)
            return;

        if (output.NumberOfChecks > 0)
            CheckNumberInShift = output.NumberOfChecks;
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
        {
            var days = (validUntil.Date - DateTime.Today).Days;
            FnDaysResources = days < 0 ? 0 : days;
        }

        ApplyBacklog(output.Backlog);

        if (output.FnWarnings != null)
            FnWarnings = output.FnWarnings;
    }

    private PrintTemplate[] ReadTemplateList()
    {
        if (LastResult.ValueKind != JsonValueKind.Array)
            return [];

        var list = new List<PrintTemplate>();
        foreach (var item in LastResult.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.String)
                list.Add(new PrintTemplate { Name = item.GetString() ?? "" });
            else
            {
                var parsed = item.Deserialize<PrintTemplate>(ResultJsonOptions);
                if (parsed != null)
                    list.Add(parsed);
            }
        }
        return list.ToArray();
    }

    private T? ReadResult<T>()
    {
        if (LastResult.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
            return default;

        try
        {
            return LastResult.Deserialize<T>(ResultJsonOptions);
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

        if (!string.IsNullOrEmpty(document.FiscalSign))
            FiscalSign = document.FiscalSign!;
        if (document.DocNumber > 0)
            CheckNumber = document.DocNumber;
        if (document.ShiftNumber > 0)
            ShiftNumber = document.ShiftNumber;
        if (!string.IsNullOrEmpty(document.DocId))
            DocumentId = document.DocId!;
        if (document.DocNumberInShift > 0)
            CheckNumberInShift = document.DocNumberInShift;

        var header = document.DocumentHeader;
        if (!string.IsNullOrEmpty(header?.Fn))
        {
            FnNumber = header!.Fn!;
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
            DateTime = document.Date.ToString("o", CultureInfo.InvariantCulture),
            DeviceName = document.DeviceName,
            DocId = document.DocId,
            FnsUrl = header?.FnsUrl,
            FnNumber = header?.Fn,
            RnNumber = header?.RnNumber,
            FiscalDateTime = document.FiscalDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
            FiscalSign = document.FiscalSign,
            ShiftNumber = document.ShiftNumber,
            FiscalNumber = document.DocNumber
        };
        if (!string.IsNullOrEmpty(FiscalResult.DateTime))
            ServerDateTime = FiscalResult.DateTime!;
        if (!string.IsNullOrEmpty(FiscalResult.FiscalDateTime))
        {
            FiscalDateTime = FiscalResult.FiscalDateTime!;
            DeviceDateTime = FiscalResult.FiscalDateTime!;
        }
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

    private async Task Get(string path, bool useBasicAuth = false)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Get(path, useBasicAuth, cts.Token));
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task Post(string path, object? body = null)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Post(path, body, cts.Token));
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task Put(string path, object? body = null)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Put(path, body, cts.Token));
        }
        finally
        {
            EndCall(cts);
        }
    }

    private async Task Delete(string path)
    {
        var cts = BeginCall();
        try
        {
            Apply(await Transport().Delete(path, cts.Token));
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
    private async Task GetDocumentById(string path)
    {
        await Get($"{path}?{IdQuery}");
        ApplyDocument(ReadResult<CheckDocument>());
    }

    /// <summary>
    /// GET списка документов по кассе.
    /// </summary>
    private async Task GetCheckList(string path)
    {
        await Get($"{path}?{DeviceQuery}");
        Checks = ReadResult<CheckDocument[]>() ?? [];
    }

    /// <summary>
    /// GET списка отчётов за период <see cref="ShiftsFrom"/>..<see cref="ShiftsTo"/>.
    /// </summary>
    private async Task GetReportList(string path, string? extraQuery = null)
    {
        var query = $"{DeviceQuery}&{DateQuery(ShiftsFrom, ShiftsTo)}";
        if (!string.IsNullOrWhiteSpace(extraQuery))
            query += $"&{extraQuery}";
        await Get($"{path}?{query}");
        Shifts = ReadResult<ShiftListItem[]>() ?? [];
    }
}
