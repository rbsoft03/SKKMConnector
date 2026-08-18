using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ GET version — версия сервера ККМ
    /// </summary>
    internal class ResServerVersion
    {
        /// <summary>
        /// Версия сервера ККМ
        /// </summary>
        [JsonPropertyName("ServerVersion")]
        public string? ServerVersion { get; set; }
    }
}
