using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Внутренний HTTP-транспорт к серверу ККМ.
    /// вызывается только из ServerKkm.
    /// </summary>
    internal class KkmTransport : IDisposable
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

        /// <summary>
        /// Путь API сервера ККМ.
        /// </summary>
        private const string ApiPath = "/PrintService/api/v4/";

        private const string JsonMediaType = "application/json";

        private readonly HttpClient _http;

        public string Host { get; }
        public int Port { get; }

        /// <summary>
        /// Токен авторизации (заголовок api_key)
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Идентификатор терминала (заголовок TerminalId).
        /// Необязательный, применяется для идентификации рабочего места
        /// </summary>
        public string? TerminalId { get; set; }

        /// <summary>
        /// Тело последнего ответа сервера — для консоли
        /// </summary>
        public string? LastResponseBody { get; private set; }
        public string? LastRequestInfo { get; private set; }

        /// <summary>
        /// Тело последнего запроса (JSON), если было
        /// </summary>
        public string? LastRequestBody { get; private set; }

        /// <summary>
        /// Заголовки последнего запроса (пары ключ-значение) — для отображения в консоли
        /// </summary>
        public IReadOnlyList<KeyValuePair<string, string>> LastRequestHeaders { get; private set; }
            = Array.Empty<KeyValuePair<string, string>>();

        /// <summary>
        /// Длительность последнего запроса, мс
        /// </summary>
        public long LastDurationMs { get; private set; }

        /// <summary>
        /// HTTP-статус последнего ответа
        /// </summary>
        public int LastStatusCode { get; private set; }

        public KkmTransport(string host, int port)
        {
            Host = host;
            Port = port;
            _http = new HttpClient
            {
                BaseAddress = new Uri($"http://{host}:{port}{ApiPath}"),
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        // GET-запрос без тела: например получить статус, список устройств, чек по id.
        internal Task<T> Get<T>(string path) where T : ResponseResultBase, new()
        {
            return SendAsync<T>(HttpMethod.Get, path, body: null);
        }

        // POST-запрос с телом: например напечатать чек, открыть/закрыть смену.
        internal Task<T> Post<T>(string path, object? body = null) where T : ResponseResultBase, new()
        {
            return SendAsync<T>(HttpMethod.Post, path, body);
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string relativeUrl, object? body = null)
            where T : ResponseResultBase, new()
        {
            using var request = new HttpRequestMessage(method, relativeUrl);

            // Content-Type: application/json отправляем на всех запросах (как в коллекции РБ-Софт),
            // в том числе на GET без тела. Для этого всегда прикрепляем содержимое:
            // JSON-тело для POST/PUT или пустое содержимое-носитель заголовка для GET/DELETE.
            LastRequestBody = body != null ? JsonSerializer.Serialize(body, body.GetType(), BodyJsonOptions) : null;
            var content = new StringContent(LastRequestBody ?? string.Empty, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(JsonMediaType);
            request.Content = content;

            AddApiKey(request);

            LastRequestInfo = $"{method} {_http.BaseAddress}{relativeUrl}";
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                using var response = await _http.SendAsync(request);
                LastResponseBody = await response.Content.ReadAsStringAsync();
                stopwatch.Stop();
                LastDurationMs = stopwatch.ElapsedMilliseconds;
                LastStatusCode = (int)response.StatusCode;

                if (string.IsNullOrWhiteSpace(LastResponseBody))
                {
                    var empty = FailResult<T>(LastStatusCode, DescribeHttpError(LastStatusCode, response.StatusCode.ToString()));
                    LastResponseBody = ToErrorJson(empty);
                    return empty;
                }

                try
                {
                    var parsed = JsonSerializer.Deserialize<T>(LastResponseBody, JsonOptions);
                    if (parsed != null)
                        return parsed;
                }
                catch (JsonException)
                {
                    // Ответ не JSON (часто HTML при 401) — сформируем понятную ошибку для консоли
                }

                var fail = FailResult<T>(LastStatusCode, DescribeHttpError(LastStatusCode, "некорректный ответ сервера"));
                LastResponseBody = ToErrorJson(fail);
                return fail;
            }
            catch (HttpRequestException ex)
            {
                stopwatch.Stop();
                LastDurationMs = stopwatch.ElapsedMilliseconds;
                LastStatusCode = 0;
                var fail = FailResult<T>(0, $"Ошибка соединения: {ex.Message}");
                LastResponseBody = ToErrorJson(fail);
                return fail;
            }
            catch (TaskCanceledException)
            {
                stopwatch.Stop();
                LastDurationMs = stopwatch.ElapsedMilliseconds;
                LastStatusCode = 0;
                var fail = FailResult<T>(0, "Превышено время ожидания ответа сервера");
                LastResponseBody = ToErrorJson(fail);
                return fail;
            }
            finally
            {
                stopwatch.Stop();
            }
        }

        private static T FailResult<T>(int code, string description) where T : ResponseResultBase, new()
            => new() { Success = false, Code = code, Description = description };

        private static string ToErrorJson(ResponseResultBase result) =>
            JsonSerializer.Serialize(new
            {
                result.Success,
                result.Code,
                result.Description
            });

        private static string DescribeHttpError(int statusCode, string fallback)
            => statusCode switch
            {
                401 => "Ошибка авторизации. Укажите токен или включите анонимный доступ на сервере ККМ.",
                403 => "Доступ запрещён. Проверьте токен API.",
                _ => $"Ошибка HTTP {statusCode}: {fallback}"
            };

        private void AddApiKey(HttpRequestMessage request)
        {
            // Реально отправляем заголовки только с непустыми значениями
            if (!string.IsNullOrEmpty(Token))
                request.Headers.Add("api_key", Token);

            if (!string.IsNullOrEmpty(TerminalId))
                request.Headers.Add("TerminalId", TerminalId);

            // Content-Type уходит всегда как application/json (без charset)
            LastRequestHeaders = new List<KeyValuePair<string, string>>
            {
                new("Content-Type", JsonMediaType),
                new("api_key", Token ?? ""),
                new("TerminalId", TerminalId ?? "")
            };
        }

        public void Dispose() => _http.Dispose();
    }
}
