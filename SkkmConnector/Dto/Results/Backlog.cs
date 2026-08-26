using System;
using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Данные о непереданных документах (Backlog).
    /// </summary>
    public class Backlog
    {
        /// <summary>
        /// Количество непереданных документов.
        /// </summary>
        [JsonPropertyName("DocumentsCounter")]
        public long DocumentsCounter { get; set; }

        /// <summary>
        /// Номер первого непереданного документа.
        /// </summary>
        [JsonPropertyName("DocumentFirstNumber")]
        public long DocumentFirstNumber { get; set; }

        /// <summary>
        /// Дата и время первого из непереданных документов.
        /// </summary>
        [JsonPropertyName("DocumentFirstDateTime")]
        public DateTime DocumentFirstDateTime { get; set; }
    }
}
