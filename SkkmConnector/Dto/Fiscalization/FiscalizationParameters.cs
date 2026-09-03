namespace SkkmConnector;

/// <summary>
/// Параметры фискализации / перерегистрации ККТ.
/// <para>
/// Регистрационные данные:
/// </para>
/// <para>
/// DeviceName - Имя кассы на сервере ККМ
/// </para>
/// <para>
/// Cashier - Кассир (объект <see cref="Cashier"/>: Name, Vatin)
/// </para>
/// <para>
/// RnNumber - Регистрационный номер ККТ (РНМ)
/// </para>
/// <para>
/// TaxationSystems - Системы налогообложения через запятую (коды 0–5, например "0,1,2")
/// </para>
/// <para>
/// Vatin - ИНН организации
/// </para>
/// <para>
/// CompanyName - Наименование организации
/// </para>
/// <para>
/// Fn - Заводской номер фискального накопителя
/// </para>
/// <para>
/// ФФД и коды изменения сведений:
/// </para>
/// <para>
/// FfdVersionKkt - Версия ФФД ККТ (например "1.05", "1.2")
/// </para>
/// <para>
/// FfdVersionFn - Версия ФФД ФН
/// </para>
/// <para>
/// RegistrationLabelCodes - Коды причин изменения сведений о ККТ (например "3.1")
/// </para>
/// <para>
/// ОФД и отправитель:
/// </para>
/// <para>
/// OfdAddress / OfdPort - Адрес и порт сервера ОФД
/// </para>
/// <para>
/// OfdVatin / OfdName - ИНН и наименование ОФД
/// </para>
/// <para>
/// SenderEmail - Email отправителя чеков
/// </para>
/// <para>
/// ReasonCode - Причина перерегистрации. Используйте enum <see cref="FiscalizationReasonCode"/>
/// </para>
/// <para>
/// ИСМ, ФНС, автоматы, агенты:
/// </para>
/// <para>
/// IsmHost / IsmPort - Хост и порт ИСМ (для маркировки)
/// </para>
/// <para>
/// FnsUrl - Адрес сайта ФНС (например "nalog.ru")
/// </para>
/// <para>
/// AutomaticNumber - Номер автоматического устройства для расчётов
/// </para>
/// <para>
/// AgentTypes - Признаки агента через запятую (коды типов агента)
/// </para>
/// <para>
/// Признаки режимов применения ККТ (true / false):
/// </para>
/// <para>
/// IsBsoSign - АС БСО
/// </para>
/// <para>
/// IsMarking - Маркированные товары
/// </para>
/// <para>
/// IsPawnshop - Ломбард
/// </para>
/// <para>
/// IsAssurance - Страхование
/// </para>
/// <para>
/// IsAutomatic - Автоматический режим
/// </para>
/// <para>
/// IsVending - Торговый автомат
/// </para>
/// <para>
/// IsAutomaticPrinter - Принтер в автомате
/// </para>
/// <para>
/// IsOnline - Только интернет-расчёты
/// </para>
/// <para>
/// IsLottery - Лотереи
/// </para>
/// <para>
/// IsGambling - Азартные игры
/// </para>
/// <para>
/// IsExcisable - Подакцизные товары
/// </para>
/// <para>
/// IsService - Услуги
/// </para>
/// <para>
/// IsEncrypted - Шифрование данных
/// </para>
/// <para>
/// IsOffline - Автономный режим (без ОФД)
/// </para>
/// <para>
/// IsCateringServices - Общественное питание
/// </para>
/// <para>
/// IsWholesaleTrade - Оптовая торговля
/// </para>
/// <para>
/// Адрес расчётов:
/// </para>
/// <para>
/// SaleAddress - Адрес места расчётов
/// </para>
/// <para>
/// SaleLocation - Место расчётов (например "Офис", "Торговый зал")
/// </para>
/// </summary>
public sealed class FiscalizationParameters
{
    /// <summary>
    /// Имя кассы на сервере ККМ. Если пусто — берётся из <c>kkm.DeviceName</c>.
    /// </summary>
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Кассир, выполняющий регистрацию. Создайте объект <see cref="Cashier"/> (Name, Vatin).
    /// Если не задан — берётся из <c>kkm.Cashier</c>.
    /// </summary>
    public Cashier? Cashier { get; set; }

    /// <summary>
    /// Регистрационный номер ККТ (РНМ), выданный при регистрации в ФНС.
    /// </summary>
    public string RnNumber { get; set; } = "";

    /// <summary>
    /// Применяемые системы налогообложения — коды через запятую
    /// (0 — ОСН, 1 — УСН доход, 2 — УСН доход−расход, 3 — ЕНВД, 4 — ЕСХН, 5 — ПСН).
    /// Пример: <c>"0,1,2"</c>.
    /// </summary>
    public string TaxationSystems { get; set; } = "";

    /// <summary>
    /// ИНН организации-пользователя ККТ.
    /// </summary>
    public string Vatin { get; set; } = "";

    /// <summary>
    /// Наименование организации-пользователя ККТ.
    /// </summary>
    public string CompanyName { get; set; } = "";

    /// <summary>
    /// Заводской номер фискального накопителя (ФН).
    /// </summary>
    public string Fn { get; set; } = "";

    /// <summary>
    /// Версия формата фискальных документов ККТ. Пример: <c>"1.2"</c>, <c>"1.05"</c>.
    /// </summary>
    public string FfdVersionKkt { get; set; } = "";

    /// <summary>
    /// Версия формата фискальных документов ФН. Пример: <c>"1.2"</c>.
    /// </summary>
    public string FfdVersionFn { get; set; } = "";

    /// <summary>
    /// Коды причин изменения сведений о ККТ (через запятую или точку, по формату сервера).
    /// Пример: <c>"3.1"</c>.
    /// </summary>
    public string RegistrationLabelCodes { get; set; } = "";

    /// <summary>
    /// DNS-имя или IP-адрес сервера ОФД.
    /// </summary>
    public string OfdAddress { get; set; } = "";

    /// <summary>
    /// TCP-порт сервера ОФД.
    /// </summary>
    public int OfdPort { get; set; }

    /// <summary>
    /// Номер автоматического устройства для расчётов (для автоматов / АС).
    /// </summary>
    public string AutomaticNumber { get; set; } = "";

    /// <summary>
    /// Адрес электронной почты отправителя чека (тег 1117).
    /// </summary>
    public string SenderEmail { get; set; } = "";

    /// <summary>
    /// Причина перерегистрации ККТ. Используйте enum <see cref="FiscalizationReasonCode"/>.
    /// Для первичной регистрации может не требоваться.
    /// </summary>
    public FiscalizationReasonCode ReasonCode { get; set; }

    /// <summary>
    /// Хост ИСМ (информационная система маркировки), если используется маркировка.
    /// </summary>
    public string IsmHost { get; set; } = "";

    /// <summary>
    /// Порт ИСМ.
    /// </summary>
    public int IsmPort { get; set; }

    /// <summary>
    /// Адрес сайта ФНС. Пример: <c>"nalog.ru"</c>.
    /// </summary>
    public string FnsUrl { get; set; } = "";

    /// <summary>
    /// ИНН оператора фискальных данных (ОФД).
    /// </summary>
    public string OfdVatin { get; set; } = "";

    /// <summary>
    /// Наименование оператора фискальных данных (ОФД).
    /// </summary>
    public string OfdName { get; set; } = "";

    /// <summary>
    /// Признаки агента — числовые коды через запятую (см. <see cref="AgentType"/>).
    /// </summary>
    public string AgentTypes { get; set; } = "";

    /// <summary>
    /// <c>true</c> — ККТ применяется для формирования АС БСО.
    /// </summary>
    public bool IsBsoSign { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при продаже маркированных товаров.
    /// </summary>
    public bool IsMarking { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при осуществлении ломбардной деятельности.
    /// </summary>
    public bool IsPawnshop { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при осуществлении страховой деятельности.
    /// </summary>
    public bool IsAssurance { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется в автоматическом режиме.
    /// </summary>
    public bool IsAutomatic { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется в составе торгового автомата (вендинг).
    /// </summary>
    public bool IsVending { get; set; }

    /// <summary>
    /// <c>true</c> — в автоматическом устройстве установлен принтер чеков.
    /// </summary>
    public bool IsAutomaticPrinter { get; set; }

    /// <summary>
    /// <c>true</c> — расчёты ведутся только в сети Интернет (без выдачи бумажного чека покупателю на месте).
    /// </summary>
    public bool IsOnline { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при проведении лотерей.
    /// </summary>
    public bool IsLottery { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при проведении азартных игр.
    /// </summary>
    public bool IsGambling { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при продаже подакцизных товаров.
    /// </summary>
    public bool IsExcisable { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при оказании услуг.
    /// </summary>
    public bool IsService { get; set; }

    /// <summary>
    /// <c>true</c> — данные в ФН шифруются.
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// <c>true</c> — автономный режим (без передачи данных в ОФД).
    /// </summary>
    public bool IsOffline { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при оказании услуг общественного питания.
    /// </summary>
    public bool IsCateringServices { get; set; }

    /// <summary>
    /// <c>true</c> — ККТ применяется при оптовой торговле.
    /// </summary>
    public bool IsWholesaleTrade { get; set; }

    /// <summary>
    /// Адрес места осуществления расчётов (улица, дом и т.п.).
    /// </summary>
    public string SaleAddress { get; set; } = "";

    /// <summary>
    /// Место расчётов (краткое наименование: офис, торговый зал, павильон и т.п.).
    /// </summary>
    public string SaleLocation { get; set; } = "";
}
