using System;
using System.Text.Json.Serialization;

namespace RBSoftSkkm
{
    /// <summary>
    /// Лицензия сервера ККМ на устройство.
    /// </summary>
    public class ServerLicense
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Лицензия выдана конечному пользователю (false — партнёру для перепродажи).
        /// </summary>
        [JsonPropertyName("isEndUser")]
        public bool IsEndUser { get; set; }

        /// <summary>
        /// Признак активации.
        /// </summary>
        [JsonPropertyName("isActivated")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// Признак блокировки.
        /// </summary>
        [JsonPropertyName("isBlocked")]
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Дата блокировки.
        /// </summary>
        [JsonPropertyName("blockDate")]
        public DateTime BlockDate { get; set; }

        /// <summary>
        /// Дата продажи.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Дата истечения срока действия.
        /// </summary>
        [JsonPropertyName("expired")]
        public DateTime Expired { get; set; }

        /// <summary>
        /// Дата истечения доступа к обновлениям.
        /// </summary>
        [JsonPropertyName("updateExpired")]
        public DateTime UpdateExpired { get; set; }

        /// <summary>
        /// Количество ККМ, которые можно привязать к одной лицензии.
        /// </summary>
        [JsonPropertyName("limitInstalls")]
        public int LimitInstalls { get; set; }

        /// <summary>
        /// Требуется привязка объектов лицензирования после привязки.
        /// </summary>
        [JsonPropertyName("needObjectActivation")]
        public bool NeedObjectActivation { get; set; }

        /// <summary>
        /// Количество объектов привязки на каждую установку.
        /// </summary>
        [JsonPropertyName("limitObjects")]
        public int LimitObjects { get; set; }

        /// <summary>
        /// Индекс установочного токена.
        /// </summary>
        [JsonPropertyName("setupTokenIndex")]
        public int SetupTokenIndex { get; set; }

        /// <summary>
        /// Время последней проверки лицензии.
        /// </summary>
        [JsonPropertyName("licenseUpdated")]
        public DateTime LicenseUpdated { get; set; }
    }
}
