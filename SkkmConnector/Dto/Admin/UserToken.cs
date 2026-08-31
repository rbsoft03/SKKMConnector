using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Токен авторизации пользователя.
/// </summary>
public sealed class UserToken
{
    [JsonPropertyName("tokenId")]
    public string TokenId { get; set; } = "";

    [JsonPropertyName("expire")]
    public string Expire { get; set; } = "";
}
