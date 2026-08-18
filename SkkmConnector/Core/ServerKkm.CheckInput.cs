namespace SkkmConnector;

// Входные свойства запроса: чек, коррекция, наличные, слип, картинки, маркировка.
public sealed partial class ServerKkm
{

    // Документы / смены

    /// <summary>
    /// Идентификатор документа или задачи.
    /// </summary>
    public string DocumentId { get; set; } = "";

    /// <summary>
    /// Фискальный признак документа (для копии чека).
    /// </summary>
    public string FiscalSign { get; set; } = "";

    /// <summary>
    /// Номер смены (и для запроса чеков за смену).
    /// </summary>
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер чека / последнего фискального документа.
    /// </summary>
    public int CheckNumber { get; set; }

    /// <summary>
    /// Начало периода списка смен.
    /// </summary>
    public DateTime ShiftsFrom { get; set; } = DateTime.Today.AddDays(-7);

    /// <summary>
    /// Конец периода списка смен.
    /// </summary>
    public DateTime ShiftsTo { get; set; } = DateTime.Today;

    // Наличные

    /// <summary>
    /// Сумма внесения или выемки
    /// </summary>
    public decimal CashAmount { get; set; }

    // Картинки

    /// <summary>
    /// Имя картинки на сервере.
    /// </summary>
    public string PictureName { get; set; } = "";

    /// <summary>
    /// Содержимое картинки BMP в Base64.
    /// </summary>
    public string PictureBase64 { get; set; } = "";

    /// <summary>
    /// Выравнивание картинки: 1 — слева, 2 — центр, 3 — справа.
    /// </summary>
    public int PictureAlignment { get; set; } = 2;

    // Слип

    /// <summary>
    /// Текст нефискального документа (слипа) с разметкой.
    /// </summary>
    public string TextForPrint { get; set; } = "";

    // Чек

    /// <summary>
    /// Тип чека (<see cref="CheckType"/>). По умолчанию продажа. Коррекция — метод <see cref="PrintCheckCorrection120"/>.
    /// </summary>
    public int PaymentType { get; set; } = (int)CheckType.Sale;

    /// <summary>
    /// Система налогообложения (<see cref="TaxSystem"/>). По умолчанию ОСН.
    /// </summary>
    public int TaxVariant { get; set; } = (int)TaxSystem.ОСН;

    /// <summary>
    /// Электронный чек без бумаги. Нужен контакт покупателя: <see cref="CustomerEmail"/> или <see cref="CustomerPhone"/>.
    /// </summary>
    public bool Electronically { get; set; }

    /// <summary>
    /// Текст в шапке чека.
    /// </summary>
    public string TextBefore { get; set; } = "";

    /// <summary>
    /// Текст в подвале чека.
    /// </summary>
    public string TextAfter { get; set; } = "";

    /// <summary>
    /// Место проведения расчётов.
    /// </summary>
    public string SaleLocation { get; set; } = "";

    /// <summary>
    /// Наименование покупателя.
    /// </summary>
    public string CustomerInfo { get; set; } = "";

    /// <summary>
    /// ИНН покупателя.
    /// </summary>
    public string CustomerVatin { get; set; } = "";

    /// <summary>
    /// Email покупателя.
    /// </summary>
    public string CustomerEmail { get; set; } = "";

    /// <summary>
    /// Телефон покупателя.
    /// </summary>
    public string CustomerPhone { get; set; } = "";

    /// <summary>
    /// Сумма наличной оплаты. Итог всех оплат должен быть не меньше суммы позиций, иначе чек не закроется.
    /// </summary>
    public decimal PayCash { get; set; }

    /// <summary>
    /// Сумма безналичной оплаты.
    /// </summary>
    public decimal PayElectronic { get; set; }

    /// <summary>
    /// Сумма постоплатой (кредит).
    /// </summary>
    public decimal PayCredit { get; set; }

    /// <summary>
    /// Сумма предоплатой (аванс).
    /// </summary>
    public decimal PayAdvance { get; set; }

    /// <summary>
    /// Сумма встречным предоставлением.
    /// </summary>
    public decimal PayProvision { get; set; }

    /// <summary>
    /// Строки чека: фискальные и нефискальные. Нужна хотя бы одна фискальная — обычно через <see cref="AddFiscalLine"/>.
    /// </summary>
    public List<CheckLine> Positions { get; } = new();

    // Коррекция
    /// <summary>
    /// Признак чека коррекции.
    /// </summary>
    public bool IsCorrection { get; set; }

    /// <summary>
    /// Тип коррекции.
    /// </summary>
    public CorrectionTypes CorrectionType { get; set; } = CorrectionTypes.Самостоятельно;

    /// <summary>
    /// Описание коррекции.
    /// </summary>
    public string CorrectionDescription { get; set; } = "";

    /// <summary>
    /// Дата корректируемого расчёта.
    /// </summary>
    public DateTime CorrectionDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Номер предписания / документа коррекции.
    /// </summary>
    public string CorrectionNumber { get; set; } = "";

    /// <summary>
    /// Номер корректируемого чека (доп. реквизит).
    /// </summary>
    public string CorrectedCheckNumber { get; set; } = "";

    // Маркировка (вход)

    /// <summary>
    /// Код маркировки (Base64), для <see cref="RequestKM"/>.
    /// </summary>
    public string MarkingCode { get; set; } = "";

    /// <summary>
    /// Планируемый статус товара (тег 2003 ФФД).
    /// </summary>
    public int PlannedStatus { get; set; } = 1;

    /// <summary>
    /// Количество предмета расчёта для запроса КМ.
    /// </summary>
    public decimal MarkingQuantity { get; set; } = 1;

    /// <summary>
    /// Мера количества предмета расчёта (таблица 114 ФФД) для запроса КМ.
    /// </summary>
    public int MeasureOfQuantity { get; set; }

    /// <summary>
    /// Числитель дробного количества маркированного товара.
    /// </summary>
    public int FractionalQuantityNumerator { get; set; }

    /// <summary>
    /// Знаменатель дробного количества маркированного товара.
    /// </summary>
    public int FractionalQuantityDenominator { get; set; }

    /// <summary>
    /// Не отправлять запрос КМ на сервер ОИСМ.
    /// </summary>
    public bool NotSendToServer { get; set; }

    /// <summary>
    /// Ждать ответ ОИСМ в <see cref="RequestKM"/>.
    /// </summary>
    public bool WaitForResult { get; set; }

    /// <summary>
    /// Guid запроса КМ. Если пуст, <see cref="RequestKM"/> генерирует новый.
    /// </summary>
    public string RequestKmGuid { get; set; } = "";

    /// <summary>
    /// Подтверждение КМ: 0 — включён в документ, 1 — не включён.
    /// </summary>
    public int ConfirmationType { get; set; }
}
