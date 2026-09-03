namespace SkkmConnector;

/// <summary>
/// Тип печатного шаблона:
/// <para>
/// Advertisement - Реклама
/// </para>
/// <para>
/// CheckLines - Строки чека
/// </para>
/// <para>
/// HeaderOrFooter - Шапка или подвал чека
/// </para>
/// </summary>
public enum PrintTemplateType
{
    /// <summary>
    /// Реклама.
    /// </summary>
    Advertisement = 0,

    /// <summary>
    /// Строки чека.
    /// </summary>
    CheckLines = 1,

    /// <summary>
    /// Шапка или подвал чека.
    /// </summary>
    HeaderOrFooter = 2,
}
