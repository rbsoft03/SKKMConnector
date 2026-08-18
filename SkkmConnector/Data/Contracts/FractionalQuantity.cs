using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Дробное количество маркированного товара.
    /// </summary>
    internal class FractionalQuantity
    {
        [JsonPropertyName("Numerator")]
        public int Numerator { get; set; }

        [JsonPropertyName("Denominator")]
        public int Denominator { get; set; }
    }
}
