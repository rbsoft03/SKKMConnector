namespace SkkmConnector;

/// <summary>
/// Сведения о покупателе (клиенте)
/// </summary>
public sealed class Customer
{
    /// <summary>
    /// Наименование организации или фамилия, имя, отчество (при наличии)
    /// </summary>
    public string? Info { get; set; }

    /// <summary>
    /// ИНН организации или покупателя (клиента)
    /// </summary>
    public string? Vatin { get; set; }

    /// <summary>
    /// Электронная почта покупателя
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Дата рождения покупателя (клиента) в формате "DD.MM.YYYY"
    /// </summary>
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// Числовой код страны, гражданином которой является покупатель (клиент)
    /// </summary>
    public string? Citizenship { get; set; }

    /// <summary>
    /// Числовой код вида документа, удостоверяющего личность (ФФД, таблица 116)
    /// </summary>
    public string? DocumentTypeCode { get; set; }

    /// <summary>
    /// Данные документа, удостоверяющего личность
    /// </summary>
    public string? DocumentData { get; set; }

    /// <summary>
    /// Адрес покупателя (клиента)
    /// </summary>
    public string? Address { get; set; }
}
