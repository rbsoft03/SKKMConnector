using System.Text.Json.Serialization;

namespace RBSoftSkkm
{
    /// <summary>
    /// Ответ операций маркировки без данных: открытие/закрытие сессии регистрации КМ, подтверждение КМ.
    /// </summary>
    public class BooleanResponse
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        [JsonPropertyName("Code")]
        public int Code { get; set; }

        /// <summary>
        /// Описание результата.
        /// </summary>
        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        /// <summary>
        /// Признак успешного выполнения операции.
        /// </summary>
        [JsonPropertyName("Success")]
        public bool Success { get; set; }
    }
}
