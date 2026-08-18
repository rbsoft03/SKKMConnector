using System.Text.Json.Serialization;

namespace SkkmConnector.Internal
{
    /// <summary>
    /// Тело запроса загрузки картинки на сервер: POST picture
    /// </summary>
    internal class UploadPicture
    {
        /// <summary>
        /// Имя кассы, зарегистрированной на сервере
        /// </summary>
        [JsonPropertyName("DeviceName")]
        public string? DeviceName { get; set; }

        /// <summary>
        /// Изображение в формате Base64 (BMP)
        /// </summary>
        [JsonPropertyName("Base64")]
        public string? Base64 { get; set; }

        /// <summary>
        /// Имя картинки на сервере
        /// </summary>
        [JsonPropertyName("PictureName")]
        public string? PictureName { get; set; }

        /// <summary>
        /// Выравнивание: 1 - слева, 2 - по центру, 3 - справа
        /// </summary>
        [JsonPropertyName("Alignment")]
        public int Alignment { get; set; } = 2;
    }
}
