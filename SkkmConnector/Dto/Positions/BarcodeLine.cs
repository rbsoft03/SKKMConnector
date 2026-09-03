using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Строка штрихкода в чеке:
/// <para>
/// Type - Тип штрихкода
/// </para>
/// <para>
/// Value - Значение
/// </para>
/// </summary>
public sealed class BarcodeLine : Position
{
    /// <summary>
    /// Тип штрихкода
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// Значение штрихкода
    /// </summary>
    [JsonPropertyName("Value")]
    public string Barcode { get; set; } = "";

    /// <summary>
    /// Значение штрихкода в Base64
    /// </summary>
    public string? ValueBase64 { get; set; }

    /// <summary>
    /// Выравнивание штрихкода
    /// </summary>
    public string? Alignment { get; set; }
}
