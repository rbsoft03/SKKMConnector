namespace SkkmConnector;

/// <summary>
/// Отраслевой реквизит предмета расчёта:
/// <para>
/// IdentifierFoiv - Идентификатор ФОИВ
/// </para>
/// <para>
/// DocumentDate - Дата документа-основания
/// </para>
/// <para>
/// DocumentNumber - Номер документа-основания
/// </para>
/// <para>
/// AttributeValue - Значение отраслевого реквизита
/// </para>
/// </summary>
public sealed class Industry
{
    /// <summary>
    /// Идентификатор ФОИВ.
    /// </summary>
    public string? IdentifierFoiv { get; set; }

    /// <summary>
    /// Дата документа основания.
    /// </summary>
    public string? DocumentDate { get; set; }

    /// <summary>
    /// Номер документа основания.
    /// </summary>
    public string? DocumentNumber { get; set; }

    /// <summary>
    /// Значение отраслевого реквизита.
    /// </summary>
    public string? AttributeValue { get; set; }
}
