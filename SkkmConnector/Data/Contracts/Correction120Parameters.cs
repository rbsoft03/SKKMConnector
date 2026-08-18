using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса печати чека коррекции по ФФД 1.2:
    /// POST correction120 / correction120/async.
    /// Обычный чек с позициями, дополненный данными корректировки
    /// </summary>
    internal class Correction120Parameters : CheckParameters
    {
        /// <summary>
        /// Данные корректировки
        /// </summary>
        [JsonPropertyName("CorrectionData")]
        public CorrectionData CorrectionData { get; set; } = new CorrectionData();
    }
}
