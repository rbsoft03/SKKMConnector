using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Разделительная линия в чеке:
/// <para>
/// LineStyle - Стиль. Используйте enum <see cref="LineStyle"/>
/// </para>
/// </summary>
public sealed class SeparatorLine : Position
{
    /// <summary>
    /// Стиль разделительной линии. Используйте enum <see cref="LineStyle"/>.
    /// </summary>
    [JsonPropertyName("lineStyle")]
    public LineStyle LineStyle { get; set; } = LineStyle.Solid;
}
