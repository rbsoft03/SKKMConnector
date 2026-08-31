using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Разделительная линия
/// </summary>
public sealed class SeparatorLine : Position
{
    /// <summary>
    /// Стиль разделительной линии
    /// </summary>
    [JsonPropertyName("lineStyle")]
    public LineStyle LineStyle { get; set; } = LineStyle.Solid;
}
