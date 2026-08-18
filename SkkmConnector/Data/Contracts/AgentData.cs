using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Данные агента в строке чека.
    /// </summary>
    internal class AgentData
    {
        [JsonPropertyName("PayingAgentOperation")]
        public string? PayingAgentOperation { get; set; }

        [JsonPropertyName("PayingAgentPhone")]
        public string[]? PayingAgentPhone { get; set; }

        [JsonPropertyName("ReceivePaymentsOperatorPhone")]
        public string[]? ReceivePaymentsOperatorPhone { get; set; }

        [JsonPropertyName("MoneyTransferOperatorPhone")]
        public string[]? MoneyTransferOperatorPhone { get; set; }

        [JsonPropertyName("MoneyTransferOperatorName")]
        public string? MoneyTransferOperatorName { get; set; }

        [JsonPropertyName("MoneyTransferOperatorAddress")]
        public string? MoneyTransferOperatorAddress { get; set; }

        [JsonPropertyName("MoneyTransferOperatorVatin")]
        public string? MoneyTransferOperatorVatin { get; set; }
    }
}
