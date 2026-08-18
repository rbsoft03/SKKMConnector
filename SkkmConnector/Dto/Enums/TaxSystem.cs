namespace SkkmConnector
{
    /// <summary>
    /// Код системы налогообложения.
    /// </summary>
    public enum TaxSystem
    {
        /// <summary>
        /// Общая
        /// </summary>
        ОСН = 0,

        /// <summary>
        /// Упрощенная (Доход).
        /// </summary>
        УСН = 1,

        /// <summary>
        /// Упрощенная (Доход минус Расход)
        /// </summary>
        УСНД_Р = 2,

        /// <summary>
        /// Единый налог на вмененный доход
        /// </summary>
        ЕНВД = 3,

        /// <summary>
        /// Единый сельскохозяйственный налог
        /// </summary>
        ЕСН = 4,

        /// <summary>
        /// Патентная система налогообложения
        /// </summary>
        ПСН = 5,
    }
}
