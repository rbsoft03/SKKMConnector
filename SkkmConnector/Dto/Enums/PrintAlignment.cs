namespace SkkmConnector
{
    /// <summary>
    /// Выравнивание строки или штрихкода при печати:
    /// <para>
    /// Left - По левому краю
    /// </para>
    /// <para>
    /// Center - По центру
    /// </para>
    /// <para>
    /// Right - По правому краю
    /// </para>
    /// <para>
    /// Width - На всю ширину
    /// </para>
    /// </summary>
    public enum PrintAlignment
    {
        /// <summary>
        /// По левому краю.
        /// </summary>
        Left = 0,

        /// <summary>
        /// По центру.
        /// </summary>
        Center = 1,

        /// <summary>
        /// По правому краю.
        /// </summary>
        Right = 2,

        /// <summary>
        /// На всю ширину.
        /// </summary>
        Width = 3,
    }
}
