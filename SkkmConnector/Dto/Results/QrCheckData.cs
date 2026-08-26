using System.Text.Json.Serialization;

namespace SkkmConnector;

/// <summary>
/// Данные для отображения QR-кода чека (QrCheckData).
/// </summary>
public sealed class QrCheckData
{
    /// <summary>
    /// Дата создания документа.
    /// </summary>
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Сумма чека.
    /// </summary>
    [JsonPropertyName("Amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Фискальный накопитель.
    /// </summary>
    [JsonPropertyName("Fn")]
    public string? Fn { get; set; }

    /// <summary>
    /// Фискальный документ.
    /// </summary>
    [JsonPropertyName("Fd")]
    public int Fd { get; set; }

    /// <summary>
    /// Фискальный признак.
    /// </summary>
    [JsonPropertyName("Fp")]
    public string? Fp { get; set; }

    /// <summary>
    /// Тип операции: 1 — приход; 2 — возврат прихода; 4 — расход; 5 — возврат расхода; 7 — коррекция прихода; 9 — коррекция расхода.
    /// </summary>
    [JsonPropertyName("N")]
    public int N { get; set; }
}
