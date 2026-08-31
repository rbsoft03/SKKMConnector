namespace SkkmConnector;

/// <summary>
/// Настройки кассы на сервере ККМ.
/// </summary>
public sealed class DeviceSettings
{
    public string DeviceName { get; set; } = "";
    public int DeviceType { get; set; }
    public bool Available { get; set; }
    public int MethodConnection { get; set; }
    public int PortNumber { get; set; }
    public int BaudRate { get; set; }
    public string IpAddress { get; set; } = "";
    public int TcpPort { get; set; }
    public string Password { get; set; } = "";
    public string AccessPassword { get; set; } = "";
    public string SerialNumber { get; set; } = "";
    public string Vatin { get; set; } = "";
    public string OrganizationName { get; set; } = "";
    public string SaleAddress { get; set; } = "";
    public string ClientSaleLocation { get; set; } = "";
    public string Cashier { get; set; } = "";
    public string CashierVatin { get; set; } = "";
    public string SenderEmail { get; set; } = "";
    public int TimeoutConnection { get; set; }
    public int TimeoutWaitForPrinting { get; set; }
    public string OfdAddress { get; set; } = "";
    public int OfdPort { get; set; }
    public string Pool { get; set; } = "";
    public string TemplateSettingH1 { get; set; } = "";
    public string TemplateSettingH2 { get; set; } = "";
    public string TemplateSettingH3 { get; set; } = "";
    public string TemplateSettingH4 { get; set; } = "";
    public string TemplateSettingH5 { get; set; } = "";
}
