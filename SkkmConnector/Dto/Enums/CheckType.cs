namespace SkkmConnector
{
    /// <summary>
    /// Тип чека
    /// </summary>
    public enum CheckType
    {
        /// <summary>
        /// Не используется.
        /// </summary>
        None = 0,

        /// <summary>
        /// Продажа (приход).
        /// </summary>
        Sale = 1,

        /// <summary>
        /// Возврат (возврат прихода).
        /// </summary>
        SaleReturn = 2,

        /// <summary>
        /// Покупка (расход).
        /// </summary>
        Purchase = 3,

        /// <summary>
        /// Возврат покупки (возврат расхода).
        /// </summary>
        PurchaseReturn = 4,

        /// <summary>
        /// Чек коррекции прихода.
        /// </summary>
        CorrectionSale = 5,

        /// <summary>
        /// Чек коррекции возврата прихода.
        /// </summary>
        CorrectionSaleReturn = 6,

        /// <summary>
        /// Чек коррекции расхода.
        /// </summary>
        CorrectionPurchase = 7,

        /// <summary>
        /// Чек коррекции возврата расхода.
        /// </summary>
        CorrectionPurchaseReturn = 8,
    }
}
