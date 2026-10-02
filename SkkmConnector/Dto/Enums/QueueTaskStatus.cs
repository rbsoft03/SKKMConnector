namespace RBSoftSkkm;

/// <summary>
/// Статус выполнения задачи печати в очереди.
/// </summary>
public enum QueueTaskStatus
{
    /// <summary>
    /// Новая задача, обработка ещё не началась (в очереди).
    /// </summary>
    New = 0,

    /// <summary>
    /// Задача отправлена на выполнение.
    /// </summary>
    Sent = 1,

    /// <summary>
    /// Задача удачно обработана.
    /// </summary>
    Ok = 2,

    /// <summary>
    /// Задача вернулась из обработки с ошибкой.
    /// </summary>
    Error = -1
}
