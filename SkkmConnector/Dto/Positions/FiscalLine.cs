using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Фискальная строка чека (товар / услуга). Основные поля: Name, Quantity, Price, Sum,
/// Tax, SignMethodCalculation, SignCalculationObject; при необходимости Marking, Agent, Vendor.
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
    /// Признак способа расчёта. Используйте enum <see cref="SignMethodCalculation"/>.
    /// </summary>
    public SignMethodCalculation? SignMethodCalculation { get; set; }

    /// <summary>
    /// Признак предмета расчёта. Используйте enum <see cref="SignCalculationObject"/>.
    /// </summary>
    public SignCalculationObject? SignCalculationObject { get; set; }

    /// <summary>
    /// Единица измерения предмета расчета
    /// </summary>
    public string? MeasurementUnit { get; set; }

    /// <summary>
    /// Мера количества предмета расчёта. Используйте enum <see cref="MeasureOfQuantity"/>.
    /// </summary>
    public MeasureOfQuantity? MeasureOfQuantity { get; set; }

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
    /// Признак агента по предмету расчёта. Используйте enum <see cref="AgentType"/>.
    /// </summary>
    [JsonPropertyName("SignSubjectCalculationAgent")]
    public AgentType? AgentSign { get; set; }

    /// <summary>
    /// Данные агента. Создайте объект <see cref="Agent"/> и заполните нужные поля.
    /// </summary>
    [JsonPropertyName("AgentData")]
    public Agent? Agent { get; set; }

    /// <summary>
    /// Данные поставщика. Создайте объект <see cref="Vendor"/>
    /// (Name, Phones, Vatin).
    /// </summary>
    public Vendor? Vendor { get; set; }

    /// <summary>
    /// Данные кода товарной номенклатуры. Создайте объект <see cref="Marking"/>.
    /// </summary>
    [JsonPropertyName("GoodCodeData")]
    public Marking? Marking { get; set; }

    /// <summary>
    /// Код контрольной марки
    /// </summary>
    public string? MarkingCode { get; set; }

    /// <summary>
    /// Описание частичного выбытия. Создайте объект <see cref="FractionalQuantity"/>
    /// (Numerator, Denominator).
    /// </summary>
    [JsonPropertyName("FractionalQuantity")]
    public FractionalQuantity? Fractional { get; set; }

    /// <summary>
    /// Отраслевой реквизит. Создайте объект <see cref="Industry"/>
    /// (IdentifierFoiv, DocumentDate, DocumentNumber, AttributeValue).
    /// </summary>
    [JsonPropertyName("IndustryAttribute")]
    public Industry? Industry { get; set; }

    /// <summary>
    /// Дополнительный реквизит предмета расчета
    /// </summary>
    public string? AdditionalAttribute { get; set; }
}
