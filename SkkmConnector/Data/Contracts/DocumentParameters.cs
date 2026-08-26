using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати нефискального документа.
    /// </summary>
    internal class DocumentParameters : CheckbaseParameters
    {
        /// <summary>
        /// Строки документа
        /// </summary>
        [JsonPropertyName("Positions")]
        public DocPosition[]? Positions { get; set; }
    }
}
