using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Печать штрихкода в документе.
    /// </summary>
    internal class Barcode
    {
        /// <summary>
        /// Тип штрихкода: UPCA, CODE39, EAN13, EAN8, UPCE, ITF, CODABAR, CODE93, CODE128, PDF417, CODE32, QR
        /// </summary>
        [JsonPropertyName("Type")]
        public string? Type { get; set; }

        /// <summary>
        /// Значение штрихкода
        /// </summary>
        [JsonPropertyName("Barcode")]
        public string? Value { get; set; }

        /// <summary>
        /// Выравнивание: left, right, center
        /// </summary>
        [JsonPropertyName("Alignment")]
        public string? Alignment { get; set; }
    }
}
