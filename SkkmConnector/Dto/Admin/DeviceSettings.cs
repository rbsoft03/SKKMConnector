namespace SkkmConnector;

/// <summary>
/// Настройки кассы на сервере ККМ:
/// <para>
/// DeviceName - Имя кассы
/// </para>
/// <para>
/// DeviceType - Тип драйвера ККТ. Используйте enum <see cref="DeviceType"/>
/// </para>
/// <para>
/// Available - Доступность устройства для печати (<c>true</c> — доступно)
/// </para>
/// <para>
/// MethodConnection - Способ связи с ККТ. Используйте enum <see cref="ConnectionMethod"/>
/// </para>
/// <para>
/// PortNumber - Номер COM-порта (для MethodConnection = Com)
/// </para>
/// <para>
/// BaudRate - Скорость COM-порта (например 9600, 115200)
/// </para>
/// <para>
/// IpAddress - IP-адрес ККТ (для MethodConnection = TcpIp)
/// </para>
/// <para>
/// TcpPort - TCP-порт ККТ (для MethodConnection = TcpIp)
/// </para>
/// <para>
/// Password - Пароль пользователя ККТ
/// </para>
/// <para>
/// AccessPassword - Пароль администратора / доступ к настройкам ККТ
/// </para>
/// <para>
/// SerialNumber - Заводской номер ККТ
/// </para>
/// <para>
/// Vatin - ИНН организации-пользователя ККТ
/// </para>
/// <para>
/// OrganizationName - Наименование организации
/// </para>
/// <para>
/// SaleAddress - Адрес места расчётов
/// </para>
/// <para>
/// ClientSaleLocation - Место расчётов (офис, торговый зал и т.п.)
/// </para>
/// <para>
/// Cashier - Имя кассира по умолчанию
/// </para>
/// <para>
/// CashierVatin - ИНН кассира по умолчанию
/// </para>
/// <para>
/// SenderEmail - Email отправителя чека
/// </para>
/// <para>
/// TimeoutConnection - Таймаут соединения с ККТ, мс
/// </para>
/// <para>
/// TimeoutWaitForPrinting - Таймаут ожидания завершения печати, мс
/// </para>
/// <para>
/// OfdAddress - Адрес (хост) ОФД
/// </para>
/// <para>
/// OfdPort - Порт ОФД
/// </para>
/// <para>
/// Pool - Имя пула устройств (если касса входит в пул)
/// </para>
/// <para>
/// TemplateSettingH1…H5 - Параметры шаблонов печати H1–H5
/// </para>
/// </summary>
public sealed class DeviceSettings
{
    /// <summary>
    /// Имя кассы на сервере ККМ (уникальный идентификатор устройства).
    /// </summary>
    public string DeviceName { get; set; } = "";

    /// <summary>
    /// Тип драйвера ККТ. Используйте enum <see cref="DeviceType"/>.
    /// </summary>
    public DeviceType DeviceType { get; set; }

    /// <summary>
    /// <c>true</c> — устройство доступно для печати; <c>false</c> — недоступно.
    /// </summary>
    public bool Available { get; set; }

    /// <summary>
    /// Способ связи с ККТ. Используйте enum <see cref="ConnectionMethod"/>
    /// (Com — COM-порт, TcpIp — сеть).
    /// </summary>
    public ConnectionMethod MethodConnection { get; set; }

    /// <summary>
    /// Номер COM-порта. Используется при <see cref="ConnectionMethod.Com"/>.
    /// </summary>
    public int PortNumber { get; set; }

    /// <summary>
    /// Скорость COM-порта (бод). Пример: <c>9600</c>, <c>115200</c>.
    /// </summary>
    public int BaudRate { get; set; }

    /// <summary>
    /// IP-адрес ККТ. Используется при <see cref="ConnectionMethod.TcpIp"/>.
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    /// TCP-порт ККТ. Используется при <see cref="ConnectionMethod.TcpIp"/>.
    /// </summary>
    public int TcpPort { get; set; }

    /// <summary>
    /// Пароль пользователя ККТ.
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    /// Пароль администратора / доступ к настройкам ККТ.
    /// </summary>
    public string AccessPassword { get; set; } = "";

    /// <summary>
    /// Заводской номер ККТ.
    /// </summary>
    public string SerialNumber { get; set; } = "";

    /// <summary>
    /// ИНН организации-пользователя ККТ.
    /// </summary>
    public string Vatin { get; set; } = "";

    /// <summary>
    /// Наименование организации.
    /// </summary>
    public string OrganizationName { get; set; } = "";

    /// <summary>
    /// Адрес места осуществления расчётов.
    /// </summary>
    public string SaleAddress { get; set; } = "";

    /// <summary>
    /// Место расчётов (краткое наименование: офис, торговый зал и т.п.).
    /// </summary>
    public string ClientSaleLocation { get; set; } = "";

    /// <summary>
    /// Имя кассира по умолчанию для этой кассы.
    /// </summary>
    public string Cashier { get; set; } = "";

    /// <summary>
    /// ИНН кассира по умолчанию.
    /// </summary>
    public string CashierVatin { get; set; } = "";

    /// <summary>
    /// Email отправителя чека (тег 1117).
    /// </summary>
    public string SenderEmail { get; set; } = "";

    /// <summary>
    /// Таймаут соединения с ККТ, миллисекунды.
    /// </summary>
    public int TimeoutConnection { get; set; }

    /// <summary>
    /// Таймаут ожидания завершения печати, миллисекунды.
    /// </summary>
    public int TimeoutWaitForPrinting { get; set; }

    /// <summary>
    /// DNS-имя или IP-адрес сервера ОФД.
    /// </summary>
    public string OfdAddress { get; set; } = "";

    /// <summary>
    /// TCP-порт сервера ОФД.
    /// </summary>
    public int OfdPort { get; set; }

    /// <summary>
    /// Имя пула устройств, в который входит касса (если используется пул).
    /// </summary>
    public string Pool { get; set; } = "";

    /// <summary>
    /// Параметр шаблона печати H1.
    /// </summary>
    public string TemplateSettingH1 { get; set; } = "";

    /// <summary>
    /// Параметр шаблона печати H2.
    /// </summary>
    public string TemplateSettingH2 { get; set; } = "";

    /// <summary>
    /// Параметр шаблона печати H3.
    /// </summary>
    public string TemplateSettingH3 { get; set; } = "";

    /// <summary>
    /// Параметр шаблона печати H4.
    /// </summary>
    public string TemplateSettingH4 { get; set; } = "";

    /// <summary>
    /// Параметр шаблона печати H5.
    /// </summary>
    public string TemplateSettingH5 { get; set; } = "";
}
