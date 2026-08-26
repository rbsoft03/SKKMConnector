namespace SkkmConnector;

/// <summary>
/// Отраслевой реквизит предмета расчета
/// </summary>
public sealed class Industry
{
    /// <summary>
    /// Идентификатор ФОИВ
    /// </summary>
    public string? IdentifierFoiv { get; set; }

    /// <summary>
    /// Дата документа основания в формате "DD.MM.YYYY"
    /// </summary>
    public string? DocumentDate { get; set; }

    /// <summary>
    /// Номер документа основания
    /// </summary>
    public string? DocumentNumber { get; set; }

    /// <summary>
    /// Значение отраслевого реквизита
    /// </summary>
    public string? AttributeValue { get; set; }
}
