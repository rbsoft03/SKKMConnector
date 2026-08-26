using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Позиция чека: фискальная строка, либо текст/штрихкод.
    /// </summary>
    internal class ApiPosition : DocPosition
    {
        /// <summary>
        /// Фискальная строка.
        /// </summary>
        [JsonPropertyName("FiscalString")]
        public FiscalLine? FiscalString { get; set; }
    }
}
