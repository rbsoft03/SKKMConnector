using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Сведения о приложении-источнике запроса.
/// </summary>
public sealed class SenderInfo
{
    /// <summary>
    /// Название приложения.
    /// </summary>
    [JsonPropertyName("AppName")]
    public string AppName { get; set; } = "";

    /// <summary>
    /// Версия приложения.
    /// </summary>
    [JsonPropertyName("AppVersion")]
    public string AppVersion { get; set; } = "";
}
