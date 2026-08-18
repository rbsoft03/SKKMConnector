using SkkmConnector;

using System;
using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Данные корректировки для чека коррекции
    /// </summary>
    internal class CorrectionData
    {
        /// <summary>
        /// Тип коррекции: 0 - самостоятельно, 1 - по предписанию
        /// </summary>
        [JsonPropertyName("Type")]
        public CorrectionTypes Type { get; set; } = CorrectionTypes.Самостоятельно;

        /// <summary>
        /// Описание коррекции
        /// </summary>
        [JsonPropertyName("Description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Дата совершения корректируемого расчета
        /// </summary>
        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер предписания налогового органа
        /// </summary>
        [JsonPropertyName("Number")]
        public string Number { get; set; } = string.Empty;
    }
}
