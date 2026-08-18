using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Печать текстовой строки в документе.
    /// </summary>
    internal class TextString
    {
        /// <summary>
        /// Строка с произвольным текстом
        /// </summary>
        [JsonPropertyName("Text")]
        public string? Text { get; set; }

        /// <summary>
        /// Шрифт строки: Normal, Bold, Small, Medium, Big, H1, H2, H3, H4, H5
        /// </summary>
        [JsonPropertyName("Font")]
        public string? Font { get; set; }

        /// <summary>
        /// Выравнивание: left, right, center, width
        /// </summary>
        [JsonPropertyName("Alignment")]
        public string? Alignment { get; set; }
    }
}
