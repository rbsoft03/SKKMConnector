namespace SkkmConnector
{
    /// <summary>
    /// Стиль разделительной линии: 0 — сплошная; 1 — жирная; 2 — штриховая; 3 — пунктирная; 4 — двойная.
    /// </summary>
    public enum LineStyle
    {
        /// <summary>
        /// Сплошная линия (по умолчанию).
        /// </summary>
        Solid,

        /// <summary>
        /// Жирная линия.
        /// </summary>
        Bold,

        /// <summary>
        /// Штриховая линия.
        /// </summary>
        Dashed,

        /// <summary>
        /// Пунктирная линия.
        /// </summary>
        Dotted,

        /// <summary>
        /// Двойная линия.
        /// </summary>
        Double,
    }
}
