namespace SkkmConnector;

/// <summary>
/// Данные поставщика:
/// <para>
/// Name - Наименование поставщика
/// </para>
/// <para>
/// Phones - Телефон(ы) поставщика
/// </para>
/// <para>
/// Vatin - ИНН поставщика
/// </para>
/// </summary>
public sealed class Vendor
{
    /// <summary>
    /// Наименование поставщика.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Телефоны поставщика.
    /// </summary>
    public string[]? Phones { get; set; }

    /// <summary>
    /// ИНН поставщика.
    /// </summary>
    public string? Vatin { get; set; }
}
