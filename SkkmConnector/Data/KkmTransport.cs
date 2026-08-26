using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// HTTP-транспорт к серверу ККМ. 
    /// </summary>
    internal sealed class KkmTransport : IDisposable
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly JsonSerializerOptions BodyJsonOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private readonly HttpClient _http = CreateHttp();
        private bool _disposed;

        private const string ApiPath = "/PrintService/api/v4/";
        private const string JsonMediaType = "application/json";
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 4398;
        public bool UseHttps { get; set; }
        public string? Token { get; set; }
        public string? TerminalId { get; set; }
        public TimeSpan Timeout { get; set; } = DefaultTimeout;

        internal Task<ResponseResult<JsonElement>> Get(string path)
            => SendAsync(HttpMethod.Get, path, body: null);

        internal Task<ResponseResult<JsonElement>> Post(string path, object? body = null)
            => SendAsync(HttpMethod.Post, path, body);

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _http.Dispose();
        }

        private async Task<ResponseResult<JsonElement>> SendAsync(HttpMethod method, string relativeUrl, object? body)
        {
            if (_disposed)
                return FailResult(-1, "Коннектор закрыт. Создайте новый ServerKkm.");
            if (string.IsNullOrWhiteSpace(Host) || Port is < 1 or > 65535)
                return FailResult(-1, "Укажите Host и Port сервера ККМ.");
            using var request = new HttpRequestMessage(method, RequestUri(relativeUrl));
            AddApiKey(request);

            if (method == HttpMethod.Post && body != null)
            {
                var json = JsonSerializer.Serialize(body, body.GetType(), BodyJsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, JsonMediaType);
            }

            var timeout = Timeout <= TimeSpan.Zero ? DefaultTimeout : Timeout;
            using var cts = new CancellationTokenSource(timeout);

            try
            {
                using var response = await _http.SendAsync(request, cts.Token);
                var responseBody = await response.Content.ReadAsStringAsync(cts.Token);
                var statusCode = (int)response.StatusCode;

                if (string.IsNullOrWhiteSpace(responseBody))
                    return FailResult(statusCode, DescribeHttpError(statusCode, response.StatusCode.ToString()));

                try
                {
                    var parsed = JsonSerializer.Deserialize<ResponseResult<JsonElement>>(responseBody, JsonOptions);
                    if (parsed != null)
                        return parsed;
                }
                catch (JsonException)
                {
                }

                return FailResult(statusCode, DescribeHttpError(statusCode, "некорректный ответ сервера"));
            }
            catch (HttpRequestException ex)
            {
                return FailResult(-1, $"Ошибка соединения: {ex.Message}");
            }
            catch (OperationCanceledException)
            {
                return FailResult(-2, "Превышено время ожидания ответа сервера");
            }
        }

        private Uri RequestUri(string relativeUrl)
        {
            var queryStart = relativeUrl.IndexOf('?');
            var path = queryStart < 0 ? relativeUrl : relativeUrl[..queryStart];
            var query = queryStart < 0 ? "" : relativeUrl[(queryStart + 1)..];

            return new UriBuilder
            {
                Scheme = UseHttps ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                Host = Host,
                Port = Port,
                Path = $"{ApiPath.TrimEnd('/')}/{path.TrimStart('/')}",
                Query = query
            }.Uri;
        }

        private static HttpClient CreateHttp()
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1)
            };

            return new HttpClient(handler)
            {
                Timeout = System.Threading.Timeout.InfiniteTimeSpan
            };
        }

        private static ResponseResult<JsonElement> FailResult(int code, string description)
            => new() { Success = false, Code = code, Description = description };

        private static string DescribeHttpError(int statusCode, string fallback)
            => statusCode switch
            {
                401 => "Ошибка авторизации. Укажите токен или включите анонимный доступ на сервере ККМ.",
                403 => "Доступ запрещён. Проверьте токен API.",
                _ => $"Ошибка HTTP {statusCode}: {fallback}"
            };

        private void AddApiKey(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(Token))
                request.Headers.TryAddWithoutValidation("api_key", Token);

            if (!string.IsNullOrEmpty(TerminalId))
                request.Headers.TryAddWithoutValidation("TerminalId", TerminalId);
        }
    }
}
