namespace SkkmConnector;

/// <summary>
/// Операционный реквизит чека:
/// <para>
/// DateTime - Дата и время операции
/// </para>
/// <para>
/// OperationId - Идентификатор операции
/// </para>
/// <para>
/// OperationData - Данные операции
/// </para>
/// </summary>
public sealed class OperationalAttribute
{
    /// <summary>
    /// Дата, время операции.
    /// </summary>
    public string? DateTime { get; set; }

    /// <summary>
    /// Идентификатор операции.
    /// </summary>
    public int? OperationId { get; set; }

    /// <summary>
    /// Данные операции.
    /// </summary>
    public string? OperationData { get; set; }
}
