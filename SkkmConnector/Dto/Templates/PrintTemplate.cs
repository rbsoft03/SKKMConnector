using System.Text.Json.Serialization;

namespace RBSoftSkkm;

/// <summary>
/// Шаблон печати, сохранённый на сервере.
/// </summary>
public sealed class PrintTemplate
{
    /// <summary>
    /// Имя шаблона. Уникальный идентификатор на сервере.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Тип шаблона.
    /// </summary>
    public PrintTemplateType Type { get; set; }

    /// <summary>
    /// Строки шаблона в том виде, в каком их хранит сервер (каждая обёрнута в <see cref="TemplateItem"/>).
    /// Для чтения удобнее <see cref="Lines"/>.
    /// </summary>
    public List<TemplateItem> TemplateItems { get; set; } = [];

    /// <summary>
    /// Строки шаблона без обёртки: текст, штрихкоды, картинки, разделительные линии.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<PrintLine> Lines
        => TemplateItems
            .Where(item => item.PrintLine != null)
            .Select(item => item.PrintLine!)
            .ToArray();

    /// <summary>
    /// Количество строк в шаблоне.
    /// </summary>
    [JsonIgnore]
    public int LineCount => Lines.Count;
}
