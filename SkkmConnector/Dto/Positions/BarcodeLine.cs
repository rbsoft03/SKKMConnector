namespace SkkmConnector;

/// <summary>
/// Штрихкод
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
    public string Barcode { get; set; } = "";

    /// <summary>
    /// Значение штрихкода в Base64. Если заданы и текст, и Base64 — сервер берёт Base64
    /// </summary>
    public string? ValueBase64 { get; set; }

    /// <summary>
    /// Выравнивание штрихкода
    /// </summary>
    public string? Alignment { get; set; }
}
