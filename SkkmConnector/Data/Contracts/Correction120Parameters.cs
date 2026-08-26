using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати чека коррекции ФФД 1.2.
    /// </summary>
    internal class Correction120Parameters : CheckParameters
    {
        /// <summary>
        /// Данные корректировки
        /// </summary>
        [JsonPropertyName("CorrectionData")]
        public CorrectionData? CorrectionData { get; set; }
    }
}
