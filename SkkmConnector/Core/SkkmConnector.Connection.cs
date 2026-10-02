namespace RBSoftSkkm;

// Вход: подключение к серверу ККМ и общие параметры запроса (кассир, пул, фискализация).
public sealed partial class SkkmConnector
{
    // Подключение

    /// <summary>
    /// Адрес сервера ККМ в виде хост:порт (например, "localhost:4398").
    /// Порт можно опустить — тогда используется 4398. Можно менять между запросами.
    /// </summary>
    public string Address { get; set; } = "localhost:4398";

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
    /// Имя (ФИО) кассира.
    /// </summary>
    public string CashierName { get; private set; } = "";

    /// <summary>
    /// ИНН кассира (при наличии).
    /// </summary>
    public string CashierVatin { get; private set; } = "";

    /// <summary>
    /// Задать кассира на смену
    /// </summary>
    public void SetCashier(string cashierName)
    {
        CashierName = cashierName;
        CashierVatin = "";
    }

    /// <summary>
    /// Задать кассира на смену
    /// </summary>
    public void SetCashier(string cashierName, string cashierVatin)
    {
        CashierName = cashierName;
        CashierVatin = cashierVatin;
    }

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
    /// Коды маркировки для проверки.
    /// </summary>
    public List<string> MarkingCodes { get; } = [];

    /// <summary>
    /// Регистрационный номер ККТ (РНМ) для фискализации.
    /// </summary>
    public string FiscalizationRnNumber { get; set; } = "";

    /// <summary>
    /// Системы налогообложения, поддерживаемые ККТ
    /// </summary>
    public TaxSystem[] FiscalizationTaxationSystems { get; set; } = [];

    /// <summary>
    /// ИНН организации.
    /// </summary>
    public string FiscalizationVatin { get; set; } = "";

    /// <summary>
    /// Наименование организации.
    /// </summary>
    public string FiscalizationCompanyName { get; set; } = "";

    /// <summary>
    /// Номер фискального накопителя (ФН).
    /// </summary>
    public string FiscalizationFn { get; set; } = "";

    /// <summary>
    /// Версия ФФД ККТ.
    /// </summary>
    public string FiscalizationFfdVersionKkt { get; set; } = "";

    /// <summary>
    /// Версия ФФД ФН.
    /// </summary>
    public string FiscalizationFfdVersionFn { get; set; } = "";

    /// <summary>
    /// Коды причин перерегистрации (метки).
    /// </summary>
    public string FiscalizationRegistrationLabelCodes { get; set; } = "";

    /// <summary>
    /// Адрес ОФД.
    /// </summary>
    public string FiscalizationOfdAddress { get; set; } = "";

    /// <summary>
    /// Порт ОФД.
    /// </summary>
    public int FiscalizationOfdPort { get; set; }

    /// <summary>
    /// Номер автомата (для автоматического режима).
    /// </summary>
    public string FiscalizationAutomaticNumber { get; set; } = "";

    /// <summary>
    /// Адрес электронной почты отправителя.
    /// </summary>
    public string FiscalizationSenderEmail { get; set; } = "";

    /// <summary>
    /// Код причины перерегистрации.
    /// </summary>
    public FiscalizationReasonCode FiscalizationReasonCode { get; set; }

    /// <summary>
    /// Хост ИСМ (информационная система маркировки).
    /// </summary>
    public string FiscalizationIsmHost { get; set; } = "";

    /// <summary>
    /// Порт ИСМ.
    /// </summary>
    public string FiscalizationIsmPort { get; set; } = "";

    /// <summary>
    /// Адрес сайта ФНС.
    /// </summary>
    public string FiscalizationFnsUrl { get; set; } = "";

    /// <summary>
    /// ИНН ОФД.
    /// </summary>
    public string FiscalizationOfdVatin { get; set; } = "";

    /// <summary>
    /// Наименование ОФД.
    /// </summary>
    public string FiscalizationOfdName { get; set; } = "";

    /// <summary>
    /// Признаки агента, поддерживаемые ККТ.
    /// </summary>
    public AgentType[] FiscalizationAgentTypes { get; set; } = [];

    /// <summary>
    /// Признак БСО.
    /// </summary>
    public bool FiscalizationIsBsoSign { get; set; }

    /// <summary>
    /// Работа с маркировкой.
    /// </summary>
    public bool FiscalizationIsMarking { get; set; }

    /// <summary>
    /// Ломбард.
    /// </summary>
    public bool FiscalizationIsPawnshop { get; set; }

    /// <summary>
    /// Страхование.
    /// </summary>
    public bool FiscalizationIsAssurance { get; set; }

    /// <summary>
    /// Автоматический режим.
    /// </summary>
    public bool FiscalizationIsAutomatic { get; set; }

    /// <summary>
    /// Торговый автомат (вендинг).
    /// </summary>
    public bool FiscalizationIsVending { get; set; }

    /// <summary>
    /// Автоматический принтер.
    /// </summary>
    public bool FiscalizationIsAutomaticPrinter { get; set; }

    /// <summary>
    /// Расчёты только в интернете.
    /// </summary>
    public bool FiscalizationIsOnline { get; set; }

    /// <summary>
    /// Проведение лотерей.
    /// </summary>
    public bool FiscalizationIsLottery { get; set; }

    /// <summary>
    /// Проведение азартных игр.
    /// </summary>
    public bool FiscalizationIsGambling { get; set; }

    /// <summary>
    /// Продажа подакцизных товаров.
    /// </summary>
    public bool FiscalizationIsExcisable { get; set; }

    /// <summary>
    /// Применение в сфере услуг.
    /// </summary>
    public bool FiscalizationIsService { get; set; }

    /// <summary>
    /// Шифрование данных.
    /// </summary>
    public bool FiscalizationIsEncrypted { get; set; }

    /// <summary>
    /// Автономный режим.
    /// </summary>
    public bool FiscalizationIsOffline { get; set; }

    /// <summary>
    /// Услуги общественного питания.
    /// </summary>
    public bool FiscalizationIsCateringServices { get; set; }

    /// <summary>
    /// Оптовая торговля.
    /// </summary>
    public bool FiscalizationIsWholesaleTrade { get; set; }
}
