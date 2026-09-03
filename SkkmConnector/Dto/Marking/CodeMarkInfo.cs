namespace SkkmConnector;

/// <summary>
/// Сведения о коде маркировки.
/// </summary>
public sealed class CodeMarkInfo
{
    /// <summary>
    /// Полный код маркировки (КиЗ).
    /// </summary>
    public string Cis { get; set; } = "";

    /// <summary>
    /// Признак валидности структуры кода.
    /// </summary>
    public bool Valid { get; set; }

    /// <summary>
    /// Код маркировки без крипто-подписи.
    /// </summary>
    public string PrintView { get; set; } = "";

    /// <summary>
    /// Идентификаторы товарных групп.
    /// </summary>
    public int[] GroupIds { get; set; } = [];

    /// <summary>
    /// Результат криптографической проверки кода.
    /// </summary>
    public bool Verified { get; set; }

    /// <summary>
    /// Признак статуса «В обороте».
    /// </summary>
    public bool Realizable { get; set; }

    /// <summary>
    /// Признак нанесения кода на упаковку.
    /// </summary>
    public bool Utilised { get; set; }

    /// <summary>
    /// Признак наличия кода в ГИС МТ.
    /// </summary>
    public bool Found { get; set; }

    /// <summary>
    /// Код ошибки проверки.
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    /// Признак старта прослеживаемости в товарной группе.
    /// </summary>
    public bool IsTracking { get; set; }

    /// <summary>
    /// Признак того, что товар с данным кодом уже продан.
    /// </summary>
    public bool Sold { get; set; }

    /// <summary>
    /// Код товара (GTIN).
    /// </summary>
    public string Gtin { get; set; } = "";

    /// <summary>
    /// Тип упаковки.
    /// </summary>
    public string PackageType { get; set; } = "";

    /// <summary>
    /// ИНН производителя.
    /// </summary>
    public string ProducerInn { get; set; } = "";

    /// <summary>
    /// Признак нахождения продукции в «серой зоне».
    /// </summary>
    public bool GrayZone { get; set; }

    /// <summary>
    /// Признак блокировки кода по решению ОГВ.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Признак некорректного (незарегистрированного) GTIN.
    /// </summary>
    public bool IsGreyGtin { get; set; }

    /// <summary>
    /// Органы государственной власти, установившие блокировку.
    /// </summary>
    public string[] Ogvs { get; set; } = [];

    /// <summary>
    /// Ёмкость КИГУ (количество потенциальных вложений).
    /// </summary>
    public int PackageQuantity { get; set; }
}
