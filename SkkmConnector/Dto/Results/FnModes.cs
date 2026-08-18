using System.Text.Json.Serialization;

namespace SkkmConnector
{
    /// <summary>
    /// Режимы работы ККТ, заданные при фискализации
    /// </summary>
    public class FnModes
    {
        /// <summary>
        /// Принтер в автомате
        /// </summary>
        [JsonPropertyName("PrinterAutomatic")]
        public bool PrinterAutomatic { get; set; }

        /// <summary>
        /// Автономный режим (без передачи в ОФД)
        /// </summary>
        [JsonPropertyName("OfflineMode")]
        public bool OfflineMode { get; set; }

        /// <summary>
        /// Признак расчетов за услуги
        /// </summary>
        [JsonPropertyName("ServiceSign")]
        public bool ServiceSign { get; set; }

        /// <summary>
        /// Признак формирования БСО
        /// </summary>
        [JsonPropertyName("BsoSign")]
        public bool BsoSign { get; set; }

        /// <summary>
        /// ККТ для расчетов только в Интернет
        /// </summary>
        [JsonPropertyName("CalcOnlineSign")]
        public bool CalcOnlineSign { get; set; }

        /// <summary>
        /// Шифрование данных
        /// </summary>
        [JsonPropertyName("DataEncryption")]
        public bool DataEncryption { get; set; }

        /// <summary>
        /// Продажа подакцизного товара
        /// </summary>
        [JsonPropertyName("SaleExcisableGoods")]
        public bool SaleExcisableGoods { get; set; }

        /// <summary>
        /// Признак проведения азартных игр
        /// </summary>
        [JsonPropertyName("SignOfGambling")]
        public bool SignOfGambling { get; set; }

        /// <summary>
        /// Признак проведения лотереи
        /// </summary>
        [JsonPropertyName("SignOfLottery")]
        public bool SignOfLottery { get; set; }

        /// <summary>
        /// Ломбард
        /// </summary>
        [JsonPropertyName("Pawnshop")]
        public bool Pawnshop { get; set; }

        /// <summary>
        /// Страхование
        /// </summary>
        [JsonPropertyName("Assurance")]
        public bool Assurance { get; set; }

        /// <summary>
        /// Продажа маркированного товара
        /// </summary>
        [JsonPropertyName("Marking")]
        public bool Marking { get; set; }

        /// <summary>
        /// Вендинговый автомат
        /// </summary>
        [JsonPropertyName("VendingMachine")]
        public bool VendingMachine { get; set; }

        /// <summary>
        /// Общественное питание
        /// </summary>
        [JsonPropertyName("CateringServices")]
        public bool CateringServices { get; set; }

        /// <summary>
        /// Оптовая торговля
        /// </summary>
        [JsonPropertyName("WholesaleTrade")]
        public bool WholesaleTrade { get; set; }

        /// <summary>
        /// Автоматический режим
        /// </summary>
        [JsonPropertyName("AutomaticMode")]
        public bool AutomaticMode { get; set; }
    }
}
