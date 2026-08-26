using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Фискальная строка чека
/// </summary>
public sealed class FiscalLine : Position
{
    /// <summary>
    /// Наименование товара
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Код товара
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public decimal Quantity { get; set; } = 1;

    /// <summary>
    /// Цена единицы товара с учетом скидок/наценок
    /// </summary>
    [JsonPropertyName("PriceWithDiscount")]
    public decimal Price { get; set; }

    /// <summary>
    /// Конечная сумма по позиции чека с учетом всех скидок/наценок
    /// </summary>
    [JsonPropertyName("SumWithDiscount")]
    public decimal Sum { get; set; }

    /// <summary>
    /// Сумма скидок и наценок
    /// </summary>
    public decimal DiscountSum { get; set; }

    /// <summary>
    /// Ставка НДС. Обязательна: сервер отклоняет позицию без ставки
    /// </summary>
    public string Tax { get; set; } = "";

    /// <summary>
    /// Сумма НДС за предмет расчета
    /// </summary>
    public decimal TaxSum { get; set; }

    /// <summary>
    /// Отдел, по которому ведется продажа
    /// </summary>
    public int Department { get; set; }

    /// <summary>
    /// Признак способа расчета
    /// </summary>
    public int? SignMethodCalculation { get; set; }

    /// <summary>
    /// Признак предмета расчета
    /// </summary>
    public int? SignCalculationObject { get; set; }

    /// <summary>
    /// Единица измерения предмета расчета
    /// </summary>
    public string? MeasurementUnit { get; set; }

    /// <summary>
    /// Мера количества предмета расчета
    /// </summary>
    public int? MeasureOfQuantity { get; set; }

    /// <summary>
    /// Сумма акциза с учетом копеек
    /// </summary>
    public decimal? ExciseAmount { get; set; }

    /// <summary>
    /// Цифровой код страны происхождения товара
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// Регистрационный номер таможенной декларации
    /// </summary>
    public string? CustomsDeclaration { get; set; }

    /// <summary>
    /// Признак агента по предмету расчета
    /// </summary>
    [JsonPropertyName("SignSubjectCalculationAgent")]
    public int? AgentSign { get; set; }

    /// <summary>
    /// Данные агента
    /// </summary>
    [JsonPropertyName("AgentData")]
    public Agent? Agent { get; set; }

    /// <summary>
    /// Данные поставщика
    /// </summary>
    public Vendor? Vendor { get; set; }

    /// <summary>
    /// Данные кода товарной номенклатуры
    /// </summary>
    [JsonPropertyName("GoodCodeData")]
    public Marking? Marking { get; set; }

    /// <summary>
    /// Код контрольной марки
    /// </summary>
    public string? MarkingCode { get; set; }

    /// <summary>
    /// Описание частичного выбытия
    /// </summary>
    [JsonPropertyName("FractionalQuantity")]
    public FractionalQuantity? Fractional { get; set; }

    /// <summary>
    /// Отраслевой реквизит
    /// </summary>
    [JsonPropertyName("IndustryAttribute")]
    public Industry? Industry { get; set; }

    /// <summary>
    /// Дополнительный реквизит предмета расчета
    /// </summary>
    public string? AdditionalAttribute { get; set; }
}
