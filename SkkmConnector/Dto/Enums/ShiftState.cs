namespace SkkmConnector
{
    /// <summary>
    /// Состояние кассовой смены.
    /// </summary>
    public enum ShiftState
    {
        /// <summary>
        /// Смена закрыта
        /// </summary>
        Closed = 1,

        /// <summary>
        /// Смена открыта
        /// </summary>
        Opened = 2,

        /// <summary>
        /// Смена истекла (открыта более 24 часов)
        /// </summary>
        Expired = 3,
    }
}
