namespace SkkmConnector;

/// <summary>
/// Результат фискализации.
/// </summary>
public sealed class FiscalizationDocument
{
    /// <summary>
    /// Тип выполненной операции.
    /// </summary>
    public FiscalizationOperationType OperationType { get; set; }

    /// <summary>
    /// Регистрационный номер ККТ.
    /// </summary>
    public string RnNumber { get; set; } = "";

    /// <summary>
    /// Коды систем налогообложения.
    /// </summary>
    public string TaxationSystems { get; set; } = "";

    /// <summary>
    /// ИНН организации.
    /// </summary>
    public string Vatin { get; set; } = "";

    /// <summary>
    /// Название организации.
    /// </summary>
    public string CompanyName { get; set; } = "";

    /// <summary>
    /// Версия ФФД ККТ.
    /// </summary>
    public string FfdVersionKkt { get; set; } = "";

    /// <summary>
    /// Версия ФФД ФН.
    /// </summary>
    public string FfdVersionFn { get; set; } = "";

    /// <summary>
    /// Признак фискального режима.
    /// </summary>
    public bool IsFiscal { get; set; }

    /// <summary>
    /// Идентификатор документа фискализации.
    /// </summary>
    public string DocId { get; set; } = "";

    /// <summary>
    /// Название устройства.
    /// </summary>
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Номер смены.
    /// </summary>
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер фискального документа.
    /// </summary>
    public int DocNumber { get; set; }

    /// <summary>
    /// Фискальный признак документа.
    /// </summary>
    public string FiscalSign { get; set; } = "";
}
