namespace RBSoftSkkm;

/// <summary>
/// Состояние документа в очереди печати.
/// </summary>
public enum DocumentPrintState
{
    /// <summary>
    /// Документ не найден.
    /// </summary>
    NotFound = -1,

    /// <summary>
    /// Начало печати.
    /// </summary>
    BeginPrinting = 0,

    /// <summary>
    /// Напечатан.
    /// </summary>
    Printed = 1,

    /// <summary>
    /// Ошибка печати.
    /// </summary>
    PrintingError = 2,

    /// <summary>
    /// Повтор печати.
    /// </summary>
    PrintingRepeat = 3,

    /// <summary>
    /// Добавлен в очередь.
    /// </summary>
    AddedToQueue = 4,

    /// <summary>
    /// Удалён из очереди.
    /// </summary>
    RemovedFromQueue = 5,

    /// <summary>
    /// Не добавлен в очередь.
    /// </summary>
    NotAddedToQueue = 6,

    /// <summary>
    /// Некорректный тип задания.
    /// </summary>
    IncorrectTaskType = 7,

    /// <summary>
    /// Неизвестный тип задания.
    /// </summary>
    UnknownTaskType = 8,

    /// <summary>
    /// Устройство назначено пулу.
    /// </summary>
    PoolDeviceAssigned = 9,

    /// <summary>
    /// Устройство удалено из пула.
    /// </summary>
    PoolDeviceErased = 10,

    /// <summary>
    /// Печать выполняется.
    /// </summary>
    Printing = 11,

    /// <summary>
    /// Начало открытия чека.
    /// </summary>
    BeginOpenCheck = 12,

    /// <summary>
    /// Начало закрытия чека.
    /// </summary>
    BeginCloseCheck = 13,

    /// <summary>
    /// Печать невозможна.
    /// </summary>
    CantPrinting = 14,

    /// <summary>
    /// Восстановление невозможно.
    /// </summary>
    CantReanimation = 15,

    /// <summary>
    /// Восстановление выполнено успешно.
    /// </summary>
    ReanimationSuccessed = 16,

    /// <summary>
    /// Бумага не найдена.
    /// </summary>
    PaperNotFound = 17,

    /// <summary>
    /// Печать с коррекцией.
    /// </summary>
    PrintingWithCorrection = 18
}
