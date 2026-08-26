namespace SkkmConnector;
public sealed partial class ServerKkm
{
    // Подключение

    /// <summary>
    /// Хост сервера ККМ (IP или DNS). Можно менять между запросами, пока программа запущена.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// TCP-порт сервера ККМ. Можно менять между запросами, пока программа запущена.
    /// </summary>
    public int Port { get; set; } = 4398;

    /// <summary>
    /// HTTPS вместо HTTP
    /// </summary>
    public bool UseHttps { get; set; }

    /// <summary>
    /// Таймаут запроса к серверу ККМ. По умолчанию 60 секунд.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Токен авторизации (заголовок api_key). Можно менять между запросами, пока программа запущена.
    /// </summary>
    public string Token { get; set; } = "";

    /// <summary>
    /// Идентификатор терминала
    /// </summary>
    public string TerminalId { get; set; } = "";

    /// <summary>
    /// Имя устройства.
    /// </summary>
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Сведения о кассире (продавце)
    /// </summary>
    public Cashier? Cashier { get; set; }
}
