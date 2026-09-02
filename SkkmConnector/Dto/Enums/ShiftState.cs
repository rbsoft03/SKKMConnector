namespace SkkmConnector
{
    /// <summary>
    /// Состояние кассовой смены:
    /// <para>
    /// Closed - Смена закрыта
    /// </para>
    /// <para>
    /// Opened - Смена открыта
    /// </para>
    /// <para>
    /// Expired - Смена истекла (открыта более 24 часов)
    /// </para>
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
