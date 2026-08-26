using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Строка нефискального документа
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
        public BarcodeLine? Barcode { get; set; }

        /// <summary>
        /// Печать картинки (Base64)
        /// </summary>
        [JsonPropertyName("Picture")]
        public PictureLine? Picture { get; set; }

        /// <summary>
        /// Горизонтальная разделительная линия на всю ширину чека
        /// </summary>
        [JsonPropertyName("SeparatorLine")]
        public SeparatorLine? SeparatorLine { get; set; }
    }
}
