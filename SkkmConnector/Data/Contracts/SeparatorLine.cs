using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Разделительная линия на всю ширину чека в нефискальном документе
    /// </summary>
    internal class SeparatorLine
    {
        /// <summary>
        /// Стиль линии
        /// </summary>
        [JsonPropertyName("lineStyle")]
        public LineStyle LineStyle { get; set; } = LineStyle.Solid;
    }
}
