using System;

namespace SkkmConnector;

/// <summary>
/// Данные коррекции
/// </summary>
public sealed class CorrectionData
{
    /// <summary>
    /// Тип коррекции: 0 — самостоятельно, 1 — по предписанию
    /// </summary>
    public CorrectionTypes Type { get; set; } = CorrectionTypes.Самостоятельно;

    /// <summary>
    /// Описание коррекции
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Дата совершения корректируемого расчета
    /// </summary>
    public DateTime Date { get; set; } = DateTime.Today;

    /// <summary>
    /// Номер предписания налогового органа
    /// </summary>
    public string Number { get; set; } = "";
}
