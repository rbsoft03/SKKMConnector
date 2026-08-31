namespace SkkmConnector;

/// <summary>
/// Параметры создания или изменения шаблона печати.
/// </summary>
public sealed class TemplateParameters
{
    /// <summary>
    /// Имя шаблона. Уникальный идентификатор на сервере.
    /// Разрешены символы a-z, A-Z, 0-9, _, -, (, ). Пробелы запрещены.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Тип шаблона: 0 — реклама; 1 — строки чека; 2 — шапка или подвал чека.
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Строки шаблона (текст, штрихкод, картинка, разделительная линия).
    /// </summary>
    public List<TemplateItem> TemplateItems { get; set; } = [];
}
