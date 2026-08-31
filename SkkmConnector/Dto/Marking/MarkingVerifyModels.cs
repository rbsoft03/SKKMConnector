namespace SkkmConnector;

/// <summary>
/// Результат проверки кода маркировки.
/// </summary>
public sealed class MarkingVerifyResult
{
    public int Code { get; set; }
    public string Description { get; set; } = "";
    public List<CodeMarkInfo> Codes { get; set; } = [];
    public string ReqId { get; set; } = "";
    public long ReqTimestamp { get; set; }
    public bool IsCheckedOffline { get; set; }
}

/// <summary>
/// Сведения о коде маркировки.
/// </summary>
public sealed class CodeMarkInfo
{
    public string Cis { get; set; } = "";
    public bool Valid { get; set; }
    public string PrintView { get; set; } = "";
    public int[] GroupIds { get; set; } = [];
    public bool Verified { get; set; }
    public bool Realizable { get; set; }
    public bool Utilised { get; set; }
    public bool Found { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; } = "";
    public bool IsTracking { get; set; }
    public bool Sold { get; set; }
    public string Gtin { get; set; } = "";
    public string PackageType { get; set; } = "";
    public string ProducerInn { get; set; } = "";
    public bool GrayZone { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsGreyGtin { get; set; }
    public string[] Ogvs { get; set; } = [];
    public int PackageQuantity { get; set; }
}
