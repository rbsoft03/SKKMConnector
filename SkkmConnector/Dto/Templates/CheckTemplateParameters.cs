namespace SkkmConnector;

/// <summary>
/// Параметры создания или изменения шаблона чека:
/// <para>
/// Name - Уникальное имя шаблона на сервере
/// </para>
/// <para>
/// Document - Документ шаблона (<see cref="CheckTemplateDocument"/>)
/// </para>
/// </summary>
public sealed class CheckTemplateParameters
{
    /// <summary>
    /// Имя шаблона чека. Уникальный идентификатор на сервере.
    /// Разрешены символы a-z, A-Z, 0-9, _, -, (, ). Пробелы запрещены.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Документ шаблона
    /// </summary>
    public CheckTemplateDocument? Document { get; set; }
}
