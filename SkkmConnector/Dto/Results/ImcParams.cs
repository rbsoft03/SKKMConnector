using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Параметры проверки кода маркировки позиции.
/// </summary>
public sealed class ImcParams
{
    /// <summary>
    /// Тип кода маркировки.
    /// </summary>
    [JsonPropertyName("imcType")]
    public string? ImcType { get; set; }

    /// <summary>
    /// Код маркировки.
    /// </summary>
    [JsonPropertyName("imc")]
    public string? Imc { get; set; }

    /// <summary>
    /// Планируемый статус товара.
    /// </summary>
    [JsonPropertyName("itemEstimatedStatus")]
    public string? ItemEstimatedStatus { get; set; }

    /// <summary>
    /// Режим обработки кода маркировки.
    /// </summary>
    [JsonPropertyName("imcModeProcessing")]
    public int ImcModeProcessing { get; set; }

    /// <summary>
    /// Результат проверки сведений о товаре.
    /// </summary>
    [JsonPropertyName("itemInfoCheckResult")]
    public ItemInfoCheckResult? ItemInfoCheckResult { get; set; }
}

/// <summary>
/// Результат проверки кода маркировки.
/// </summary>
public sealed class ItemInfoCheckResult
{
    /// <summary>
    /// Признак выполнения проверки КМ.
    /// </summary>
    [JsonPropertyName("imcCheckFlag")]
    public bool ImcCheckFlag { get; set; }

    /// <summary>
    /// Результат проверки КМ.
    /// </summary>
    [JsonPropertyName("imcCheckResult")]
    public bool ImcCheckResult { get; set; }

    /// <summary>
    /// Корректность статуса информации о КМ.
    /// </summary>
    [JsonPropertyName("imcStatusInfo")]
    public bool ImcStatusInfo { get; set; }

    /// <summary>
    /// Корректность планируемого статуса товара.
    /// </summary>
    [JsonPropertyName("imcEstimatedStatusCorrect")]
    public bool ImcEstimatedStatusCorrect { get; set; }

    /// <summary>
    /// Признак автономного режима ККТ при проверке.
    /// </summary>
    [JsonPropertyName("ecrStandAloneFlag")]
    public bool EcrStandAloneFlag { get; set; }
}
