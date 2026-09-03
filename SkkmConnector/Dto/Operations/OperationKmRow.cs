using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Строка журнала кодов маркировки операции.
/// </summary>
public sealed class OperationKmRow
{
    /// <summary>
    /// Код маркировки (КиЗ).
    /// </summary>
    [JsonPropertyName("Cis")]
    public string Cis { get; set; } = "";

    /// <summary>
    /// Время проверки кода.
    /// </summary>
    [JsonPropertyName("CheckedAt")]
    public DateTime CheckedAt { get; set; }

    /// <summary>
    /// Код маркировки без крипто-подписи.
    /// </summary>
    [JsonPropertyName("PrintView")]
    public string PrintView { get; set; } = "";

    /// <summary>
    /// Сообщение о результате проверки.
    /// </summary>
    [JsonPropertyName("Message")]
    public string Message { get; set; } = "";

    /// <summary>
    /// Статус проверки кода.
    /// </summary>
    [JsonPropertyName("CheckStatus")]
    public int CheckStatus { get; set; }

    /// <summary>
    /// Наименование позиции чека.
    /// </summary>
    [JsonPropertyName("PositionName")]
    public string PositionName { get; set; } = "";

    /// <summary>
    /// Идентификаторы связанных документов.
    /// </summary>
    [JsonPropertyName("DocIds")]
    public string[] DocIds { get; set; } = [];

    /// <summary>
    /// Цена продажи (в копейках).
    /// </summary>
    [JsonPropertyName("SalePrice")]
    public long SalePrice { get; set; }

    /// <summary>
    /// Имя устройства.
    /// </summary>
    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Идентификатор марки.
    /// </summary>
    [JsonPropertyName("MarkId")]
    public string MarkId { get; set; } = "";

    /// <summary>
    /// Метод проверки кода маркировки.
    /// </summary>
    [JsonPropertyName("KmVerificationMethod")]
    public int KmVerificationMethod { get; set; }

    /// <summary>
    /// Инициатор проверки кода маркировки.
    /// </summary>
    [JsonPropertyName("KmCheckInitiator")]
    public int KmCheckInitiator { get; set; }
}
