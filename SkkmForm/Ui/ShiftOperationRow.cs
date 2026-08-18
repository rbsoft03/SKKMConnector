namespace SkkmConnector.Ui
{
    /// <summary>
    /// Строка таблицы фискальных операций за смену на вкладке "Кассовые смены"
    /// </summary>
    public class ShiftOperationRow
    {
        /// <summary>
        /// Вид операции (приход, возврат прихода и т.д.)
        /// </summary>
        public string Operation { get; set; } = "";

        /// <summary>
        /// Количество документов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Сумма по документам
        /// </summary>
        public decimal Sum { get; set; }
    }
}
