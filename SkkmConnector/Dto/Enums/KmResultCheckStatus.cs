namespace RBSoftSkkm;

/// <summary>
/// Статус проверки кода маркировки (результат проверки КМ в позиции).
/// </summary>
public enum KmResultCheckStatus
{
    /// <summary>
    /// Возможна реализация (восстанавливаемый статус).
    /// </summary>
    Recoverable = 0,

    /// <summary>
    /// Реализован.
    /// </summary>
    Realized = 1,

    /// <summary>
    /// Заблокирован.
    /// </summary>
    Blocked = 2,

    /// <summary>
    /// Возвращён.
    /// </summary>
    Returned = 3,

    /// <summary>
    /// Проверен.
    /// </summary>
    Checked = 4,

    /// <summary>
    /// В обороте (реализуемый).
    /// </summary>
    Realizable = 5
}
