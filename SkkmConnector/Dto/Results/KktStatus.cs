using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Состояние ККМ после GetStatus / Connect.
    /// </summary>
    public class KktStatus
    {
        [JsonPropertyName("IsFnPresent")]
        public bool IsFnPresent { get; set; }

        [JsonPropertyName("IsFnError")]
        public bool IsFnError { get; set; }

        [JsonPropertyName("IsIsmDisconnected")]
        public bool IsIsmDisconnected { get; set; }

        [JsonPropertyName("IsOfdDisconnected")]
        public bool IsOfdDisconnected { get; set; }

        [JsonPropertyName("Warnings")]
        public Warnings? Warnings { get; set; }

        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        [JsonPropertyName("DocNumber")]
        public int DocNumber { get; set; }

        [JsonPropertyName("IsFiscal")]
        public bool IsFiscal { get; set; }

        [JsonPropertyName("IsShiftOpened")]
        public bool IsShiftOpened { get; set; }

        [JsonPropertyName("IsShiftExpired")]
        public bool IsShiftExpired { get; set; }

        [JsonPropertyName("ComputerTime")]
        public DateTime ComputerTime { get; set; }

        [JsonPropertyName("DeviceTime")]
        public DateTime DeviceTime { get; set; }

        [JsonPropertyName("IsDrawerOpened")]
        public bool IsDrawerOpened { get; set; }

        [JsonPropertyName("IsCheckPaperPresent")]
        public bool IsCheckPaperPresent { get; set; }

        [JsonPropertyName("IsCoverOpened")]
        public bool IsCoverOpened { get; set; }

        [JsonPropertyName("IsBatteryLow")]
        public bool IsBatteryLow { get; set; }

        [JsonPropertyName("IsOpenDocument")]
        public bool IsOpenDocument { get; set; }

        [JsonPropertyName("LineLength")]
        public int LineLength { get; set; }

        /// <summary>
        /// Состояние смены по флагам: закрыта / открыта / истекла.
        /// </summary>
        [JsonIgnore]
        public ShiftState ShiftState
            => IsShiftExpired ? ShiftState.Expired : IsShiftOpened ? ShiftState.Opened : ShiftState.Closed;
    }
}
