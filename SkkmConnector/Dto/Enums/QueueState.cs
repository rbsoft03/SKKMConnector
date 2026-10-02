namespace RBSoftSkkm;

/// <summary>
/// Состояние очереди печати.
/// </summary>
public enum QueueState
{
    /// <summary>
    /// Остановлена.
    /// </summary>
    Stopped = 0,

    /// <summary>
    /// Выполняется.
    /// </summary>
    Running = 1,

    /// <summary>
    /// Приостановлена.
    /// </summary>
    Paused = 2,

    /// <summary>
    /// Останавливается.
    /// </summary>
    StopPending = 3,

    /// <summary>
    /// Возобновляется.
    /// </summary>
    ContinuePending = 4,

    /// <summary>
    /// Переполнение очереди.
    /// </summary>
    QueueOverflow = 5
}
