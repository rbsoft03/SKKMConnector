using SkkmConnector.Internal;

namespace SkkmConnector;

/// <summary>
/// Коннектор Сервера ККМ. Один экземпляр — одна сессия
/// </summary>
public sealed partial class ServerKkm : IDisposable
{
    private readonly KkmTransport _http = new();
    private readonly object _callLock = new();
    private CancellationTokenSource? _callCts;
    private bool _disposed;

    /// <summary>
    /// Отменяет текущий HTTP-запрос к серверу ККМ.
    /// </summary>
    public void Cancel()
    {
        lock (_callLock)
            _callCts?.Cancel();
    }

    /// <summary>
    /// Освобождает HTTP-соединение с сервером ККМ. После этого экземпляр использовать нельзя —
    /// создайте новый, если снова нужен доступ к кассе.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        Cancel();
        _http.Dispose();
    }
}
