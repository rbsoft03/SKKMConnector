namespace SkkmConnector;

/// <summary>
/// Настройки службы печати:
/// <para>
/// WcfServicePort - TCP-порт WCF-службы сервера ККМ
/// </para>
/// <para>
/// WebServicePort - TCP-порт веб-службы (HTTP API)
/// </para>
/// <para>
/// ServiceTimeOut - Таймаут ожидания ответа службы
/// </para>
/// <para>
/// ProxyServerSettings - Настройки прокси. Создайте объект <see cref="ProxyConfig"/>
/// </para>
/// <para>
/// MaxQueueSize - Максимальное число заданий в очереди печати
/// </para>
/// <para>
/// RepeatPrintingOnError - Повторять печать при ошибке (<c>true</c> / <c>false</c>)
/// </para>
/// </summary>
public sealed class ServiceSettings
{
    /// <summary>
    /// TCP-порт WCF-службы сервера ККМ.
    /// </summary>
    public int WcfServicePort { get; set; }

    /// <summary>
    /// TCP-порт веб-службы (HTTP API).
    /// </summary>
    public int WebServicePort { get; set; }

    /// <summary>
    /// Таймаут ожидания ответа службы (строка в формате, ожидаемом сервером).
    /// </summary>
    public string ServiceTimeOut { get; set; } = "";

    /// <summary>
    /// Настройки прокси-сервера. Создайте объект <see cref="ProxyConfig"/> и заполните нужные поля.
    /// </summary>
    public ProxyConfig? ProxyServerSettings { get; set; }

    /// <summary>
    /// Максимальное число заданий в очереди печати.
    /// </summary>
    public int MaxQueueSize { get; set; }

    /// <summary>
    /// <c>true</c> — повторять печать при ошибке; <c>false</c> — не повторять.
    /// </summary>
    public bool RepeatPrintingOnError { get; set; }
}
