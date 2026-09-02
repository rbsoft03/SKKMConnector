namespace SkkmConnector;

/// <summary>
/// Планируемый статус товара при проверке кода маркировки (таблица 105 ФФД):
/// <para>
/// NotSpecified - Не задан (значение по умолчанию в запросе)
/// </para>
/// <para>
/// Sold - Реализован
/// </para>
/// <para>
/// InSale - Мерный товар в стадии реализации
/// </para>
/// <para>
/// Returned - Возвращён
/// </para>
/// <para>
/// PartiallyReturned - Часть товара возвращена
/// </para>
/// <para>
/// Unchanged - Статус не изменился
/// </para>
/// </summary>
public enum MarkingPlannedStatus
{
    /// <summary>
    /// Не задан (значение по умолчанию в запросе).
    /// </summary>
    NotSpecified = 0,

    /// <summary>
    /// Реализован.
    /// </summary>
    Sold = 1,

    /// <summary>
    /// Мерный товар в стадии реализации.
    /// </summary>
    InSale = 2,

    /// <summary>
    /// Возвращён.
    /// </summary>
    Returned = 3,

    /// <summary>
    /// Часть товара возвращена.
    /// </summary>
    PartiallyReturned = 4,

    /// <summary>
    /// Статус не изменился.
    /// </summary>
    Unchanged = 255,
}
