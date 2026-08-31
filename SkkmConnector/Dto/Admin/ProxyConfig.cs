namespace SkkmConnector;

/// <summary>
/// Настройки прокси-сервера.
/// </summary>
public sealed class ProxyConfig
{
    public bool IsUseProxy { get; set; }
    public bool IsUseProxyService { get; set; }
    public bool IsUseProxyMarking { get; set; }
    public string IpAddress { get; set; } = "";
    public int Port { get; set; }
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
}
