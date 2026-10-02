using System.Text.Json.Serialization;

namespace RBSoftSkkm
{
    /// <summary>
    /// Состояние обмена с ОФД на момент документа (X/Z-отчёт, открытие смены).
    /// </summary>
    public class OfdStatus
    {
        /// <summary>
        /// Обмен с ОФД завершён.
        /// </summary>
        [JsonPropertyName("IsCompleted")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Соединение с ОФД установлено.
        /// </summary>
        [JsonPropertyName("IsConnectedOFD")]
        public bool IsConnectedOFD { get; set; }

        /// <summary>
        /// Есть документы для отправки в ОФД.
        /// </summary>
        [JsonPropertyName("IsExistDocsToSend")]
        public bool IsExistDocsToSend { get; set; }

        /// <summary>
        /// Ожидается запрос от ОФД.
        /// </summary>
        [JsonPropertyName("IsWaitRequestFromOFD")]
        public bool IsWaitRequestFromOFD { get; set; }

        /// <summary>
        /// Есть команда от ОФД.
        /// </summary>
        [JsonPropertyName("IsExistCommandFromOFD")]
        public bool IsExistCommandFromOFD { get; set; }

        /// <summary>
        /// Изменены параметры соединения.
        /// </summary>
        [JsonPropertyName("IsConnectionParametersChanged")]
        public bool IsConnectionParametersChanged { get; set; }

        /// <summary>
        /// Ожидается ответ на команду от ОФД.
        /// </summary>
        [JsonPropertyName("WaitingForResponseToCommandFromOFD")]
        public bool WaitingForResponseToCommandFromOFD { get; set; }

        /// <summary>
        /// Количество непереданных документов.
        /// </summary>
        [JsonPropertyName("DocumentsCount")]
        public int DocumentsCount { get; set; }

        /// <summary>
        /// Номер первого непереданного документа.
        /// </summary>
        [JsonPropertyName("FirstDocumentNumber")]
        public long FirstDocumentNumber { get; set; }

        /// <summary>
        /// Дата первого непереданного документа.
        /// </summary>
        [JsonPropertyName("FirstDocumentDate")]
        public DateTime? FirstDocumentDate { get; set; }

        /// <summary>
        /// Сообщение ОФД прочитано.
        /// </summary>
        [JsonPropertyName("OfdMessageRead")]
        public bool OfdMessageRead { get; set; }
    }
}
