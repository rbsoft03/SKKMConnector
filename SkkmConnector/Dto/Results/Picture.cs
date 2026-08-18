using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Картинка на сервере (элемент списка после GetPictureList)
    /// </summary>
    public class Picture
    {
        [JsonPropertyName("PictureName")]
        public string? PictureName { get; set; }

        /// <summary>
        /// Выравнивание: 1 - слева, 2 - по центру, 3 - справа
        /// </summary>
        [JsonPropertyName("Alignment")]
        public int Alignment { get; set; }
    }
}
