namespace SkkmConnector
{
    /// <summary>
    /// Тип штрихкода для печати в документе.
    /// </summary>
    public enum BarcodeType
    {
        /// <summary>
        /// QR-код
        /// </summary>
        QR,

        /// <summary>
        /// EAN-13
        /// </summary>
        EAN13,

        /// <summary>
        /// EAN-8
        /// </summary>
        EAN8,

        /// <summary>
        /// Code 39
        /// </summary>
        CODE39,

        /// <summary>
        /// Code 93
        /// </summary>
        CODE93,

        /// <summary>
        /// Code 128
        /// </summary>
        CODE128,

        /// <summary>
        /// UPC-A
        /// </summary>
        UPCA,

        /// <summary>
        /// UPC-E
        /// </summary>
        UPCE,

        /// <summary>
        /// Interleaved 2 of 5
        /// </summary>
        ITF,

        /// <summary>
        /// Codabar
        /// </summary>
        CODABAR,

        /// <summary>
        /// PDF417
        /// </summary>
        PDF417,

        /// <summary>
        /// Code 32
        /// </summary>
        CODE32,
    }
}
