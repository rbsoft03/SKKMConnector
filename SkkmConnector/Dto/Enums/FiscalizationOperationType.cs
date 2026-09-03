namespace SkkmConnector;

/// <summary>
/// Тип операции фискализации:
/// <para>
/// Registration - Регистрация
/// </para>
/// <para>
/// ChangeParameters - Изменение параметров
/// </para>
/// <para>
/// CloseFn - Закрытие ФН
/// </para>
/// </summary>
public enum FiscalizationOperationType
{
    /// <summary>
    /// Регистрация.
    /// </summary>
    Registration = 1,

    /// <summary>
    /// Изменение параметров.
    /// </summary>
    ChangeParameters = 2,

    /// <summary>
    /// Закрытие ФН.
    /// </summary>
    CloseFn = 3,
}
