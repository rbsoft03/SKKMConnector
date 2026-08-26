using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Элемент списка изображений
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Название изображения.
        /// </summary>
        [JsonPropertyName("PictureName")]
        public string? PictureName { get; set; }

        /// <summary>
        /// Выравнивание: 1 — по левому краю; 2 — по центру; 3 — по правому краю.
        /// </summary>
        [JsonPropertyName("Alignment")]
        public int Alignment { get; set; }
    }
}
