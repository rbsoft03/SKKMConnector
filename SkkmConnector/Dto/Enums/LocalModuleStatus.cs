namespace RBSoftSkkm;

/// <summary>
/// Статус локального модуля проверки маркировки.
/// </summary>
public enum LocalModuleStatus
{
    /// <summary>
    /// Неизвестно.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Готов к работе.
    /// </summary>
    Ready = 1,

    /// <summary>
    /// Инициализация.
    /// </summary>
    Initialization = 2,

    /// <summary>
    /// Сбой.
    /// </summary>
    Failure = 3,

    /// <summary>
    /// Не настроен.
    /// </summary>
    NotConfigured = 4
}
