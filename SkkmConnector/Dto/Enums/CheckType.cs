namespace SkkmConnector
{
    /// <summary>
    /// Тип чека.
    /// </summary>
    public enum CheckType
    {
        /// <summary>
        /// Отмена чека
        /// </summary>
        CancelCheck = 0,

        /// <summary>
        /// Приход (продажа)
        /// </summary>
        Sale = 1,

        /// <summary>
        /// Возврат прихода
        /// </summary>
        SaleReturn = 2,

        /// <summary>
        /// Аннулирование
        /// </summary>
        Annulment = 3,

        /// <summary>
        /// Расход (покупка)
        /// </summary>
        Purchase = 4,

        /// <summary>
        /// Возврат расхода
        /// </summary>
        PurchaseReturn = 5,

        /// <summary>
        /// Аннулирование покупки
        /// </summary>
        PurchaseAnnulment = 6,

        /// <summary>
        /// Чек коррекции прихода
        /// </summary>
        CorrectionSale = 7,

        /// <summary>
        /// Чек коррекции возврата прихода
        /// </summary>
        CorrectionSaleReturn = 8,

        /// <summary>
        /// Чек коррекции расхода
        /// </summary>
        CorrectionPurchase = 9,

        /// <summary>
        /// Чек коррекции возврата расхода
        /// </summary>
        CorrectionPurchaseReturn = 10,
    }
}
