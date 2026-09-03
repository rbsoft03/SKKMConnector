namespace SkkmConnector;

/// <summary>
/// Дробное количество предмета расчёта:
/// <para>
/// Numerator - Числитель
/// </para>
/// <para>
/// Denominator - Знаменатель
/// </para>
/// Используется вместе с мерой количества при частичной реализации маркированного товара.
/// </summary>
public sealed class FractionalQuantity
{
    /// <summary>
    /// Числитель.
    /// </summary>
    public int Numerator { get; set; }

    /// <summary>
    /// Знаменатель.
    /// </summary>
    public int Denominator { get; set; }
}
