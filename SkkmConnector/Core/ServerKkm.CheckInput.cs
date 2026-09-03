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
    /// Номер фискального документа (ФД)
    /// </summary>
    public int CheckNumber { get; set; }

    /// <summary>
    /// Номер чека за смену
    /// </summary>
    public int CheckNumberInShift { get; set; }

    /// <summary>
    /// Регистрационный номер ККТ (РНМ)
    /// </summary>
    public string RnNumber { get; set; } = "";

    /// <summary>
    /// Адрес сайта ФНС
    /// </summary>
    public string FnsUrl { get; set; } = "";

    /// <summary>
    /// Время на сервере ККМ
    /// </summary>
    public string ServerDateTime { get; set; } = "";

    /// <summary>
    /// Дата и время документа по часам ФН 
    /// </summary>
    public string FiscalDateTime { get; set; } = "";

    /// <summary>
    /// Время ККТ
    /// </summary>
    public string DeviceDateTime { get; set; } = "";

    /// <summary>
    /// Состояние смены. Используйте enum <see cref="ShiftState"/>.
    /// </summary>
    public ShiftState? CurrentShiftState { get; set; }

    /// <summary>
    /// Количество непереданных в ОФД документов.
    /// </summary>
    public long BacklogDocumentsCount { get; set; }

    /// <summary>
    /// Номер первого непереданного документа.
    /// </summary>
    public long BacklogFirstDocumentNumber { get; set; }

    /// <summary>
    /// Дата и время первого непереданного документа.
    /// </summary>
    public DateTime? BacklogFirstDocumentDateTime { get; set; }

    /// <summary>
    /// Срок действия ФН.
    /// </summary>
    public string FnValidityDate { get; set; } = "";

    /// <summary>
    /// Остаток ресурса ФН в днях.
    /// </summary>
    public int FnDaysResources { get; set; }

    /// <summary>
    /// ФН присутствует
    /// </summary>
    public bool IsFnPresent { get; set; }

    /// <summary>
    /// Фискальный режим
    /// </summary>
    public bool IsFiscal { get; set; }

    /// <summary>
    /// Предупреждения ФН из ответа.
    /// </summary>
    public Warnings? FnWarnings { get; set; }

    /// <summary>
    /// Начало периода отбора отчётов, чеков и операций
    /// </summary>
    public DateTime ShiftsFrom { get; set; } = DateTime.Today.AddDays(-7);

    /// <summary>
    /// Конец периода отбора отчётов, чеков и операций
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
    /// Выравнивание изображения при печати. Используйте enum <see cref="PictureAlignment"/>.
    /// </summary>
    public PictureAlignment PictureAlignment { get; set; } = PictureAlignment.Center;

    // Слип

    /// <summary>
    /// Текст нефискального документа.
    /// </summary>
    public string TextForPrint { get; set; } = "";

    // Чек

    /// <summary>
    /// Тип чека / задания. Используйте enum <see cref="CheckType"/>.
    /// </summary>
    public CheckType PaymentType { get; set; } = CheckType.Sale;

    /// <summary>
    /// Только обработанные операции. Параметр <c>isProcessed</c> в <see cref="GetOperationLast"/>.
    /// </summary>
    public bool IsProcessed { get; set; }

    /// <summary>
    /// Система налогообложения. Используйте enum <see cref="TaxSystem"/>.
    /// </summary>
    public TaxSystem TaxVariant { get; set; } = TaxSystem.ОСН;

    /// <summary>
    /// Часовая зона. Используйте enum <see cref="CheckTimeZone"/>.
    /// </summary>
    public CheckTimeZone? TimeZone { get; set; }

    /// <summary>
    /// Чек только в электронном виде (без печати на бумаге).
    /// true — не печатать; для обычной печати оставляйте false.
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
    /// Отраслевой реквизит чека. Создайте объект <see cref="Industry"/>
    /// (IdentifierFoiv, DocumentDate, DocumentNumber, AttributeValue).
    /// </summary>
    public Industry? IndustryAttribute { get; set; }

    /// <summary>
    /// Дополнительный реквизит пользователя. Создайте объект <see cref="UserAttribute"/>
    /// (Name, Value).
    /// </summary>
    public UserAttribute? UserAttribute { get; set; }

    /// <summary>
    /// Операционный реквизит чека. Создайте объект <see cref="OperationalAttribute"/>
    /// (DateTime, OperationId, OperationData).
    /// </summary>
    public OperationalAttribute? OperationalAttribute { get; set; }

    /// <summary>
    /// Детализация безналичных оплат. Добавляйте объекты <see cref="ElectronicPayment"/>
    /// (Amount, PaymentMethod, Identifiers, AdditionalInformation).
    /// </summary>
    public List<ElectronicPayment> ElectronicPayments { get; } = new();

    /// <summary>
    /// Признак агента. Используйте enum <see cref="AgentType"/>.
    /// </summary>
    public AgentType? AgentSign { get; set; }

    /// <summary>
    /// Данные агента. Создайте объект <see cref="Agent"/> и заполните нужные поля.
    /// </summary>
    public Agent? Agent { get; set; }

    /// <summary>
    /// Данные поставщика. Создайте объект <see cref="Vendor"/>
    /// (Name, Phones, Vatin).
    /// </summary>
    public Vendor? Vendor { get; set; }

    /// <summary>
    /// Сведения о покупателе. Создайте объект <see cref="Customer"/> и заполните нужные поля.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Суммы оплаты. Создайте объект <see cref="Payments"/>
    /// (Cash, ElectronicPayment, AdvancePayment, Credit, CashProvision).
    /// </summary>
    public Payments Payments { get; set; } = new();

    /// <summary>
    /// Позиции чека. Добавляйте наследники <see cref="Position"/>:
    /// <see cref="FiscalLine"/>, <see cref="TextLine"/>, <see cref="BarcodeLine"/>,
    /// <see cref="PictureLine"/>, <see cref="SeparatorLine"/>.
    /// </summary>
    public List<Position> Positions { get; } = new();

    // Коррекция

    /// <summary>
    /// Данные коррекции. Создайте объект <see cref="CorrectionData"/>
    /// (Type, Description, Date, Number).
    /// </summary>
    public CorrectionData? CorrectionData { get; set; }

    /// <summary>
    /// Суммы НДС по ставкам для чека коррекции ФФД 1.05.
    /// Создайте объект <see cref="Correction105Taxes"/> и заполните нужные ставки.
    /// </summary>
    public Correction105Taxes? Correction105Taxes { get; set; }

    // Маркировка (вход)

    /// <summary>
    /// Код маркировки в кодировке Base64
    /// </summary>
    public string MarkingCode { get; set; } = "";

    /// <summary>
    /// Планируемый статус товара. Используйте enum <see cref="MarkingPlannedStatus"/>.
    /// </summary>
    public MarkingPlannedStatus PlannedStatus { get; set; } = MarkingPlannedStatus.Sold;

    /// <summary>
    /// Количество товара
    /// </summary>
    public decimal MarkingQuantity { get; set; } = 1;

    /// <summary>
    /// Мера количества предмета расчёта. Используйте enum <see cref="MeasureOfQuantity"/>.
    /// </summary>
    public MeasureOfQuantity MeasureOfQuantity { get; set; }

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
    /// Признак подтверждения кода маркировки. Используйте enum <see cref="KmConfirmationType"/>.
    /// </summary>
    public KmConfirmationType ConfirmationType { get; set; }
}
