using System;

namespace RBSoftSkkm;

/// <summary>
/// Данные коррекции:
/// <para>
/// Type - Тип коррекции.
/// </para>
/// <para>
/// Description - Описание коррекции
/// </para>
/// <para>
/// Date - Дата совершения корректируемого расчёта
/// </para>
/// <para>
/// Number - Номер предписания налогового органа (для Type = ПоПредписанию)
/// </para>
/// </summary>
public sealed class CorrectionData
{
    /// <summary>
    /// Тип коррекции.
    /// </summary>
    public CorrectionTypes Type { get; set; } = CorrectionTypes.Самостоятельно;

    /// <summary>
    /// Описание коррекции.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Дата совершения корректируемого расчёта.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.Today;

    /// <summary>
    /// Номер предписания налогового органа.
    /// </summary>
    public string Number { get; set; } = "";
}
