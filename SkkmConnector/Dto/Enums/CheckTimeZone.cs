namespace SkkmConnector;

/// <summary>
/// Часовая зона места расчёта (тег 1011 ФФД):
/// <para>
/// Auto - Авто (из настроек ККТ)
/// </para>
/// <para>
/// MskMinus1 - 1-я часовая зона (МСК−1, UTC+2)
/// </para>
/// <para>
/// Msk - 2-я часовая зона (МСК, UTC+3)
/// </para>
/// <para>
/// MskPlus1 - 3-я часовая зона (МСК+1, UTC+4)
/// </para>
/// <para>
/// MskPlus2 - 4-я часовая зона (МСК+2, UTC+5)
/// </para>
/// <para>
/// MskPlus3 - 5-я часовая зона (МСК+3, UTC+6)
/// </para>
/// <para>
/// MskPlus4 - 6-я часовая зона (МСК+4, UTC+7)
/// </para>
/// <para>
/// MskPlus5 - 7-я часовая зона (МСК+5, UTC+8).
/// </para>
/// <para>
/// MskPlus6 - 8-я часовая зона (МСК+6, UTC+9)
/// </para>
/// <para>
/// MskPlus7 -  9-я часовая зона (МСК+7, UTC+10)
/// </para>
/// <para>
/// MskPlus8 - 10-я часовая зона (МСК+8, UTC+11)
/// </para>
/// <para>
/// MskPlus9 - 11-я часовая зона (МСК+9, UTC+12)
/// </para>
/// </summary>
public enum CheckTimeZone
{
    /// <summary>
    /// Авто (из настроек ККТ).
    /// </summary>
    Auto = 0,

    /// <summary>
    /// 1-я часовая зона (МСК−1, UTC+2).
    /// </summary>
    MskMinus1 = 1,

    /// <summary>
    /// 2-я часовая зона (МСК, UTC+3).
    /// </summary>
    Msk = 2,

    /// <summary>
    /// 3-я часовая зона (МСК+1, UTC+4).
    /// </summary>
    MskPlus1 = 3,

    /// <summary>
    /// 4-я часовая зона (МСК+2, UTC+5).
    /// </summary>
    MskPlus2 = 4,

    /// <summary>
    /// 5-я часовая зона (МСК+3, UTC+6).
    /// </summary>
    MskPlus3 = 5,

    /// <summary>
    /// 6-я часовая зона (МСК+4, UTC+7).
    /// </summary>
    MskPlus4 = 6,

    /// <summary>
    /// 7-я часовая зона (МСК+5, UTC+8).
    /// </summary>
    MskPlus5 = 7,

    /// <summary>
    /// 8-я часовая зона (МСК+6, UTC+9).
    /// </summary>
    MskPlus6 = 8,

    /// <summary>
    /// 9-я часовая зона (МСК+7, UTC+10).
    /// </summary>
    MskPlus7 = 9,

    /// <summary>
    /// 10-я часовая зона (МСК+8, UTC+11).
    /// </summary>
    MskPlus8 = 10,

    /// <summary>
    /// 11-я часовая зона (МСК+9, UTC+12).
    /// </summary>
    MskPlus9 = 11,
}
