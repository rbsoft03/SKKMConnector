using System.Globalization;
using System.Text.Json;
using SkkmConnector.Internal;

namespace SkkmConnector;

// Транспортная инфраструктура: выбор соединения, вызов и разбор ответа.
public sealed partial class ServerKkm
{
    private string DeviceQuery => $"device={Uri.EscapeDataString(DeviceName)}";
    private string IdQuery => $"id={Uri.EscapeDataString(DocumentId)}";

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

    // Раскладывает результат фискальной операции из ответа: заполняет FiscalResult
    // и переносит ключевые значения в плоские свойства 
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
        if (string.IsNullOrEmpty(fiscal.FiscalSign)
            && fiscal.FiscalNumber == 0
            && fiscal.ShiftNumber == 0
            && string.IsNullOrEmpty(fiscal.DocId))
            return;

        FiscalResult = fiscal;

        // Обновляем плоские свойства только если сервер реально вернул значение,
        // чтобы не затирать их нулями на ответах без фискальных полей.
        if (!string.IsNullOrEmpty(fiscal.FiscalSign))
            FiscalSign = fiscal.FiscalSign!;
        if (fiscal.FiscalNumber > 0)
            CheckNumber = fiscal.FiscalNumber;
        if (fiscal.ShiftNumber > 0)
            ShiftNumber = fiscal.ShiftNumber;
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

        FiscalResult = new FiscalResult
        {
            DateTime = document.Date.ToString("o", CultureInfo.InvariantCulture),
            DeviceName = document.DeviceName,
            DocId = document.DocId,
            FnsUrl = document.DocumentHeader?.FnsUrl,
            FnNumber = document.DocumentHeader?.Fn,
            RnNumber = document.DocumentHeader?.RnNumber,
            FiscalDateTime = document.FiscalDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
            FiscalSign = document.FiscalSign,
            ShiftNumber = document.ShiftNumber,
            FiscalNumber = document.DocNumber
        };
    }

    private async Task Get(string path)
    {
        Apply(await Transport().Get(path));
    }

    private async Task Post(string path, object? body = null)
    {
        Apply(await Transport().Post(path, body));
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
    private async Task GetReportList(string path)
    {
        var from = ShiftsFrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var to = ShiftsTo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        await Get($"{path}?{DeviceQuery}&from={from}&to={to}");
        Shifts = ReadResult<ShiftListItem[]>() ?? [];
    }
}
