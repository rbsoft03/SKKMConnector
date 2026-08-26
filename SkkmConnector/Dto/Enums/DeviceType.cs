namespace SkkmConnector
{
    /// <summary>
    /// Тип драйвера устройства.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// Shtrih.
        /// </summary>
        Shtrih = 1,

        /// <summary>
        /// 1С(4.7).
        /// </summary>
        Native1C = 2,

        /// <summary>
        /// Atol.
        /// </summary>
        AtolFRv10 = 3,

        /// <summary>
        /// RrElectro.
        /// </summary>
        RrElectro = 4,

        /// <summary>
        /// 1С(5.0).
        /// </summary>
        Native1C5000 = 5,

        /// <summary>
        /// Эмулятор.
        /// </summary>
        EmulatorFR = 100,
    }
}
