namespace SkkmConnector;

/// <summary>
/// Признак подтверждения кода маркировки при закрытии сессии регистрации:
/// <para>
/// Included - Код маркировки включён в документ реализации
/// </para>
/// <para>
/// NotIncluded - Код маркировки не включён в документ реализации
/// </para>
/// </summary>
public enum KmConfirmationType
{
    /// <summary>
    /// Код маркировки включён в документ реализации.
    /// </summary>
    Included = 0,

    /// <summary>
    /// Код маркировки не включён в документ реализации.
    /// </summary>
    NotIncluded = 1,
}
