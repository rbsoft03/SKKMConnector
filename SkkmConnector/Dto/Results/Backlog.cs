using System;
using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Сведения о непереданных в ОФД документах
    /// </summary>
    public class Backlog
    {
        /// <summary>
        /// Количество непереданных документов
        /// </summary>
        [JsonPropertyName("DocumentsCounter")]
        public long DocumentsCounter { get; set; }

        /// <summary>
        /// Номер первого непереданного документа
        /// </summary>
        [JsonPropertyName("DocumentFirstNumber")]
        public long DocumentFirstNumber { get; set; }

        /// <summary>
        /// Дата и время первого непереданного документа
        /// </summary>
        [JsonPropertyName("DocumentFirstDateTime")]
        public DateTime DocumentFirstDateTime { get; set; }
    }
}
