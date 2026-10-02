namespace RBSoftSkkm;

/// <summary>
/// Класс (тип) устройства.
/// </summary>
public enum DeviceClass
{
    /// <summary>
    /// Принтер. Зарезервировано.
    /// </summary>
    Printer = 1,

    /// <summary>
    /// Чековый принтер. Зарезервировано.
    /// </summary>
    CheckPrinter = 2,

    /// <summary>
    /// Фискальный регистратор (не онлайн-ККМ). Зарезервировано.
    /// </summary>
    Fr = 3,

    /// <summary>
    /// Онлайн-ККМ, применяемая в РФ в соответствии с ФЗ-54.
    /// </summary>
    Kkt = 4,

    /// <summary>
    /// Эквайринговый терминал. Зарезервировано.
    /// </summary>
    Acquiring = 5,

    /// <summary>
    /// Терминал сбора данных. Зарезервировано.
    /// </summary>
    Tsd = 6,

    /// <summary>
    /// Электронные весы. Зарезервировано.
    /// </summary>
    Weigher = 7,

    /// <summary>
    /// Электронные весы с печатью этикеток. Зарезервировано.
    /// </summary>
    LabelWeigher = 8,

    /// <summary>
    /// Сканер штрихкодов. Зарезервировано.
    /// </summary>
    BarcodeScanner = 9
}
