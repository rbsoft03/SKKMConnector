namespace SkkmConnector;

/// <summary>
/// Метод подключения устройства:
/// <para>
/// Com - COM-порт
/// </para>
/// <para>
/// TcpIp - TCP/IP
/// </para>
/// </summary>
public enum ConnectionMethod
{
    /// <summary>
    /// COM-порт.
    /// </summary>
    Com = 0,

    /// <summary>
    /// TCP/IP.
    /// </summary>
    TcpIp = 1,
}
