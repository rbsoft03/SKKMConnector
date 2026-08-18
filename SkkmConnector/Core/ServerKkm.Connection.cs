namespace SkkmConnector;

// Свойства подключения к серверу ККМ и кассира.
public sealed partial class ServerKkm
{
    // Подключение

    /// <summary>
    /// Адрес сервера ККМ (IP или DNS).
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// Порт сервера ККМ. По умолчанию 4398.
    /// </summary>
    public int Port { get; set; } = 4398;

    /// <summary>
    /// Адрес сервера ККМ одной строкой
    /// Порт через двоеточие необязателен: localhost, 192.168.1.150:12345.
    /// Без порта используется 4398.
    /// </summary>
    public string ServerAddress
    {
        get => Port == DefaultPort ? Host : $"{Host}:{Port}";
        set
        {
            var connection = ParseServerAddress(value);
            Host = connection.Host;
            Port = connection.Port;
        }
    }

    /// <summary>
    /// Токен авторизации (заголовок api_key).
    /// </summary>
    public string Token { get; set; } = "";

    /// <summary>
    /// Идентификатор терминала (заголовок TerminalId).
    /// </summary>
    public string TerminalId { get; set; } = "";

    /// <summary>
    /// Имя кассы на сервере ККМ. Обязательно для операций с устройством (чек, смена, ящик и т.п.).
    /// </summary>
    public string DeviceName { get; set; } = "";

    // Кассир

    /// <summary>
    /// ФИО кассира. Если пусто, сервер может подставить значение из настроек ККТ.
    /// </summary>
    public string CashierName { get; set; } = "";

    /// <summary>
    /// ИНН кассира.
    /// </summary>
    public string CashierVatin { get; set; } = "";
}
