namespace RBSoftSkkm;

/// <summary>
/// Инициатор проверки кода маркировки.
/// </summary>
public enum KmCheckInitiator
{
    /// <summary>
    /// По запросу (RequestKM).
    /// </summary>
    Request = 0,

    /// <summary>
    /// Из интерфейса.
    /// </summary>
    Interface = 1,

    /// <summary>
    /// В составе чека.
    /// </summary>
    InReceipt = 2
}
