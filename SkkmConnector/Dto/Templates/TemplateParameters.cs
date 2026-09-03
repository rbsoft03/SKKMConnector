namespace SkkmConnector;

/// <summary>
/// Параметры создания или изменения шаблона печати:
/// <para>
/// Name - Уникальное имя шаблона на сервере
/// </para>
/// <para>
/// Type - Тип шаблона. Используйте enum <see cref="PrintTemplateType"/>
/// </para>
/// <para>
/// TemplateItems - Строки шаблона (<see cref="TemplateItem"/> / <see cref="PrintLine"/>)
/// </para>
/// </summary>
public sealed class TemplateParameters
{
    /// <summary>
    /// Имя шаблона. Уникальный идентификатор на сервере.
    /// Разрешены символы a-z, A-Z, 0-9, _, -, (, ). Пробелы запрещены.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Тип шаблона. Используйте enum <see cref="PrintTemplateType"/>.
    /// </summary>
    public PrintTemplateType Type { get; set; }

    /// <summary>
    /// Строки шаблона (текст, штрихкод, картинка, разделительная линия).
    /// </summary>
    public List<TemplateItem> TemplateItems { get; set; } = [];
}
