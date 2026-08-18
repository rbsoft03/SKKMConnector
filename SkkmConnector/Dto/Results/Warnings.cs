using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Предупреждения фискального накопителя
    /// </summary>
    public class Warnings
    {
        /// <summary>
        /// Критическая ошибка ФН
        /// </summary>
        [JsonPropertyName("CriticalError")]
        public bool CriticalError { get; set; }

        /// <summary>
        /// Память ФН переполнена
        /// </summary>
        [JsonPropertyName("MemoryOverflow")]
        public bool MemoryOverflow { get; set; }

        /// <summary>
        /// Требуется срочная замена ФН
        /// </summary>
        [JsonPropertyName("NeedReplacement")]
        public bool NeedReplacement { get; set; }

        /// <summary>
        /// Превышено время ожидания подтверждения от ОФД
        /// </summary>
        [JsonPropertyName("OfdTimeout")]
        public bool OfdTimeout { get; set; }

        /// <summary>
        /// Ресурс ФН исчерпан
        /// </summary>
        [JsonPropertyName("ResourceExhausted")]
        public bool ResourceExhausted { get; set; }
    }
}
