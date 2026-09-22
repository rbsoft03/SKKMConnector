using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Сериализует <see cref="TaxRate"/> в строку ставки НДС, которую ждёт сервер ККМ
/// (none, 0, 5, 7, 10, 20, 22, 5/105, 7/107, 10/110, 18/118, 20/120, 22/122).
/// </summary>
public sealed class TaxRateJsonConverter : JsonConverter<TaxRate>
{
    private static string ToText(TaxRate rate) => rate switch
    {
        TaxRate.None => "none",
        TaxRate.Vat0 => "0",
        TaxRate.Vat5 => "5",
        TaxRate.Vat7 => "7",
        TaxRate.Vat10 => "10",
        TaxRate.Vat20 => "20",
        TaxRate.Vat22 => "22",
        TaxRate.Vat5_105 => "5/105",
        TaxRate.Vat7_107 => "7/107",
        TaxRate.Vat10_110 => "10/110",
        TaxRate.Vat20_120 => "20/120",
        TaxRate.Vat22_122 => "22/122",
        _ => "none"
    };

    private static TaxRate FromText(string? text) => text switch
    {
        "none" or "" or null => TaxRate.None,
        "0" => TaxRate.Vat0,
        "5" => TaxRate.Vat5,
        "7" => TaxRate.Vat7,
        "10" => TaxRate.Vat10,
        "20" => TaxRate.Vat20,
        "22" => TaxRate.Vat22,
        "5/105" => TaxRate.Vat5_105,
        "7/107" => TaxRate.Vat7_107,
        "10/110" => TaxRate.Vat10_110,
        "20/120" => TaxRate.Vat20_120,
        "22/122" => TaxRate.Vat22_122,
        _ => TaxRate.None
    };

    public override TaxRate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => FromText(reader.GetString());

    public override void Write(Utf8JsonWriter writer, TaxRate value, JsonSerializerOptions options)
        => writer.WriteStringValue(ToText(value));
}
