namespace RBSoftSkkm;

/// <summary>
/// Общие поля строки печати (печатная форма и строка печатного шаблона).
/// </summary>
public interface IPrintLine
{
    PrintLineType Type { get; }
    string? Line { get; }
    string? LineRight { get; }
    PrintAlignment Alignment { get; }
    PrintFont Font { get; }
    bool IsFontSpecified { get; }
    int Width { get; }
    int Scale { get; }
    bool Wrap { get; }
    bool IsCreateFromTemplate { get; }
    SeparatorLine? SeparatorLine { get; }
    Picture? Picture { get; }
    PrintFormBarcode? Barcode { get; }
    string[]? BarcodeLines { get; }
}
