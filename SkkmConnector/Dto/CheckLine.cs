namespace SkkmConnector;

/// <summary>
/// Строка чека: фискальная позиция (участвует в сумме) или нефискальный текст/штрихкод.
/// Обычно добавляется через <see cref="ServerKkm.AddFiscalLine"/> или <see cref="ServerKkm.AddNonFiscalLine"/>.
/// </summary>
public sealed class CheckLine
{
    /// <summary>
    /// Фискальная товарная строка. Иначе — текст или штрихкод.
    /// </summary>
    public bool IsFiscalLine { get; set; } = true;

    /// <summary>
    /// Наименование товара. Для фискальной строки обязательно.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Количество.
    /// </summary>
    public decimal Quantity { get; set; } = 1;

    /// <summary>
    /// Единица измерения.
    /// </summary>
    public string MeasurementUnit { get; set; } = "шт";

    /// <summary>
    /// Цена с учётом скидки.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Сумма позиции. 0 — считается как количество × цена.
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Сумма скидки.
    /// </summary>
    public decimal DiscountSum { get; set; }

    /// <summary>
    /// Ставка НДС: none, 0, 10, 20, 22, 10/110, 20/120, 22/122.
    /// </summary>
    public string Tax { get; set; } = "none";

    /// <summary>
    /// Сумма НДС.
    /// </summary>
    public decimal TaxSum { get; set; }

    /// <summary>
    /// Секция / отдел.
    /// </summary>
    public int Department { get; set; }

    /// <summary>
    /// Признак предмета расчёта. По умолчанию 4 — услуга.
    /// </summary>
    public int SignCalculationObject { get; set; } = 4;

    /// <summary>
    /// Признак способа расчёта. По умолчанию 1 — предоплата полная.
    /// </summary>
    public int SignMethodCalculation { get; set; } = 1;

    /// <summary>
    /// Сумма акциза.
    /// </summary>
    public decimal ExciseAmount { get; set; }

    /// <summary>
    /// Код страны происхождения.
    /// </summary>
    public string CountryOfOrigin { get; set; } = "";

    /// <summary>
    /// Номер таможенной декларации.
    /// </summary>
    public string CustomsDeclaration { get; set; } = "";

    /// <summary>
    /// Признак агента по предмету расчёта. Нужен, если заполнены данные агента или поставщика.
    /// </summary>
    public int AgentSign { get; set; }

    /// <summary>
    /// Операция платёжного агента.
    /// </summary>
    public string PayingAgentOperation { get; set; } = "";

    /// <summary>
    /// Телефон платёжного агента.
    /// </summary>
    public string PayingAgentPhone { get; set; } = "";

    /// <summary>
    /// Телефон оператора по приёму платежей.
    /// </summary>
    public string ReceivePaymentsOperatorPhone { get; set; } = "";

    /// <summary>
    /// Телефон оператора перевода.
    /// </summary>
    public string MoneyTransferOperatorPhone { get; set; } = "";

    /// <summary>
    /// Наименование оператора перевода.
    /// </summary>
    public string MoneyTransferOperatorName { get; set; } = "";

    /// <summary>
    /// Адрес оператора перевода.
    /// </summary>
    public string MoneyTransferOperatorAddress { get; set; } = "";

    /// <summary>
    /// ИНН оператора перевода.
    /// </summary>
    public string MoneyTransferOperatorVatin { get; set; } = "";

    /// <summary>
    /// Наименование поставщика.
    /// </summary>
    public string PurveyorName { get; set; } = "";

    /// <summary>
    /// Телефон поставщика.
    /// </summary>
    public string PurveyorPhone { get; set; } = "";

    /// <summary>
    /// ИНН поставщика.
    /// </summary>
    public string PurveyorVatin { get; set; } = "";

    /// <summary>
    /// GTIN маркированного товара.
    /// </summary>
    public string Gtin { get; set; } = "";

    /// <summary>
    /// Серийный номер маркированного товара.
    /// </summary>
    public string SerialNumber { get; set; } = "";

    /// <summary>
    /// Тип / группа товара.
    /// </summary>
    public string CommodityGroup { get; set; } = "";

    /// <summary>
    /// КИЗ (штрихкод маркировки).
    /// </summary>
    public string Kiz { get; set; } = "";

    /// <summary>
    /// Код маркировки в Base64.
    /// </summary>
    public string KizBase64 { get; set; } = "";

    /// <summary>
    /// Мера количества предмета расчёта (таблица 114 ФФД).
    /// </summary>
    public int MeasureOfQuantity { get; set; }

    /// <summary>
    /// Числитель дробного количества маркированного товара.
    /// </summary>
    public int FractionalNumerator { get; set; }

    /// <summary>
    /// Знаменатель дробного количества маркированного товара.
    /// </summary>
    public int FractionalDenominator { get; set; }

    /// <summary>
    /// Дополнительный реквизит предмета расчёта.
    /// </summary>
    public string AdditionalAttribute { get; set; } = "";

    /// <summary>
    /// Идентификатор ФОИВ отраслевого реквизита.
    /// </summary>
    public string IndustryFoiv { get; set; } = "";

    /// <summary>
    /// Дата документа отраслевого реквизита.
    /// </summary>
    public string IndustryDocumentDate { get; set; } = "";

    /// <summary>
    /// Номер документа отраслевого реквизита.
    /// </summary>
    public string IndustryDocumentNumber { get; set; } = "";

    /// <summary>
    /// Значение отраслевого реквизита.
    /// </summary>
    public string IndustryAttributeValue { get; set; } = "";

    /// <summary>
    /// Дополнительный текст после позиции.
    /// </summary>
    public string ExtraText { get; set; } = "";

    /// <summary>
    /// Тип штрихкода (QR, EAN13, CODE128, …).
    /// </summary>
    public string BarcodeType { get; set; } = "";

    /// <summary>
    /// Значение штрихкода.
    /// </summary>
    public string Barcode { get; set; } = "";
}
