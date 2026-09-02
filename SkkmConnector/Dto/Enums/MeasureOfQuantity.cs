namespace SkkmConnector;

/// <summary>
/// Мера количества предмета расчёта (таблица 114 ФФД):
/// <para>
/// Piece - Штука или единица (поштучная реализация)
/// </para>
/// <para>
/// Gram - Грамм
/// </para>
/// <para>
/// Kilogram - Килограмм
/// </para>
/// <para>
/// Tonne - Тонна
/// </para>
/// <para>
/// Centimeter - Сантиметр
/// </para>
/// <para>
/// Decimeter - Дециметр
/// </para>
/// <para>
/// Meter - Метр
/// </para>
/// <para>
/// SquareCentimeter - Квадратный сантиметр
/// </para>
/// <para>
/// SquareDecimeter - Квадратный дециметр
/// </para>
/// <para>
/// SquareMeter - Квадратный метр
/// </para>
/// <para>
/// Milliliter - Миллилитр
/// </para>
/// <para>
/// Liter - Литр
/// </para>
/// <para>
/// CubicMeter - Кубический метр
/// </para>
/// <para>
/// KilowattHour - Киловатт-час
/// </para>
/// <para>
/// Gigacalorie - Гигакалория
/// </para>
/// <para>
/// Day - Сутки
/// </para>
/// <para>
/// Hour - Час
/// </para>
/// <para>
/// Minute - Минута
/// </para>
/// <para>
/// Second - Секунда
/// </para>
/// <para>
/// Kilobyte - Килобайт
/// </para>
/// <para>
/// Megabyte - Мегабайт
/// </para>
/// <para>
/// Gigabyte - Гигабайт
/// </para>
/// <para>
/// Terabyte - Терабайт
/// </para>
/// <para>
/// Other - Иная единица измерения
/// </para>
/// </summary>
public enum MeasureOfQuantity
{
    /// <summary>
    /// Штука или единица (поштучная реализация).
    /// </summary>
    Piece = 0,

    /// <summary>
    /// Грамм.
    /// </summary>
    Gram = 10,

    /// <summary>
    /// Килограмм.
    /// </summary>
    Kilogram = 11,

    /// <summary>
    /// Тонна.
    /// </summary>
    Tonne = 12,

    /// <summary>
    /// Сантиметр.
    /// </summary>
    Centimeter = 20,

    /// <summary>
    /// Дециметр.
    /// </summary>
    Decimeter = 21,

    /// <summary>
    /// Метр.
    /// </summary>
    Meter = 22,

    /// <summary>
    /// Квадратный сантиметр.
    /// </summary>
    SquareCentimeter = 30,

    /// <summary>
    /// Квадратный дециметр.
    /// </summary>
    SquareDecimeter = 31,

    /// <summary>
    /// Квадратный метр.
    /// </summary>
    SquareMeter = 32,

    /// <summary>
    /// Миллилитр.
    /// </summary>
    Milliliter = 40,

    /// <summary>
    /// Литр.
    /// </summary>
    Liter = 41,

    /// <summary>
    /// Кубический метр.
    /// </summary>
    CubicMeter = 42,

    /// <summary>
    /// Киловатт-час.
    /// </summary>
    KilowattHour = 50,

    /// <summary>
    /// Гигакалория.
    /// </summary>
    Gigacalorie = 51,

    /// <summary>
    /// Сутки.
    /// </summary>
    Day = 70,

    /// <summary>
    /// Час.
    /// </summary>
    Hour = 71,

    /// <summary>
    /// Минута.
    /// </summary>
    Minute = 72,

    /// <summary>
    /// Секунда.
    /// </summary>
    Second = 73,

    /// <summary>
    /// Килобайт.
    /// </summary>
    Kilobyte = 80,

    /// <summary>
    /// Мегабайт.
    /// </summary>
    Megabyte = 81,

    /// <summary>
    /// Гигабайт.
    /// </summary>
    Gigabyte = 82,

    /// <summary>
    /// Терабайт.
    /// </summary>
    Terabyte = 83,

    /// <summary>
    /// Иная единица измерения.
    /// </summary>
    Other = 255,
}
