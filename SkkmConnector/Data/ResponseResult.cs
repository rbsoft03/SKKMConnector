using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ Сервера ККМ
    /// </summary>
    internal class ResponseResult<T> : ResponseResultBase
    {
        /// <summary>
        /// Полезная нагрузка ответа.
        /// </summary>
        [JsonPropertyName("Result")]
        public T? Result { get; set; }
    }
}
