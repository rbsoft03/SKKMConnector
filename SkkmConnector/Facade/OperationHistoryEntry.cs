namespace RBSoftSkkm;

/// <summary>
/// Плоское представление одной записи истории операции/задания
/// (<see cref="SkkmConnector.OperationHistory"/>). Обёртка только для чтения поверх
/// <see cref="OperationHistoryItem"/>: вложенный документ разворачивается в плоскую обёртку
/// <see cref="Check"/> (<see cref="Document"/>).
/// </summary>
public sealed class OperationHistoryEntry
{
    private readonly OperationHistoryItem _h;

    public OperationHistoryEntry(OperationHistoryItem source) => _h = source;

    /// <summary> Время записи. </summary>
    public DateTime Time => _h.Time;
    /// <summary> Состояние печати документа. </summary>
    public DocumentPrintState State => _h.State;
    /// <summary> Описание состояния. </summary>
    public string? Description => _h.Description;
    /// <summary> Дополнительная информация. </summary>
    public string? Info => _h.Info;
    /// <summary> Документ записи (плоский), если есть. </summary>
    public Check? Document => _h.Document is null ? null : new Check(_h.Document);
}
