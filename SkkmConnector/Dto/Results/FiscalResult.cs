using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Результат фискальной операции из ответа сервера (после печати чека, коррекции, открытия/закрытия смены и т.п.).
    /// Заполняется автоматически после вызова; поля, которых нет в конкретном ответе, остаются пустыми.
    /// </summary>
    public class FiscalResult
    {
        /// <summary>
        /// Фискальный признак документа. Можно передать в PrintCheckCopy для печати копии.
        /// </summary>
        [JsonPropertyName("fiscalSign")]
        public string? FiscalSign { get; set; }

        /// <summary>
        /// Номер фискального документа.
        /// </summary>
        [JsonPropertyName("fiscalNumber")]
        public int FiscalNumber { get; set; }

        /// <summary>
        /// Номер смены.
        /// </summary>
        [JsonPropertyName("shiftNumber")]
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Идентификатор документа на сервере.
        /// </summary>
        [JsonPropertyName("docId")]
        public string? DocId { get; set; }

        /// <summary>
        /// Время операции по данным сервера (строка ISO).
        /// </summary>
        [JsonPropertyName("datetime")]
        public string? DateTime { get; set; }

        /// <summary>
        /// Фискальное время документа (строка).
        /// </summary>
        [JsonPropertyName("fiscalDatetime")]
        public string? FiscalDateTime { get; set; }

        /// <summary>
        /// Номер ФН.
        /// </summary>
        [JsonPropertyName("fnNumber")]
        public string? FnNumber { get; set; }

        /// <summary>
        /// Регистрационный номер ККТ.
        /// </summary>
        [JsonPropertyName("rnNumber")]
        public string? RnNumber { get; set; }

        /// <summary>
        /// Адрес проверки чека на сайте ФНС.
        /// </summary>
        [JsonPropertyName("fnsUrl")]
        public string? FnsUrl { get; set; }
    }
}
