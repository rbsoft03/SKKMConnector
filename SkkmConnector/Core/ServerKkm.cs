using SkkmConnector.Internal;

namespace SkkmConnector;

/// <summary>
/// Коннектор Сервера ККМ: заполняете свойства, вызываете метод, читаете результат из свойств. Обмен с сервером скрыт внутри.
/// </summary>
public sealed partial class ServerKkm : IDisposable
{
    private KkmTransport? _http;

    /// <summary>
    /// Освобождает соединение с сервером ККМ.
    /// </summary>
    public void Dispose() => _http?.Dispose();
}
