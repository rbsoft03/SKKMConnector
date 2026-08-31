namespace SkkmConnector;

/// <summary>
/// Шаблон чека, полученный с сервера.
/// </summary>
public sealed class CheckTemplate
{
    /// <summary>
    /// Идентификатор шаблона на сервере.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Имя шаблона чека.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Документ шаблона.
    /// </summary>
    public CheckTemplateDocument? Document { get; set; }
}
