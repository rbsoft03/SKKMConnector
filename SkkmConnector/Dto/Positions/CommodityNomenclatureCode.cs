namespace RBSoftSkkm;

/// <summary>
/// Код товарной номенклатуры позиции (wire: GoodCodeData).
/// Собирается из плоских полей GoodCode* позиции <see cref="FiscalLine"/>.
/// </summary>
public sealed class CommodityNomenclatureCode
{
    /// <summary>
    /// Код контрольной марки в кодировке Base64.
    /// </summary>
    public string? MarkingCode { get; set; }

    /// <summary>
    /// GTIN.
    /// </summary>
    public string? Gtin { get; set; }

    /// <summary>
    /// Тип маркировки: "02" — мех, "05" — табак, "1520" — обувь.
    /// </summary>
    public string? StampType { get; set; }

    /// <summary>
    /// КиЗ.
    /// </summary>
    public string? Stamp { get; set; }

    /// <summary>
    /// Серийный номер.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Штрихкод.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Код неидентифицированного формата в кодировке Base64.
    /// </summary>
    public string? NotIdentified { get; set; }

    /// <summary>
    /// EAN-8 в кодировке Base64.
    /// </summary>
    public string? EAN8 { get; set; }

    /// <summary>
    /// EAN-13 в кодировке Base64.
    /// </summary>
    public string? EAN13 { get; set; }

    /// <summary>
    /// ITF-14 в кодировке Base64.
    /// </summary>
    public string? ITF14 { get; set; }

    /// <summary>
    /// GS10 (без маркировки) в кодировке Base64.
    /// </summary>
    public string? GS10 { get; set; }

    /// <summary>
    /// GS1 (с маркировкой) в кодировке Base64.
    /// </summary>
    public string? GS1M { get; set; }

    /// <summary>
    /// Короткий код маркировки в кодировке Base64.
    /// </summary>
    public string? KMK { get; set; }

    /// <summary>
    /// КиЗ мехового изделия.
    /// </summary>
    public string? MI { get; set; }

    /// <summary>
    /// ЕГАИС-2.0 в кодировке Base64.
    /// </summary>
    public string? EGAIS20 { get; set; }

    /// <summary>
    /// ЕГАИС-3.0 в кодировке Base64.
    /// </summary>
    public string? EGAIS30 { get; set; }

    /// <summary>
    /// Код формата Ф.1 в кодировке Base64.
    /// </summary>
    public string? F1 { get; set; }

    /// <summary>
    /// Код формата Ф.2 в кодировке Base64.
    /// </summary>
    public string? F2 { get; set; }

    /// <summary>
    /// Код формата Ф.3 в кодировке Base64.
    /// </summary>
    public string? F3 { get; set; }

    /// <summary>
    /// Код формата Ф.4 в кодировке Base64.
    /// </summary>
    public string? F4 { get; set; }

    /// <summary>
    /// Код формата Ф.5 в кодировке Base64.
    /// </summary>
    public string? F5 { get; set; }

    /// <summary>
    /// Код формата Ф.6 в кодировке Base64.
    /// </summary>
    public string? F6 { get; set; }
}
