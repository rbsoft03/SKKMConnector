namespace SkkmConnector;

/// <summary>
/// Дробное количество предмета расчета
/// </summary>
public sealed class FractionalQuantity
{
    /// <summary>
    /// Числитель
    /// </summary>
    public int Numerator { get; set; }

    /// <summary>
    /// Знаменатель
    /// </summary>
    public int Denominator { get; set; }
}
