using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Состояние кассовой смены после GetShiftStatus.
    /// </summary>
    public class ResponseCurrentStatus
    {
        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Номер последнего фискального документа.
        /// </summary>
        [JsonPropertyName("CheckNumber")]
        public int CheckNumber { get; set; }

        /// <summary>
        /// 1 — закрыта, 2 — открыта, 3 — истекла
        /// </summary>
        [JsonPropertyName("ShiftState")]
        public ShiftState ShiftState { get; set; }

        [JsonPropertyName("Backlog")]
        public Backlog? Backlog { get; set; }
    }
}
