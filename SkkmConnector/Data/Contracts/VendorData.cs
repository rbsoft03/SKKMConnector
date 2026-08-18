using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Данные поставщика.
    /// </summary>
    internal class VendorData
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Phones")]
        public string[]? Phones { get; set; }

        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }
    }
}
