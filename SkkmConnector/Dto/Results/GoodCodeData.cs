using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Код товара (маркировки) позиции.
/// </summary>
public sealed class GoodCodeData
{
    /// <summary>
    /// Код маркировки (КиЗ/CIS).
    /// </summary>
    [JsonPropertyName("MarkingCode")]
    public string? MarkingCode { get; set; }
}
