namespace SkkmConnector;

/// <summary>
/// Настройки службы печати.
/// </summary>
public sealed class ServiceSettings
{
    public int WcfServicePort { get; set; }
    public int WebServicePort { get; set; }
    public string ServiceTimeOut { get; set; } = "";
    public ProxyConfig? ProxyServerSettings { get; set; }
    public int MaxQueueSize { get; set; }
    public bool RepeatPrintingOnError { get; set; }
}
