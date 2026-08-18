using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса внесения/выемки наличных: POST cashin / POST cashout
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
