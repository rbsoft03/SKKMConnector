namespace SkkmConnector;

/// <summary>
/// Код причины перерегистрации ККТ:
/// <para>
/// FnReplacement - Замена ФН
/// </para>
/// <para>
/// OfdReplacement - Замена ОФД
/// </para>
/// <para>
/// RequisitesChange - Изменение реквизитов
/// </para>
/// <para>
/// SettingsChange - Изменение настроек ККТ
/// </para>
/// </summary>
public enum FiscalizationReasonCode
{
    /// <summary>
    /// Замена ФН.
    /// </summary>
    FnReplacement = 1,

    /// <summary>
    /// Замена ОФД.
    /// </summary>
    OfdReplacement = 2,

    /// <summary>
    /// Изменение реквизитов.
    /// </summary>
    RequisitesChange = 3,

    /// <summary>
    /// Изменение настроек ККТ.
    /// </summary>
    SettingsChange = 4,
}
