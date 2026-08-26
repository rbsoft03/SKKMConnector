using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Элемент списка отчётов
/// </summary>
public sealed class ShiftListItem
{
    /// <summary>
    /// Результат обработки.
    /// </summary>
    [JsonPropertyName("ResultCode")]
    public int ResultCode { get; set; }

    /// <summary>
    /// Описание результата.
    /// </summary>
    [JsonPropertyName("ResultDescription")]
    public string? ResultDescription { get; set; }

    /// <summary>
    /// Дата создания документа.
    /// </summary>
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Идентификатор документа.
    /// </summary>
    [JsonPropertyName("DocId")]
    public string? DocId { get; set; }

    /// <summary>
    /// Номер сессии (смены).
    /// </summary>
    [JsonPropertyName("ShiftNumber")]
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Имя устройства.
    /// </summary>
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    /// <summary>
    /// Идентификатор терминала, с которого пришёл документ.
    /// </summary>
    [JsonPropertyName("TerminalId")]
    public string? TerminalId { get; set; }
}
