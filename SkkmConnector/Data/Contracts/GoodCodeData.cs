using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Код маркировки / номенклатуры.
    /// </summary>
    internal class GoodCodeData
    {
        [JsonPropertyName("Gtin")]
        public string? Gtin { get; set; }

        [JsonPropertyName("SerialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("CommodityGroup")]
        public string? CommodityGroup { get; set; }

        [JsonPropertyName("Barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("MarkingCode")]
        public string? MarkingCode { get; set; }
    }
}
