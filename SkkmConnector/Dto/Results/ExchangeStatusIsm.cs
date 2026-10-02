using System;
using System.Text.Json.Serialization;

namespace RBSoftSkkm
{
    /// <summary>
    /// Состояние обмена с информационной системой маркировки (ИСМ).
    /// </summary>
    public class ExchangeStatusIsm
    {
        /// <summary>
        /// Адрес сервера ИСМ.
        /// </summary>
        [JsonPropertyName("Address")]
        public string? Address { get; set; }

        /// <summary>
        /// Порт сервера ИСМ.
        /// </summary>
        [JsonPropertyName("Port")]
        public int Port { get; set; }

        /// <summary>
        /// Сведения об ошибках обмена данными.
        /// </summary>
        [JsonPropertyName("Errors")]
        public ErrorsIsm? Errors { get; set; }

        /// <summary>
        /// Сведения о непереданных в ИСМ документах.
        /// </summary>
        [JsonPropertyName("Backlog")]
        public Backlog? Backlog { get; set; }
    }

    /// <summary>
    /// Ошибки обмена с ИСМ.
    /// </summary>
    public class ErrorsIsm
    {
        /// <summary>
        /// Код команды фискального накопителя.
        /// </summary>
        [JsonPropertyName("FnCommandCode")]
        public int FnCommandCode { get; set; }

        /// <summary>
        /// Номер документа.
        /// </summary>
        [JsonPropertyName("DocumentNumber")]
        public int DocumentNumber { get; set; }

        /// <summary>
        /// Время последнего успешного подключения.
        /// </summary>
        [JsonPropertyName("LastSuccessConnectionDateTime")]
        public DateTime LastSuccessConnectionDateTime { get; set; }

        /// <summary>
        /// Ошибка фискального накопителя.
        /// </summary>
        [JsonPropertyName("Fn")]
        public ExchangeError? Fn { get; set; }

        /// <summary>
        /// Ошибка сети.
        /// </summary>
        [JsonPropertyName("Network")]
        public ExchangeError? Network { get; set; }

        /// <summary>
        /// Ошибка ИСМ.
        /// </summary>
        [JsonPropertyName("Ism")]
        public ExchangeError? Ism { get; set; }
    }

    /// <summary>
    /// Код и описание ошибки обмена (ФН, сеть, ИСМ).
    /// </summary>
    public class ExchangeError
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        [JsonPropertyName("Code")]
        public int Code { get; set; }

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        [JsonPropertyName("Description")]
        public string? Description { get; set; }
    }
}
