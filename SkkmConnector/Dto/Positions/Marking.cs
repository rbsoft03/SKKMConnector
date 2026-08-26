using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Код товарной номенклатуры
/// </summary>
public sealed class Marking
{
    /// <summary>
    /// Глобальный идентификатор торговой единицы (GTIN)
    /// </summary>
    public string? Gtin { get; set; }

    /// <summary>
    /// Тип маркировки. Список значений:
    /// "02" – изделия из меха
    /// "05" - табачная продукция
    /// "1520" - обувные товары
    /// </summary>
    public string? StampType { get; set; }

    /// <summary>
    /// Контрольный идентификационный знак (КиЗ)
    /// </summary>
    public string? Stamp { get; set; }

    /// <summary>
    /// Серийный номер
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Код контрольной марки. Кодируется текстом в кодировке Base64
    /// </summary>
    [JsonPropertyName("MarkingCode")]
    public string? Code { get; set; }

    /// <summary>
    /// Штрихкод
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Тип (группа) товара
    /// </summary>
    public string? CommodityGroup { get; set; }

    /// <summary>
    /// Код товара, формат которого не идентифицирован в Base64
    /// </summary>
    public string? NotIdentified { get; set; }

    /// <summary>
    /// Код товара в формате EAN-8 в Base64
    /// </summary>
    [JsonPropertyName("EAN8")]
    public string? Ean8 { get; set; }

    /// <summary>
    /// Код товара в формате EAN-13 в Base64
    /// </summary>
    [JsonPropertyName("EAN13")]
    public string? Ean13 { get; set; }

    /// <summary>
    /// Код товара в формате ITF-14 в Base64
    /// </summary>
    [JsonPropertyName("ITF14")]
    public string? Itf14 { get; set; }

    /// <summary>
    /// Код товара в формате GS1, нанесенный на товар, не подлежащий маркировке средствами идентификации в Base64
    /// </summary>
    [JsonPropertyName("GS10")]
    public string? Gs10 { get; set; }

    /// <summary>
    /// Код товара в формате GS1, нанесенный на товар, подлежащий маркировке средствами идентификации в Base64
    /// </summary>
    [JsonPropertyName("GS1M")]
    public string? Gs1m { get; set; }

    /// <summary>
    /// Код товара в формате короткого кода маркировки, нанесенный на товар, подлежащий маркировке средствами идентификации в Base64
    /// </summary>
    [JsonPropertyName("KMK")]
    public string? Kmk { get; set; }

    /// <summary>
    /// Контрольно-идентификационный знак мехового изделия
    /// </summary>
    [JsonPropertyName("MI")]
    public string? Mi { get; set; }

    /// <summary>
    /// Код товара в формате ЕГАИС-2.0 в Base64
    /// </summary>
    [JsonPropertyName("EGAIS20")]
    public string? Egais20 { get; set; }

    /// <summary>
    /// Код товара в формате ЕГАИС-3.0 в Base64
    /// </summary>
    [JsonPropertyName("EGAIS30")]
    public string? Egais30 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.1 в Base64
    /// </summary>
    public string? F1 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.2 в Base64
    /// </summary>
    public string? F2 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.3 в Base64
    /// </summary>
    public string? F3 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.4 в Base64
    /// </summary>
    public string? F4 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.5 в Base64
    /// </summary>
    public string? F5 { get; set; }

    /// <summary>
    /// Код товара в формате Ф.6 в Base64
    /// </summary>
    public string? F6 { get; set; }
}
