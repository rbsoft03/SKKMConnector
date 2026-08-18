using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Строка нефискального документа: текст или штрихкод.
    /// </summary>
    internal class DocPosition
    {
        /// <summary>
        /// Печать текстовой строки
        /// </summary>
        [JsonPropertyName("TextString")]
        public TextString? TextString { get; set; }

        /// <summary>
        /// Печать штрихкода
        /// </summary>
        [JsonPropertyName("Barcode")]
        public Barcode? Barcode { get; set; }

        /// <summary>
        /// Горизонтальная разделительная линия на всю ширину чека
        /// </summary>
        [JsonPropertyName("SeparatorLine")]
        public SeparatorLine? SeparatorLine { get; set; }
    }
}
