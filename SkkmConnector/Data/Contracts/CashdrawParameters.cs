using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Внесения/выемки наличных.
    /// </summary>
    internal class CashdrawParameters : CheckbaseParameters
    {
        /// <summary>
        /// Сумма внесения или выемки
        /// </summary>
        [JsonPropertyName("Sum")]
        public decimal Sum { get; set; }
    }
}
