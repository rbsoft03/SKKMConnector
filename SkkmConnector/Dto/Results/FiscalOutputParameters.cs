using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Вложенный блок OutputParameters в ответе сервера.
/// </summary>
public sealed class FiscalOutputParameters
{
    /// <summary>
    /// Номер чека за смену.
    /// </summary>
    [JsonPropertyName("NumberOfChecks")]
    public int NumberOfChecks { get; set; }

    /// <summary>
    /// Дата и время ККТ.
    /// </summary>
    [JsonPropertyName("DateTime")]
    public string? DateTime { get; set; }

    /// <summary>
    /// Номер смены.
    /// </summary>
    [JsonPropertyName("ShiftNumber")]
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер фискального документа / чека.
    /// </summary>
    [JsonPropertyName("CheckNumber")]
    public int CheckNumber { get; set; }

    /// <summary>
    /// Остаток наличных в ящике.
    /// </summary>
    [JsonPropertyName("CashBalance")]
    public decimal CashBalance { get; set; }

    /// <summary>
    /// Срок действия ФН.
    /// </summary>
    [JsonPropertyName("FnValidityDate")]
    public string? FnValidityDate { get; set; }

    /// <summary>
    /// Очередь непереданных документов.
    /// </summary>
    [JsonPropertyName("Backlog")]
    public Backlog? Backlog { get; set; }

    /// <summary>
    /// Предупреждения ФН.
    /// </summary>
    [JsonPropertyName("FnWarnings")]
    public Warnings? FnWarnings { get; set; }

    /// <summary>
    /// Остаток ресурса ФН в днях.
    /// </summary>
    [JsonPropertyName("ResourcesFn")]
    public int ResourcesFn { get; set; }
}
