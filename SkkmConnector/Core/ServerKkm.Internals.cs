using System.Text.Json;
using SkkmConnector.Internal;

namespace SkkmConnector;

// Транспортная инфраструктура: выбор соединения, вызов и разбор ответа.
public sealed partial class ServerKkm
{
    private const int DefaultPort = 4398;

    private async Task Exec(Func<KkmTransport, Task<ResponseResult<JsonElement>>> operation)
    {
        var result = await operation(Transport());
        Apply(result);
    }

    private Task Exec(Func<KkmTransport, Task> operation) => operation(Transport());

    private KkmTransport Transport()
    {
        var connection = Connection();
        if (_http == null || _http.Host != connection.Host || _http.Port != connection.Port)
        {
            _http?.Dispose();
            _http = new KkmTransport(connection.Host, connection.Port);
        }

        _http.Token = Token;
        _http.TerminalId = TerminalId;
        return _http;
    }

    private (string Host, int Port) Connection()
        => Host.Contains(":")
            ? ParseServerAddress(Host)
            : (string.IsNullOrWhiteSpace(Host) ? "localhost" : Host.Trim(), Port > 0 ? Port : DefaultPort);

    /// <summary>
    /// Разбор адреса сервера ККМ: host или host:port.
    /// </summary>
    private static (string Host, int Port) ParseServerAddress(string? address)
    {
        address = (address ?? "").Trim();
        if (address.Length == 0)
            return ("localhost", DefaultPort);

        int colon = address.IndexOf(':');
        if (colon <= 0)
            return (address, DefaultPort);

        string host = address.Substring(0, colon).Trim();
        if (host.Length == 0)
            host = "localhost";

        if (!int.TryParse(address.Substring(colon + 1).Trim(), out int port) || port <= 0 || port > 65535)
            port = DefaultPort;

        return (host, port);
    }

    private void Apply<T>(ResponseResult<T> result)
    {
        Ok = result.Success;
        ErrorCode = result.Code;
        ErrorDescription = result.Description ?? "";
        if (_http != null)
        {
            LastStatusCode = _http.LastStatusCode;
            LastDurationMs = _http.LastDurationMs;
            LastRequestInfo = _http.LastRequestInfo;
            LastRequestBody = _http.LastRequestBody;
            LastResponseBody = _http.LastResponseBody;
            LastRequestHeaders = _http.LastRequestHeaders;
        }

        LastResult = result.Result is JsonElement element ? element : default;
        ExtractFiscalResult(LastResult);
    }

    // Раскладывает результат фискальной операции из ответа: заполняет FiscalResult
    // и переносит ключевые значения в плоские свойства (как это делает 1С после печати).
    private void ExtractFiscalResult(JsonElement result)
    {
        if (result.ValueKind != JsonValueKind.Object)
            return;

        FiscalResult? fiscal;
        try
        {
            fiscal = result.Deserialize<FiscalResult>();
        }
        catch (JsonException)
        {
            return;
        }

        if (fiscal == null)
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
}
