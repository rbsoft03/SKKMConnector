using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Пользователь сервера ККМ.
/// </summary>
public sealed class ServiceUser
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Полное имя.
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// ИНН.
    /// </summary>
    public string Vatin { get; set; } = "";

    /// <summary>
    /// Роль: 0 — администратор, 1 — сотрудник.
    /// </summary>
    public int Role { get; set; }

    /// <summary>
    /// Токен пользователя.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TokenId { get; set; }

    /// <summary>
    /// Пароль. Нужен при создании и смене пароля.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }
}
