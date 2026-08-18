using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Позиция чека: фискальная строка, либо текст/штрихкод.
    /// </summary>
    internal class Position : DocPosition
    {
        /// <summary>
        /// Фискальная строка.
        /// </summary>
        [JsonPropertyName("FiscalString")]
        public FiscalString? FiscalString { get; set; }
    }
}
