namespace RBSoftSkkm;

/// <summary>
/// Способ проверки кода маркировки.
/// </summary>
public enum KmVerificationMethod
{
    /// <summary>
    /// Не проверялся.
    /// </summary>
    None = 0,

    /// <summary>
    /// ТС ПИоТ.
    /// </summary>
    TsPiot = 1,

    /// <summary>
    /// ГИС МТ.
    /// </summary>
    GisMt = 2,

    /// <summary>
    /// ЛМЧЗ.
    /// </summary>
    Lmcz = 3,

    /// <summary>
    /// ТС ПИоТ + ЛМЧЗ.
    /// </summary>
    TsPiotLmcz = 4
}
