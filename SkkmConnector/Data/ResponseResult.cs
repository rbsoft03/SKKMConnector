using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ Сервера ККМ с полезной нагрузкой в поле Result.
    /// </summary>
    internal class ResponseResult<T> : ResponseResultBase
    {
        [JsonPropertyName("Result")]
        public T? Result { get; set; }
    }
}
