namespace SkkmConnector;

/// <summary>
/// Роль пользователя сервера ККМ:
/// <para>
/// Administrator - Администратор
/// </para>
/// <para>
/// Employee - Сотрудник
/// </para>
/// </summary>
public enum ServiceUserRole
{
    /// <summary>
    /// Администратор.
    /// </summary>
    Administrator = 0,

    /// <summary>
    /// Сотрудник.
    /// </summary>
    Employee = 1,
}
