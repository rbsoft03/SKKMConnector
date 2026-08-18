using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати нефискального документа: POST slip / POST slip/async.
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
