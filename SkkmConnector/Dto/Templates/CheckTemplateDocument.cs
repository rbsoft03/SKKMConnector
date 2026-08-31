namespace SkkmConnector;

/// <summary>
/// Документ шаблона чека
/// </summary>
public sealed class CheckTemplateDocument
{
    /// <summary>
    /// Тип чека 
    /// </summary>
    public int PaymentType { get; set; }

    /// <summary>
    /// Система налогообложения
    /// </summary>
    public int TaxVariant { get; set; }

    /// <summary>
    /// Часовая зона.
    /// </summary>
    public int? TimeZone { get; set; }

    /// <summary>
    /// Признак расчёта в сети Интернет.
    /// </summary>
    public bool OperationOnline { get; set; }

    /// <summary>
    /// Адрес электронной почты отправителя чека.
    /// </summary>
    public string SenderEmail { get; set; } = "";

    /// <summary>
    /// Адрес проведения расчётов.
    /// </summary>
    public string SaleAddress { get; set; } = "";

    /// <summary>
    /// Место проведения расчётов.
    /// </summary>
    public string SaleLocation { get; set; } = "";

    /// <summary>
    /// Формирование чека только в электронном виде.
    /// </summary>
    public bool Electronically { get; set; }

    /// <summary>
    /// Покупатель.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Позиции чека
    /// </summary>
    public List<Position> Positions { get; set; } = [];

    /// <summary>
    /// Строки шаблона
    /// </summary>
    public List<CheckItem> CheckItems { get; set; } = [];

    /// <summary>
    /// Оплаты чека.
    /// </summary>
    public Payments? Payments { get; set; }

    /// <summary>
    /// Электронные платежи.
    /// </summary>
    public List<ElectronicPayment> ElectronicPayments { get; set; } = [];

    /// <summary>
    /// Данные коррекции
    /// </summary>
    public CorrectionData? CorrectionData { get; set; }

    /// <summary>
    /// Отраслевой реквизит чека
    /// </summary>
    public Industry? IndustryAttribute { get; set; }

    /// <summary>
    /// Дополнительный реквизит пользователя
    /// </summary>
    public UserAttribute? UserAttribute { get; set; }

    /// <summary>
    /// Операционный реквизит чека
    /// </summary>
    public OperationalAttribute? OperationalAttribute { get; set; }

    /// <summary>
    /// Дополнительный реквизит чека (БСО), тег 1192
    /// </summary>
    public string AdditionalAttribute { get; set; } = "";
}
