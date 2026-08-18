using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Ответ GET kkt/lineLength.
    /// </summary>
    internal class LineLengthV2
    {
        [JsonPropertyName("LineLength")]
        public int LineLength { get; set; }

        [JsonPropertyName("LineLengthPixels")]
        public int LineLengthPixels { get; set; }
    }
}
