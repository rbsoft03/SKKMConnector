namespace SkkmConnector;

/// <summary>
/// Базовый тип позиции чека. В <c>kkm.Positions</c> добавляйте конкретные типы:
/// <para>
/// FiscalLine - Фискальная (товар/услуга)
/// </para>
/// <para>
/// TextLine - Текстовая
/// </para>
/// <para>
/// BarcodeLine - Штрихкод
/// </para>
/// <para>
/// PictureLine - Изображение
/// </para>
/// <para>
/// SeparatorLine - Разделительная линия
/// </para>
/// </summary>
public abstract class Position
{
}
