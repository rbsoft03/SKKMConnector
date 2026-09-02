namespace SkkmConnector
{
    /// <summary>
    /// Тип штрихкода для печати в документе.
    /// <para>
    /// QR - QR-код
    /// </para>
    /// <para>
    /// EAN13 - EAN-13
    /// </para>
    /// <para>
    /// EAN8 - EAN-8
    /// </para>
    /// <para>
    /// EAN13 - EAN-13
    /// </para>
    /// <para>
    /// CODE39 - Code 39
    /// </para>
    /// <para>
    /// CODE93 - Code 93
    /// </para>
    /// <para>
    /// CODE128 - Code 128
    /// </para>
    /// <para>
    /// UPCA - UPC-A
    /// </para>
    /// <para>
    /// UPCE - UPC-E
    /// </para>
    /// <para>
    /// ITF - Interleaved 2 of 5
    /// </para>
    /// <para>
    /// CODABAR - Codabar
    /// </para>
    /// <para>
    /// PDF417 - PDF417
    /// </para>
    /// <para>
    /// CODE32 - Code 32
    /// </para>
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
