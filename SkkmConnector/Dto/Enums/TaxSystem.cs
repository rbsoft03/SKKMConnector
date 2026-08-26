namespace SkkmConnector
{
    /// <summary>
    /// Система налогообложения
    /// </summary>
    public enum TaxSystem
    {
        /// <summary>
        /// ОСН.
        /// </summary>
        ОСН = 0,

        /// <summary>
        /// УСН (доход).
        /// </summary>
        УСН = 1,

        /// <summary>
        /// УСН доход минус расход.
        /// </summary>
        УСНД_Р = 2,

        /// <summary>
        /// ЕНВД.
        /// </summary>
        ЕНВД = 3,

        /// <summary>
        /// ЕСН.
        /// </summary>
        ЕСН = 4,

        /// <summary>
        /// ПСН.
        /// </summary>
        ПСН = 5,
    }
}
