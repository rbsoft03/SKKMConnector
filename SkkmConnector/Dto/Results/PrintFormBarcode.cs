using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Штрихкод в печатной форме
/// </summary>
public sealed class PrintFormBarcode
{
    /// <summary>
    /// Тип штрихкода: UPCA, CODE39, EAN13, EAN8, UPCE, ITF, CODABAR, CODE93, CODE128, PDF417, CODE32, QR.
    /// </summary>
    [JsonPropertyName("Type")]
    public string? Type { get; set; }

    /// <summary>
    /// Значение штрихкода.
    /// </summary>
    [JsonPropertyName("Value")]
    public string? Value { get; set; }

    /// <summary>
    /// Изображение штрихкода, закодированное в Base64.
    /// </summary>
    [JsonPropertyName("PictureBase64")]
    public string? PictureBase64 { get; set; }

    /// <summary>
    /// Способ печати текста штрихкода (только для одномерных): 0 — не печатать; 1 — снизу; 2 — сверху; 3 — сверху и снизу. Если не указано — 0.
    /// </summary>
    [JsonPropertyName("PrintText")]
    public int PrintText { get; set; }

    /// <summary>
    /// Высота штрихкода в точках. Допустимые значения: 0..1199.
    /// </summary>
    [JsonPropertyName("Height")]
    public int Height { get; set; }

    /// <summary>
    /// Ширина штриха в точках. Допустимые значения: 0..1199. Рекомендуемое значение — 2.
    /// </summary>
    [JsonPropertyName("BarWidth")]
    public int BarWidth { get; set; }
}
