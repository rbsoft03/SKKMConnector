using System.Net.Http.Headers;
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
        public string? BasicAuthUser { get; set; }
        public string? BasicAuthPassword { get; set; }
        public TimeSpan Timeout { get; set; } = DefaultTimeout;

        internal Task<ResponseResult<JsonElement>> Get(string path, bool useBasicAuth = false, CancellationToken cancellationToken = default)
            => SendAsync(HttpMethod.Get, path, body: null, useBasicAuth, cancellationToken);

        internal Task<ResponseResult<JsonElement>> Post(string path, object? body = null, CancellationToken cancellationToken = default)
            => SendAsync(HttpMethod.Post, path, body, cancellationToken: cancellationToken);

        internal Task<ResponseResult<JsonElement>> Put(string path, object? body = null, CancellationToken cancellationToken = default)
            => SendAsync(HttpMethod.Put, path, body, cancellationToken: cancellationToken);

        internal Task<ResponseResult<JsonElement>> Delete(string path, CancellationToken cancellationToken = default)
            => SendAsync(HttpMethod.Delete, path, body: null, cancellationToken: cancellationToken);

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _http.Dispose();
        }

        private async Task<ResponseResult<JsonElement>> SendAsync(
            HttpMethod method,
            string relativeUrl,
            object? body,
            bool useBasicAuth = false,
            CancellationToken cancellationToken = default)
        {
            if (_disposed)
                return FailResult(-1, "Коннектор закрыт. Создайте новый ServerKkm.");
            if (string.IsNullOrWhiteSpace(Host) || Port is < 1 or > 65535)
                return FailResult(-1, "Укажите Host и Port сервера ККМ.");

            using var request = new HttpRequestMessage(method, RequestUri(relativeUrl));
            if (useBasicAuth)
                AddBasicAuth(request);
            else
                AddApiKey(request);

            if (body != null && method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                var json = JsonSerializer.Serialize(body, body.GetType(), BodyJsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, JsonMediaType);
            }

            var timeout = Timeout <= TimeSpan.Zero ? DefaultTimeout : Timeout;
            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, cancellationToken);

            try
            {
                using var response = await _http.SendAsync(request, linkedCts.Token);
                var responseBody = await response.Content.ReadAsStringAsync(linkedCts.Token);
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
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                return FailResult(-3, "Запрос отменён");
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

        private void AddBasicAuth(HttpRequestMessage request)
        {
            var user = BasicAuthUser ?? "";
            var password = BasicAuthPassword ?? "";
            var bytes = Encoding.UTF8.GetBytes($"{user}:{password}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
        }
    }
}
