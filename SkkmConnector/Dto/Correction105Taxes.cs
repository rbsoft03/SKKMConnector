namespace SkkmConnector;

/// <summary>
/// Суммы НДС по чеку коррекции ФФД 1.05
/// </summary>
public sealed class Correction105Taxes
{
    /// <summary>
    /// Сумма расчёта по ставке НДС 0%
    /// </summary>
    public decimal? SumTax0 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 5%
    /// </summary>
    public decimal? SumTax5 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 7%
    /// </summary>
    public decimal? SumTax7 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 10%
    /// </summary>
    public decimal? SumTax10 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 18%
    /// </summary>
    public decimal? SumTax18 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 20%
    /// </summary>
    public decimal? SumTax20 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 22%
    /// </summary>
    public decimal? SumTax22 { get; set; }

    /// <summary>
    /// Сумма расчёта без НДС
    /// </summary>
    public decimal? SumTaxNone { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 5/105
    /// </summary>
    public decimal? SumTax105 { get; set; }

    /// <summary>
    /// Сумма НДС чека по ставке 7/107
    /// </summary>
    public decimal? SumTax107 { get; set; }

    /// <summary>
    /// Сумма НДС чека по расч. ставке 10/110
    /// </summary>
    public decimal? SumTax110 { get; set; }

    /// <summary>
    /// Сумма НДС чека по расч. ставке 18/118
    /// </summary>
    public decimal? SumTax118 { get; set; }

    /// <summary>
    /// Сумма НДС чека по расч. ставке 20/120
    /// </summary>
    public decimal? SumTax120 { get; set; }

    /// <summary>
    /// Сумма НДС чека по расч. ставке 22/122
    /// </summary>
    public decimal? SumTax122 { get; set; }
}
