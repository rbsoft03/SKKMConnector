using System.Text.Json.Serialization;

namespace SkkmConnector.Internal;

internal sealed class DeviceSettingsRequest
{
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    [JsonPropertyName("Settings")]
    public DeviceSettings? Settings { get; set; }
}

internal sealed class ServiceSettingsRequest
{
    [JsonPropertyName("ServiceSettings")]
    public ServiceSettings? ServiceSettings { get; set; }
}

internal sealed class UserProfileRequest
{
    [JsonPropertyName("User")]
    public ServiceUser? User { get; set; }
}

internal sealed class DeviceFontSettingsRequest
{
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    [JsonPropertyName("TemplateSettingH1")]
    public string? TemplateSettingH1 { get; set; }

    [JsonPropertyName("TemplateSettingH2")]
    public string? TemplateSettingH2 { get; set; }

    [JsonPropertyName("TemplateSettingH3")]
    public string? TemplateSettingH3 { get; set; }

    [JsonPropertyName("TemplateSettingH4")]
    public string? TemplateSettingH4 { get; set; }

    [JsonPropertyName("TemplateSettingH5")]
    public string? TemplateSettingH5 { get; set; }
}

internal sealed class CheckTemplateRequest
{
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("Document")]
    public CheckTemplateDocumentRequest? Document { get; set; }
}

internal sealed class CheckTemplateDocumentRequest
{
    [JsonPropertyName("PaymentType")]
    public int PaymentType { get; set; }

    [JsonPropertyName("TaxVariant")]
    public int TaxVariant { get; set; }

    [JsonPropertyName("Customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("SenderEmail")]
    public string? SenderEmail { get; set; }

    [JsonPropertyName("SaleAddress")]
    public string? SaleAddress { get; set; }

    [JsonPropertyName("SaleLocation")]
    public string? SaleLocation { get; set; }

    [JsonPropertyName("Positions")]
    public ApiPosition[]? Positions { get; set; }

    [JsonPropertyName("Payments")]
    public Payments? Payments { get; set; }

    [JsonPropertyName("ElectronicPaymentInfo")]
    public List<ElectronicPayment>? ElectronicPaymentInfo { get; set; }

    [JsonPropertyName("Electronically")]
    public bool Electronically { get; set; }

    [JsonPropertyName("OperationalAttribute")]
    public OperationalAttribute? OperationalAttribute { get; set; }

    [JsonPropertyName("IndustryAttribute")]
    public Industry? IndustryAttribute { get; set; }

    [JsonPropertyName("UserAttribute")]
    public UserAttribute? UserAttribute { get; set; }

    [JsonPropertyName("TimeZone")]
    public int? TimeZone { get; set; }

    [JsonPropertyName("OperationOnline")]
    public bool OperationOnline { get; set; }

    [JsonPropertyName("AdditionalAttribute")]
    public string? AdditionalAttribute { get; set; }

    [JsonPropertyName("CorrectionData")]
    public CorrectionData? CorrectionData { get; set; }
}

internal sealed class CheckCopyFnParameters
{
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    [JsonPropertyName("FnNumber")]
    public string? FnNumber { get; set; }

    [JsonPropertyName("FiscalSign")]
    public string? FiscalSign { get; set; }

    [JsonPropertyName("DocNumber")]
    public int DocNumber { get; set; }
}

internal sealed class MarkingCodesRequest
{
    [JsonPropertyName("DeviceName")]
    public string? DeviceName { get; set; }

    [JsonPropertyName("Codes")]
    public List<string> Codes { get; set; } = [];
}
