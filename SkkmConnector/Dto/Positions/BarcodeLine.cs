using System.Text.Json.Serialization;

namespace RBSoftSkkm;

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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BarcodeType Type { get; set; } = BarcodeType.QR;

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
    /// Если не задано — по центру.
    /// </summary>
    [JsonIgnore]
    public PrintAlignment? Alignment { get; set; }

    /// <summary>
    /// Выравнивание в том виде, в каком его ждёт сервер: left, right, center.
    /// </summary>
    [JsonPropertyName("Alignment")]
    public string? AlignmentValue => Alignment?.ToString().ToLowerInvariant();

    /// <summary>
    /// Высота штрихкода в точках. 0 — по умолчанию устройства.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Ширина штриха в точках. 0 — по умолчанию устройства.
    /// </summary>
    public int BarWidth { get; set; }

    /// <summary>
    /// Печать текста под/над штрихкодом (только для одномерных).
    /// </summary>
    public BarcodePrintText PrintText { get; set; }
}
