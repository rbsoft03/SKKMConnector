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

    /// <summary>
    /// Логин для Basic Auth при получении токена. По умолчанию Admin.
    /// </summary>
    public string AuthUserName { get; set; } = "Admin";

    /// <summary>
    /// Пароль для Basic Auth при получении токена. По умолчанию Admin.
    /// </summary>
    public string AuthPassword { get; set; } = "Admin";

    /// <summary>
    /// Имя пула устройств.
    /// </summary>
    public string PoolName { get; set; } = "";

    /// <summary>
    /// Тип отчёта для списка Z-отчётов.
    /// </summary>
    public int ReportType { get; set; }

    /// <summary>
    /// Идентификатор задания в очереди печати.
    /// </summary>
    public string QueueTaskId { get; set; } = "";

    /// <summary>
    /// Имя картинки или шаблона.
    /// </summary>
    public string PictureId { get; set; } = "";

    /// <summary>
    /// Имя шаблона печати или чека.
    /// </summary>
    public string TemplateName { get; set; } = "";

    /// <summary>
    /// Идентификатор пользователя сервера ККМ.
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// Номер ФН для печати копии чека по данным ФН.
    /// </summary>
    public string FnNumber { get; set; } = "";

    /// <summary>
    /// Коды маркировки для проверки.
    /// </summary>
    public List<string> MarkingCodes { get; } = [];

    /// <summary>
    /// Настройки кассы для добавления или изменения.
    /// </summary>
    public DeviceSettings? DeviceSettings { get; set; }

    /// <summary>
    /// Настройки службы печати.
    /// </summary>
    public ServiceSettings? ServiceSettings { get; set; }

    /// <summary>
    /// Пользователь сервера ККМ.
    /// </summary>
    public ServiceUser? ServiceUser { get; set; }

    /// <summary>
    /// Параметры шаблона печати.
    /// </summary>
    public TemplateParameters? TemplateParameters { get; set; }

    /// <summary>
    /// Параметры шаблона чека.
    /// </summary>
    public CheckTemplateParameters? CheckTemplateParameters { get; set; }

    /// <summary>
    /// Параметры фискализации.
    /// </summary>
    public FiscalizationParameters? FiscalizationParameters { get; set; }
}
