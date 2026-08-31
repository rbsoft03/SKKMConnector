namespace SkkmConnector;

/// <summary>
/// Шаблон печати, полученный с сервера.
/// </summary>
public sealed class PrintTemplate
{
    /// <summary>
    /// Имя шаблона. Уникальный идентификатор на сервере.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Тип шаблона: 0 — реклама; 1 — строки чека; 2 — шапка или подвал чека.
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Строки шаблона (текст, штрихкод, картинка, разделитель).
    /// </summary>
    public List<TemplateItem> TemplateItems { get; set; } = [];
}
