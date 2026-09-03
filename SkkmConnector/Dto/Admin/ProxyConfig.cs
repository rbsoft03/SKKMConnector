namespace SkkmConnector;

/// <summary>
/// Настройки прокси-сервера:
/// <para>
/// IsUseProxy - Включить прокси для общих запросов
/// </para>
/// <para>
/// IsUseProxyService - Использовать прокси для службы печати
/// </para>
/// <para>
/// IsUseProxyMarking - Использовать прокси для запросов маркировки
/// </para>
/// <para>
/// IpAddress - IP-адрес или DNS-имя прокси-сервера
/// </para>
/// <para>
/// Port - TCP-порт прокси-сервера
/// </para>
/// <para>
/// Name - Логин для авторизации на прокси
/// </para>
/// <para>
/// Password - Пароль для авторизации на прокси
/// </para>
/// </summary>
public sealed class ProxyConfig
{
    /// <summary>
    /// <c>true</c> — использовать прокси для общих запросов сервера ККМ.
    /// </summary>
    public bool IsUseProxy { get; set; }

    /// <summary>
    /// <c>true</c> — использовать прокси для службы печати.
    /// </summary>
    public bool IsUseProxyService { get; set; }

    /// <summary>
    /// <c>true</c> — использовать прокси для запросов маркировки (ИСМ и связанные).
    /// </summary>
    public bool IsUseProxyMarking { get; set; }

    /// <summary>
    /// IP-адрес или DNS-имя прокси-сервера.
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    /// TCP-порт прокси-сервера.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Логин для авторизации на прокси (если требуется).
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Пароль для авторизации на прокси (если требуется).
    /// </summary>
    public string Password { get; set; } = "";
}
