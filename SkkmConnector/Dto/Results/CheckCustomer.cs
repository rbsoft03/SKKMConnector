using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Сведения о покупателе из ответа
/// </summary>
public sealed class CheckCustomer
{
    /// <summary>
    /// Наименование организации или фамилия, имя, отчество (при наличии).
    /// </summary>
    [JsonPropertyName("Info")]
    public string? Info { get; set; }

    /// <summary>
    /// ИНН организации или покупателя (клиента).
    /// </summary>
    [JsonPropertyName("Inn")]
    public string? Inn { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    [JsonPropertyName("Email")]
    public string? Email { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    [JsonPropertyName("Phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Дата рождения покупателя
    /// </summary>
    [JsonPropertyName("DateOfBirth")]
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// Код страны (ОКСМ).
    /// </summary>
    [JsonPropertyName("Citizenship")]
    public string? Citizenship { get; set; }

    /// <summary>
    /// Числовой код вида документа, удостоверяющего личность.
    /// </summary>
    [JsonPropertyName("DocumentTypeCode")]
    public int? DocumentTypeCode { get; set; }

    /// <summary>
    /// Данные документа, удостоверяющего личность.
    /// </summary>
    [JsonPropertyName("DocumentData")]
    public string? DocumentData { get; set; }

    /// <summary>
    /// Адрес покупателя.
    /// </summary>
    [JsonPropertyName("Address")]
    public string? Address { get; set; }
}
