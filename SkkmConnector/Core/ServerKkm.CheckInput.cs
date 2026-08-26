namespace SkkmConnector;

// Входные свойства запроса: чек, коррекция, наличные, слип, картинки, маркировка.
public sealed partial class ServerKkm
{

    // Документы / смены

    /// <summary>
    /// Идентификатор документа (docId)
    /// </summary>
    public string DocumentId { get; set; } = "";

    /// <summary>
    /// Фискальный признак документа
    /// </summary>
    public string FiscalSign { get; set; } = "";

    /// <summary>
    /// Номер смены
    /// </summary>
    public int ShiftNumber { get; set; }

    /// <summary>
    /// Номер фискального документа
    /// </summary>
    public int CheckNumber { get; set; }

    /// <summary>
    /// Начало даты отбора списка отчётов (формат гггг-мм-дд)
    /// </summary>
    public DateTime ShiftsFrom { get; set; } = DateTime.Today.AddDays(-7);

    /// <summary>
    /// Конец даты отбора списка отчётов (формат гггг-мм-дд)
    /// </summary>
    public DateTime ShiftsTo { get; set; } = DateTime.Today;

    // Наличные

    /// <summary>
    /// Сумма внесения или выемки
    /// </summary>
    public decimal CashAmount { get; set; }

    // Картинки

    /// <summary>
    /// Название изображения
    /// </summary>
    public string PictureName { get; set; } = "";

    /// <summary>
    /// Изображение, закодированное в Base64.
    /// </summary>
    public string PictureBase64 { get; set; } = "";

    /// <summary>
    /// Выравнивание изображения при печати
    /// </summary>
    public int PictureAlignment { get; set; } = 2;

    // Слип

    /// <summary>
    /// Текст нефискального документа.
    /// </summary>
    public string TextForPrint { get; set; } = "";

    // Чек

    /// <summary>
    /// Тип чека
    /// </summary>
    public int PaymentType { get; set; } = (int)CheckType.Sale;

    /// <summary>
    /// Система налогообложения (СНО)
    /// </summary>
    public int TaxVariant { get; set; } = (int)TaxSystem.ОСН;

    /// <summary>
    /// Часовая зона
    /// </summary>
    public int? TimeZone { get; set; }

    /// <summary>
    /// Формирование чека только в электронном виде
    /// </summary>
    public bool Electronically { get; set; }

    /// <summary>
    /// Текст для печати перед товарной частью
    /// </summary>
    public string TextBefore { get; set; } = "";

    /// <summary>
    /// Текст для печати после товарной части чека
    /// </summary>
    public string TextAfter { get; set; } = "";

    /// <summary>
    /// Место проведения расчётов
    /// </summary>
    public string SaleLocation { get; set; } = "";

    /// <summary>
    /// Адрес проведения расчётов
    /// </summary>
    public string SaleAddress { get; set; } = "";

    /// <summary>
    /// Адрес электронной почты отправителя чека
    /// </summary>
    public string SenderEmail { get; set; } = "";

    /// <summary>
    /// Признак применения ККТ при осуществлении расчета в безналичном порядке в сети «Интернет»
    /// </summary>
    public bool OperationOnline { get; set; }

    /// <summary>
    /// Дополнительный реквизит чека (БСО), тег 1192
    /// </summary>
    public string AdditionalAttribute { get; set; } = "";

    /// <summary>
    /// Отраслевой реквизит чека
    /// </summary>
    public Industry? IndustryAttribute { get; set; }

    /// <summary>
    /// Дополнительный реквизит пользователя
    /// </summary>
    public UserAttribute? UserAttribute { get; set; }

    /// <summary>
    /// Операционный реквизит чека
    /// </summary>
    public OperationalAttribute? OperationalAttribute { get; set; }

    /// <summary>
    /// Сведения об оплате безналичными
    /// </summary>
    public List<ElectronicPayment> ElectronicPayments { get; } = new();

    /// <summary>
    /// Признак агента
    /// </summary>
    public int? AgentSign { get; set; }

    /// <summary>
    /// Данные агента
    /// </summary>
    public Agent? Agent { get; set; }

    /// <summary>
    /// Данные поставщика
    /// </summary>
    public Vendor? Vendor { get; set; }

    /// <summary>
    /// Сведения о покупателе (клиенте)
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Оплаты
    /// </summary>
    public Payments Payments { get; set; } = new();

    /// <summary>
    /// Товары
    /// </summary>
    public List<Position> Positions { get; } = new();

    // Коррекция

    /// <summary>
    /// Данные коррекции
    /// </summary>
    public CorrectionData? CorrectionData { get; set; }

    /// <summary>
    /// Суммы НДС по ставкам для чека коррекции ФФД 1.05
    /// </summary>
    public Correction105Taxes? Correction105Taxes { get; set; }

    // Маркировка (вход)

    /// <summary>
    /// Код маркировки в кодировке Base64
    /// </summary>
    public string MarkingCode { get; set; } = "";

    /// <summary>
    /// Планируемый статус товара
    /// </summary>
    public int PlannedStatus { get; set; } = 1;

    /// <summary>
    /// Количество товара
    /// </summary>
    public decimal MarkingQuantity { get; set; } = 1;

    /// <summary>
    /// Мера количества предмета расчета
    /// </summary>
    public int MeasureOfQuantity { get; set; }

    /// <summary>
    /// Числитель дробного количества товара
    /// </summary>
    public int FractionalQuantityNumerator { get; set; }

    /// <summary>
    /// Знаменатель дробного количества товара.
    /// </summary>
    public int FractionalQuantityDenominator { get; set; }

    /// <summary>
    /// Не отправлять результат проверки на сервер ОИСМ
    /// </summary>
    public bool NotSendToServer { get; set; }

    /// <summary>
    /// Признак ожидания ответа ОИСМ
    /// </summary>
    public bool WaitForResult { get; set; }

    /// <summary>
    /// Уникальный код запроса КМ
    /// </summary>
    public string RequestKmGuid { get; set; } = "";

    /// <summary>
    /// Признак подтверждения кода маркировки
    /// </summary>
    public int ConfirmationType { get; set; }
}
