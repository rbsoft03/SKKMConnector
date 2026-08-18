using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Оператор фискальных данных
    /// </summary>
    public class Ofd
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Vatin")]
        public string? Vatin { get; set; }
    }
}
