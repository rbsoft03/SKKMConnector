using System.Text.Json;
using System.Text.Json.Serialization;

namespace RBSoftSkkm;

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
    /// Количество документов в смене.
    /// </summary>
    [JsonPropertyName("NumberOfDocuments")]
    public int NumberOfDocuments { get; set; }

    /// <summary>
    /// Номер чека закрытия смены.
    /// </summary>
    [JsonPropertyName("ShiftClosingCheckNumber")]
    public int ShiftClosingCheckNumber { get; set; }

    /// <summary>
    /// Состояние смены.
    /// </summary>
    [JsonPropertyName("ShiftState")]
    public ShiftState? ShiftState { get; set; }

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

    /// <summary>
    /// Предупреждения ФН (так поле называется в сохранённых документах: GET shift/open, shift/x, shift/z, report/settlement).
    /// </summary>
    [JsonPropertyName("Warnings")]
    public Warnings? Warnings { get; set; }

    /// <summary>
    /// Количество непереданных в ОФД документов.
    /// </summary>
    [JsonPropertyName("DocumentsCounter")]
    public long DocumentsCounter { get; set; }

    /// <summary>
    /// Показатели отделов за смену.
    /// </summary>
    [JsonPropertyName("DepartmentTotals")]
    public JsonElement[]? DepartmentTotals { get; set; }
}
