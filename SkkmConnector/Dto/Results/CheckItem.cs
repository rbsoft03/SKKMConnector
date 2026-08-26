using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Позиция сохранённого чека
/// </summary>
public sealed class CheckItem
{
    /// <summary>
    /// Название.
    /// </summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    [JsonPropertyName("Quantity")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Цена позиции.
    /// </summary>
    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Сумма с учётом скидки.
    /// </summary>
    [JsonPropertyName("Sum")]
    public decimal Sum { get; set; }

    /// <summary>
    /// Отдел.
    /// </summary>
    [JsonPropertyName("Department")]
    public int? Department { get; set; }

    /// <summary>
    /// Фискальный режим.
    /// </summary>
    [JsonPropertyName("IsFiscal")]
    public bool IsFiscal { get; set; }

    /// <summary>
    /// Ставка НДС.
    /// </summary>
    [JsonPropertyName("TaxValue")]
    public int TaxValue { get; set; }

    /// <summary>
    /// Сумма НДС.
    /// </summary>
    [JsonPropertyName("TaxSum")]
    public decimal TaxSum { get; set; }

    /// <summary>
    /// Признак способа расчёта
    /// </summary>
    [JsonPropertyName("PaymentMode")]
    public int PaymentMode { get; set; }

    /// <summary>
    /// Признак предмета расчёта (тег 1030 / 1212).
    /// </summary>
    [JsonPropertyName("ItemType")]
    public int ItemType { get; set; }

    /// <summary>
    /// Сумма акциза с учётом копеек, включённая в стоимость предмета расчёта.
    /// </summary>
    [JsonPropertyName("ExciseAmount")]
    public decimal? ExciseAmount { get; set; }

    /// <summary>
    /// Мера количества предмета расчёта.
    /// </summary>
    [JsonPropertyName("MeasureOfQuantity")]
    public int? MeasureOfQuantity { get; set; }

    /// <summary>
    /// Скидка (&gt;0) или наценка (&lt;0).
    /// </summary>
    [JsonPropertyName("DiscountInfoValue")]
    public decimal DiscountInfoValue { get; set; }

    /// <summary>
    /// Дополнительный реквизит предмета расчёта.
    /// </summary>
    [JsonPropertyName("AdditionalAttribute")]
    public string? AdditionalAttribute { get; set; }
}
