using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Краткий статус смены и очереди ОФД (ResponseCurrentStatus, GET kkt/shift/status).
    /// </summary>
    public class ResponseCurrentStatus
    {
        /// <summary>
        /// Номер смены.
        /// </summary>
        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Номер последнего фискального документа.
        /// </summary>
        [JsonPropertyName("CheckNumber")]
        public int CheckNumber { get; set; }

        /// <summary>
        /// Состояние смены: 1 — закрыта, 2 — открыта, 3 — истекла.
        /// </summary>
        [JsonPropertyName("ShiftState")]
        public ShiftState ShiftState { get; set; }

        /// <summary>
        /// Статус обмена данными с ОФД.
        /// </summary>
        [JsonPropertyName("Backlog")]
        public Backlog? Backlog { get; set; }
    }
}
