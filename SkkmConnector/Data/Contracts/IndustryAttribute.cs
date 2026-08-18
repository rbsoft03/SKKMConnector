using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Отраслевой реквизит.
    /// </summary>
    internal class IndustryAttribute
    {
        [JsonPropertyName("IdentifierFoiv")]
        public string? IdentifierFoiv { get; set; }

        [JsonPropertyName("DocumentDate")]
        public string? DocumentDate { get; set; }

        [JsonPropertyName("DocumentNumber")]
        public string? DocumentNumber { get; set; }

        [JsonPropertyName("AttributeValue")]
        public string? AttributeValue { get; set; }
    }
}
