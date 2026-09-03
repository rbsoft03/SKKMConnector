namespace SkkmConnector;

/// <summary>
/// Тип строки печатного шаблона / печатной формы:
/// <para>
/// Fiscal - Фискальная
/// </para>
/// <para>
/// Text - Текстовая
/// </para>
/// <para>
/// Barcode - Штрихкод
/// </para>
/// <para>
/// Picture - Изображение
/// </para>
/// <para>
/// Separator - Разделительная линия
/// </para>
/// </summary>
public enum PrintLineType
{
    /// <summary>
    /// Фискальная строка.
    /// </summary>
    Fiscal = 0,

    /// <summary>
    /// Текстовая строка.
    /// </summary>
    Text = 1,

    /// <summary>
    /// Штрихкод.
    /// </summary>
    Barcode = 2,

    /// <summary>
    /// Изображение.
    /// </summary>
    Picture = 3,

    /// <summary>
    /// Разделительная линия.
    /// </summary>
    Separator = 4,
}
