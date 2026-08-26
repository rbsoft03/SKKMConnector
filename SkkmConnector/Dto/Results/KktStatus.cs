using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Состояние ККТ 
    /// </summary>
    public class KktStatus
    {
        /// <summary>
        /// Присутствует ли фискальный накопитель.
        /// </summary>
        [JsonPropertyName("IsFnPresent")]
        public bool IsFnPresent { get; set; }

        /// <summary>
        /// Находится ли фискальный накопитель в состоянии ошибки.
        /// </summary>
        [JsonPropertyName("IsFnError")]
        public bool IsFnError { get; set; }

        /// <summary>
        /// Доступна ли информационная система маркировки.
        /// </summary>
        [JsonPropertyName("IsIsmDisconnected")]
        public bool IsIsmDisconnected { get; set; }

        /// <summary>
        /// Доступен ли оператор фискальных данных.
        /// </summary>
        [JsonPropertyName("IsOfdDisconnected")]
        public bool IsOfdDisconnected { get; set; }

        /// <summary>
        /// Предупреждения ФН
        /// </summary>
        [JsonPropertyName("Warnings")]
        public Warnings? Warnings { get; set; }

        /// <summary>
        /// Номер смены.
        /// </summary>
        [JsonPropertyName("ShiftNumber")]
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Номер фискального документа.
        /// </summary>
        [JsonPropertyName("DocNumber")]
        public int DocNumber { get; set; }

        /// <summary>
        /// Фискальный режим.
        /// </summary>
        [JsonPropertyName("IsFiscal")]
        public bool IsFiscal { get; set; }

        /// <summary>
        /// Смена открыта.
        /// </summary>
        [JsonPropertyName("IsShiftOpened")]
        public bool IsShiftOpened { get; set; }

        /// <summary>
        /// Смена истекла.
        /// </summary>
        [JsonPropertyName("IsShiftExpired")]
        public bool IsShiftExpired { get; set; }

        /// <summary>
        /// Время получения данных.
        /// </summary>
        [JsonPropertyName("ComputerTime")]
        public DateTime ComputerTime { get; set; }

        /// <summary>
        /// Время в часах устройства.
        /// </summary>
        [JsonPropertyName("DeviceTime")]
        public DateTime DeviceTime { get; set; }

        /// <summary>
        /// Открыт денежный ящик.
        /// </summary>
        [JsonPropertyName("IsDrawerOpened")]
        public bool IsDrawerOpened { get; set; }

        /// <summary>
        /// Наличие чековой ленты.
        /// </summary>
        [JsonPropertyName("IsCheckPaperPresent")]
        public bool IsCheckPaperPresent { get; set; }

        /// <summary>
        /// Открыта ли крышка.
        /// </summary>
        [JsonPropertyName("IsCoverOpened")]
        public bool IsCoverOpened { get; set; }

        /// <summary>
        /// Аккумулятор разряжен.
        /// </summary>
        [JsonPropertyName("IsBatteryLow")]
        public bool IsBatteryLow { get; set; }

        /// <summary>
        /// Открытый документ.
        /// </summary>
        [JsonPropertyName("IsOpenDocument")]
        public bool IsOpenDocument { get; set; }

        /// <summary>
        /// Ширина чековой ленты.
        /// </summary>
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
