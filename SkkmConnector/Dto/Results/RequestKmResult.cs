using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Результат локальной проверки кода маркировки.
    /// </summary>
    public class RequestKmResult
    {
        /// <summary>
        /// Связь с ОИСМ на момент запроса.
        /// </summary>
        [JsonPropertyName("ISMConnected")]
        public bool IsmConnected { get; set; }

        /// <summary>
        /// Формат кода маркировки корректный.
        /// </summary>
        [JsonPropertyName("FormatChecking")]
        public bool FormatChecking { get; set; }

        /// <summary>
        /// Код маркировки поставлен в обработку фискальным накопителем.
        /// </summary>
        [JsonPropertyName("Checking")]
        public bool Checking { get; set; }

        /// <summary>
        /// Результат проверки КП КМ, если уже известен.
        /// </summary>
        [JsonPropertyName("CheckingResult")]
        public bool CheckingResult { get; set; }

        /// <summary>
        /// Штрихкод после приведения к виду со спецсимволами GS.
        /// </summary>
        [JsonPropertyName("Barcode")]
        public string? Barcode { get; set; }
    }
}
