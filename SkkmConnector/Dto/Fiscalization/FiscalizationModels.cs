using System.Text.Json.Serialization;
using SkkmConnector.Internal;

namespace SkkmConnector;

/// <summary>
/// Параметры фискализации ККТ.
/// </summary>
public sealed class FiscalizationParameters
{
    public string DeviceName { get; set; } = "";
    public Cashier? Cashier { get; set; }
    public string RnNumber { get; set; } = "";
    public string TaxationSystems { get; set; } = "";
    public string Vatin { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string Fn { get; set; } = "";
    public string FfdVersionKkt { get; set; } = "";
    public string FfdVersionFn { get; set; } = "";
    public string RegistrationLabelCodes { get; set; } = "";
    public string OfdAddress { get; set; } = "";
    public int OfdPort { get; set; }
    public string AutomaticNumber { get; set; } = "";
    public string SenderEmail { get; set; } = "";
    public int ReasonCode { get; set; }
    public string IsmHost { get; set; } = "";
    public int IsmPort { get; set; }
    public string FnsUrl { get; set; } = "";
    public string OfdVatin { get; set; } = "";
    public string OfdName { get; set; } = "";
    public string AgentTypes { get; set; } = "";
    public bool IsBsoSign { get; set; }
    public bool IsMarking { get; set; }
    public bool IsPawnshop { get; set; }
    public bool IsAssurance { get; set; }
    public bool IsAutomatic { get; set; }
    public bool IsVending { get; set; }
    public bool IsAutomaticPrinter { get; set; }
    public bool IsOnline { get; set; }
    public bool IsLottery { get; set; }
    public bool IsGambling { get; set; }
    public bool IsExcisable { get; set; }
    public bool IsService { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsOffline { get; set; }
    public bool IsCateringServices { get; set; }
    public bool IsWholesaleTrade { get; set; }
    public string SaleAddress { get; set; } = "";
    public string SaleLocation { get; set; } = "";
}

/// <summary>
/// Результат фискализации.
/// </summary>
public sealed class FiscalizationDocument
{
    public int OperationType { get; set; }
    public string RnNumber { get; set; } = "";
    public string TaxationSystems { get; set; } = "";
    public string Vatin { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string FfdVersionKkt { get; set; } = "";
    public string FfdVersionFn { get; set; } = "";
    public bool IsFiscal { get; set; }
    public string DocId { get; set; } = "";
    public string DeviceName { get; set; } = "";
    public int ShiftNumber { get; set; }
    public int DocNumber { get; set; }
    public string FiscalSign { get; set; } = "";
}

internal sealed class FiscalizationRequest : CheckbaseParameters
{
    [JsonPropertyName("RnNumber")]
    public string? RnNumber { get; set; }

    [JsonPropertyName("TaxationSystems")]
    public string? TaxationSystems { get; set; }

    [JsonPropertyName("Vatin")]
    public string? Vatin { get; set; }

    [JsonPropertyName("CompanyName")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("Fn")]
    public string? Fn { get; set; }

    [JsonPropertyName("FfdVersionKkt")]
    public string? FfdVersionKkt { get; set; }

    [JsonPropertyName("FfdVersionFn")]
    public string? FfdVersionFn { get; set; }

    [JsonPropertyName("RegistrationLabelCodes")]
    public string? RegistrationLabelCodes { get; set; }

    [JsonPropertyName("OfdAddress")]
    public string? OfdAddress { get; set; }

    [JsonPropertyName("OfdPort")]
    public int? OfdPort { get; set; }

    [JsonPropertyName("AutomaticNumber")]
    public string? AutomaticNumber { get; set; }

    [JsonPropertyName("SenderEmail")]
    public string? SenderEmail { get; set; }

    [JsonPropertyName("ReasonCode")]
    public int? ReasonCode { get; set; }

    [JsonPropertyName("IsmHost")]
    public string? IsmHost { get; set; }

    [JsonPropertyName("IsmPort")]
    public int? IsmPort { get; set; }

    [JsonPropertyName("FnsUrl")]
    public string? FnsUrl { get; set; }

    [JsonPropertyName("OfdVatin")]
    public string? OfdVatin { get; set; }

    [JsonPropertyName("OfdName")]
    public string? OfdName { get; set; }

    [JsonPropertyName("AgentTypes")]
    public string? AgentTypes { get; set; }

    [JsonPropertyName("IsBsoSign")]
    public bool? IsBsoSign { get; set; }

    [JsonPropertyName("IsMarking")]
    public bool? IsMarking { get; set; }

    [JsonPropertyName("IsPawnshop")]
    public bool? IsPawnshop { get; set; }

    [JsonPropertyName("IsAssurance")]
    public bool? IsAssurance { get; set; }

    [JsonPropertyName("IsAutomatic")]
    public bool? IsAutomatic { get; set; }

    [JsonPropertyName("IsVending")]
    public bool? IsVending { get; set; }

    [JsonPropertyName("IsAutomaticPrinter")]
    public bool? IsAutomaticPrinter { get; set; }

    [JsonPropertyName("IsOnline")]
    public bool? IsOnline { get; set; }

    [JsonPropertyName("IsLottery")]
    public bool? IsLottery { get; set; }

    [JsonPropertyName("IsGambling")]
    public bool? IsGambling { get; set; }

    [JsonPropertyName("IsExcisable")]
    public bool? IsExcisable { get; set; }

    [JsonPropertyName("IsService")]
    public bool? IsService { get; set; }

    [JsonPropertyName("IsEncrypted")]
    public bool? IsEncrypted { get; set; }

    [JsonPropertyName("IsOffline")]
    public bool? IsOffline { get; set; }

    [JsonPropertyName("IsCateringServices")]
    public bool? IsCateringServices { get; set; }

    [JsonPropertyName("IsWholesaleTrade")]
    public bool? IsWholesaleTrade { get; set; }

    [JsonPropertyName("SaleAddress")]
    public string? SaleAddress { get; set; }

    [JsonPropertyName("SaleLocation")]
    public string? SaleLocation { get; set; }
}
