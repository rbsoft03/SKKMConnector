namespace SkkmConnector;

/// <summary>
/// Элемент списка шаблонов чека.
/// </summary>
public sealed class CheckTemplateListItem
{
    /// <summary>
    /// Имя шаблона чека.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Тип чека шаблона
    /// </summary>
    public CheckType TaskType { get; set; }
}
