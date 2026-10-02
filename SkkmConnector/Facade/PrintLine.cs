namespace RBSoftSkkm;

/// <summary>
/// Плоское представление одной строки печати — печатной формы (<see cref="SkkmConnector.PrintForm"/>)
/// или печатного шаблона (<see cref="SkkmConnector.TemplateLines"/>). Обёртка только для чтения
/// поверх <see cref="IPrintLine"/>: разворачивает под-объекты (SeparatorLine/Barcode/Picture) в плоские свойства.
/// </summary>
public sealed class PrintLine
{
    private readonly IPrintLine _l;

    public PrintLine(IPrintLine source) => _l = source;

    // ─── Прямые поля строки ───
    /// <summary> Тип строки. </summary>
    public PrintLineType Type => _l.Type;
    /// <summary> Текст строки (левая часть). </summary>
    public string? Line => _l.Line;
    /// <summary> Текст строки (правая часть). </summary>
    public string? LineRight => _l.LineRight;
    /// <summary> Выравнивание. </summary>
    public PrintAlignment Alignment => _l.Alignment;
    /// <summary> Шрифт. </summary>
    public PrintFont Font => _l.Font;
    /// <summary> Шрифт задан явно. </summary>
    public bool IsFontSpecified => _l.IsFontSpecified;
    /// <summary> Ширина. </summary>
    public int Width => _l.Width;
    /// <summary> Масштаб, %. </summary>
    public int Scale => _l.Scale;
    /// <summary> Перенос строки. </summary>
    public bool Wrap => _l.Wrap;
    /// <summary> Строка создана из печатного шаблона. </summary>
    public bool IsCreateFromTemplate => _l.IsCreateFromTemplate;

    // ─── SeparatorLine ───
    /// <summary> Стиль разделительной линии (если строка — разделитель). </summary>
    public LineStyle? SeparatorLineStyle => _l.SeparatorLine?.LineStyle;

    // ─── Barcode ───
    /// <summary> Тип штрихкода (если строка — штрихкод). </summary>
    public BarcodeType? BarcodeType => _l.Barcode?.Type;
    /// <summary> Значение штрихкода. </summary>
    public string? BarcodeValue => _l.Barcode?.Value;
    /// <summary> Изображение штрихкода в Base64. </summary>
    public string? BarcodePictureBase64 => _l.Barcode?.PictureBase64;
    /// <summary> Способ печати текста штрихкода. </summary>
    public BarcodePrintText? BarcodePrintText => _l.Barcode?.PrintText;
    /// <summary> Высота штрихкода, точек. </summary>
    public int BarcodeHeight => _l.Barcode?.Height ?? 0;
    /// <summary> Ширина штриха, точек. </summary>
    public int BarcodeBarWidth => _l.Barcode?.BarWidth ?? 0;
    /// <summary> Строки, выводимые рядом со штрихкодом. </summary>
    public IReadOnlyList<string> BarcodeLines => _l.BarcodeLines ?? [];

    // ─── Picture ───
    /// <summary> Название изображения (если строка — картинка). </summary>
    public string? PictureName => _l.Picture?.PictureName;
    /// <summary> Выравнивание изображения. </summary>
    public PictureAlignment? PictureAlignment => _l.Picture?.Alignment;
    /// <summary> Изображение в Base64. </summary>
    public string? PictureBase64 => _l.Picture?.PictureBase64;
    /// <summary> Ширина изображения, точек. </summary>
    public int PictureWidth => _l.Picture?.Width ?? 0;
    /// <summary> Высота изображения, точек. </summary>
    public int PictureHeight => _l.Picture?.Height ?? 0;
    /// <summary> Номер строки начала изображения. </summary>
    public int? PictureStartLineNumber => _l.Picture?.StartLineNumber;
    /// <summary> Номер строки конца изображения. </summary>
    public int? PictureEndLineNumber => _l.Picture?.EndLineNumber;
    /// <summary> Изображение уже загружено на устройство. </summary>
    public bool? PictureIsUploaded => _l.Picture?.IsUploaded;
    /// <summary> Перезаписать ранее загруженное изображение. </summary>
    public bool? PictureOverride => _l.Picture?.Override;
}
