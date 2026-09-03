using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Пользователь сервера ККМ:
/// <para>
/// Id - Идентификатор пользователя на сервере (нужен при изменении / удалении)
/// </para>
/// <para>
/// UserName - Логин для входа
/// </para>
/// <para>
/// FullName - Отображаемое ФИО / полное имя
/// </para>
/// <para>
/// Vatin - ИНН пользователя
/// </para>
/// <para>
/// Role - Роль. Используйте enum <see cref="ServiceUserRole"/>
/// </para>
/// <para>
/// TokenId - Идентификатор токена API (обычно приходит в ответе сервера)
/// </para>
/// <para>
/// Password - Пароль (указывайте при создании пользователя и смене пароля)
/// </para>
/// </summary>
public sealed class ServiceUser
{
    /// <summary>
    /// Идентификатор пользователя на сервере ККМ. Нужен при изменении и удалении.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// Логин для входа (Basic Auth / учётная запись сервера).
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Полное имя пользователя (ФИО или отображаемое имя).
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// ИНН пользователя (при наличии).
    /// </summary>
    public string Vatin { get; set; } = "";

    /// <summary>
    /// Роль пользователя. Используйте enum <see cref="ServiceUserRole"/>.
    /// </summary>
    public ServiceUserRole Role { get; set; }

    /// <summary>
    /// Идентификатор токена API. Обычно заполняется сервером в ответе.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TokenId { get; set; }

    /// <summary>
    /// Пароль учётной записи. Указывайте при создании пользователя и при смене пароля.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }
}
