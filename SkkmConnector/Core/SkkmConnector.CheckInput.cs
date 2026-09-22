namespace RBSoftSkkm;

// Входные свойства запроса: чек, коррекция, наличные, слип, картинки, маркировка.
public sealed partial class SkkmConnector
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

    // Шаблоны

    /// <summary>
    /// Имя шаблона печати или шаблона чека.
    /// </summary>
    public string TemplateName { get; set; } = "";

    /// <summary>
    /// Тип печатного шаблона
    /// </summary>
    public PrintTemplateType TemplateType { get; set; }

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
    /// Система налогообложения. 
    /// </summary>
    public TaxSystem TaxVariant { get; set; } = TaxSystem.Osn;

    /// <summary>
    /// Задать систему налогообложения на смену.
    /// </summary>
    public void SetTaxType(TaxSystem taxVariant) => TaxVariant = taxVariant;

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

    // Отраслевой реквизит чека (тег 1261)

    /// <summary>
    /// Идентификатор ФОИВ отраслевого реквизита чека.
    /// </summary>
    public string IndustryIdentifierFoiv { get; set; } = "";

    /// <summary>
    /// Дата документа-основания отраслевого реквизита чека.
    /// </summary>
    public string IndustryDocumentDate { get; set; } = "";

    /// <summary>
    /// Номер документа-основания отраслевого реквизита чека.
    /// </summary>
    public string IndustryDocumentNumber { get; set; } = "";

    /// <summary>
    /// Значение отраслевого реквизита чека.
    /// </summary>
    public string IndustryAttributeValue { get; set; } = "";

    // Дополнительный реквизит пользователя (тег 1084)

    /// <summary>
    /// Наименование дополнительного реквизита пользователя.
    /// </summary>
    public string UserAttributeName { get; set; } = "";

    /// <summary>
    /// Значение дополнительного реквизита пользователя.
    /// </summary>
    public string UserAttributeValue { get; set; } = "";

    // Операционный реквизит чека (тег 1270)

    /// <summary>
    /// Дата и время операции операционного реквизита чека.
    /// </summary>
    public string OperationalAttributeDateTime { get; set; } = "";

    /// <summary>
    /// Идентификатор операции операционного реквизита чека.
    /// </summary>
    public int? OperationalAttributeOperationId { get; set; }

    /// <summary>
    /// Данные операции операционного реквизита чека.
    /// </summary>
    public string OperationalAttributeData { get; set; } = "";

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
    /// Операция платёжного агента.
    /// </summary>
    public string AgentPayingAgentOperation { get; set; } = "";

    /// <summary>
    /// Телефон(ы) платёжного агента.
    /// </summary>
    public string[]? AgentPayingAgentPhone { get; set; }

    /// <summary>
    /// Телефон(ы) оператора по приёму платежей.
    /// </summary>
    public string[]? AgentReceivePaymentsOperatorPhone { get; set; }

    /// <summary>
    /// Телефон(ы) оператора перевода.
    /// </summary>
    public string[]? AgentMoneyTransferOperatorPhone { get; set; }

    /// <summary>
    /// Наименование оператора перевода.
    /// </summary>
    public string AgentMoneyTransferOperatorName { get; set; } = "";

    /// <summary>
    /// Адрес оператора перевода.
    /// </summary>
    public string AgentMoneyTransferOperatorAddress { get; set; } = "";

    /// <summary>
    /// ИНН оператора перевода.
    /// </summary>
    public string AgentMoneyTransferOperatorVatin { get; set; } = "";

    /// <summary>
    /// Наименование поставщика.
    /// </summary>
    public string VendorName { get; set; } = "";

    /// <summary>
    /// Телефон(ы) поставщика.
    /// </summary>
    public string[]? VendorPhones { get; set; }

    /// <summary>
    /// ИНН поставщика.
    /// </summary>
    public string VendorVatin { get; set; } = "";

    /// <summary>
    /// Наименование организации или фамилия, имя, отчество (при наличии)
    /// </summary>
    public string? CustomerInfo { get; set; }

    /// <summary>
    /// ИНН покупателя
    /// </summary>
    public string? CustomerVatin { get; set; }

    /// <summary>
    /// Электронная почта покупателя
    /// </summary>
    public string? CustomerEmail { get; set; }
    
    /// <summary>
    /// Номер телефона покупателя
    /// </summary>
    public string? CustomerPhone { get; set; }

    /// <summary>
    /// Дата рождения покупателя (клиента) в формате "DD.MM.YYYY"
    /// </summary>
    public string? CustomerDateOfBirth {  get; set; }

    /// <summary>
    /// Числовой код страны, гражданином которой является покупатель (клиент).
    /// Код страны указывается в соответствии с Общероссийским классификатором стран мира ОКСМ.
    /// </summary>
    public string? CustomerCitizenship { get; set;  }

    /// <summary>
    /// Числовой код вида документа, удостоверяющего личность (ФФД, Таблица 116)
    /// </summary>
    public string? CustomerDocumentTypeCode { get; set; }

    /// <summary>
    /// Данные документа, удостоверяющего личность
    /// </summary>
    public string? CustomerDocumentData { get; set; }

    /// <summary>
    /// Адрес покупателя (клиента)
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// Суммы оплаты. Создайте объект <see cref="Payments"/>
    /// (Cash, ElectronicPayment, AdvancePayment, Credit, CashProvision).
    /// </summary>
    public Payments Payments { get; set; } = new();

    /// <summary>
    /// Позиции чека
    /// </summary>
    private readonly List<Position> _positions = new();

    /// <summary>
    /// Добавление фискальной строки (товара, услуги) в чек.
    /// Обязательные поля задаются параметрами: наименование, количество, сумма со скидкой и ставка НДС;
    /// признак предмета расчёта и признак способа расчёта (если не заданы, применяются
    /// значения ККТ по умолчанию). Цена за единицу вычисляется как сумма, делённая на количество.
    /// </summary>
    public FiscalLine AddPosition(
        string name,
        decimal quantity,
        decimal sum,
        TaxRate tax,
        SignCalculationObject? signCalculationObject = null,
        SignMethodCalculation? signMethodCalculation = null)
    {
        var line = new FiscalLine
        {
            Name = name,
            Quantity = quantity,
            Sum = sum,
            Price = quantity != 0 ? sum / quantity : sum,
            Tax = tax,
            SignCalculationObject = signCalculationObject,
            SignMethodCalculation = signMethodCalculation
        };
        _positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавление текстовой строки в чек или печатный шаблон.
    /// </summary>
    public TextLine AddText(
        string text,
        PrintFont font = PrintFont.Normal,
        PrintAlignment alignment = PrintAlignment.Left)
    {
        var line = new TextLine { Text = text, Font = font, Alignment = alignment };
        _positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавление текстовой строки из двух частей: левая прижимается к левому краю,
    /// правая — к правому (например, «Итого» и сумма). Перенос при этом отключается.
    /// </summary>
    public TextLine AddText(string left, string right, PrintFont font = PrintFont.Normal)
    {
        var line = new TextLine
        {
            Text = left,
            LineRight = right,
            Font = font,
            Wrap = false
        };
        _positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавление строки штрихкода в чек или печатный шаблон.
    /// Высота и ширина штриха задаются в точках; печать текста действует только для одномерных.
    /// </summary>
    public BarcodeLine AddBarcode(
        BarcodeType type,
        string value,
        PrintAlignment alignment = PrintAlignment.Center,
        int height = 0,
        int barWidth = 0,
        BarcodePrintText printText = BarcodePrintText.None)
    {
        var line = new BarcodeLine
        {
            Type = type,
            Barcode = value,
            Alignment = alignment,
            Height = height,
            BarWidth = barWidth,
            PrintText = printText
        };
        _positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавление разделительной линии в чек или печатный шаблон. Стиль по умолчанию — сплошная линия.
    /// </summary>
    public SeparatorLine AddSeparatorLine(LineStyle lineStyle = LineStyle.Solid)
    {
        var line = new SeparatorLine { LineStyle = lineStyle };
        _positions.Add(line);
        return line;
    }

    /// <summary>
    /// Добавление изображения в чек или печатный шаблон. Картинка передаётся в Base64;
    /// ширина и высота — в точках (0 — размер изображения).
    /// </summary>
    public PictureLine AddPicture(
        string valueBase64,
        PictureAlignment alignment = PictureAlignment.Center,
        int width = 0,
        int height = 0)
    {
        var line = new PictureLine
        {
            Value = valueBase64,
            Alignment = alignment,
            Width = width > 0 ? width : null,
            Height = height > 0 ? height : null
        };
        _positions.Add(line);
        return line;
    }

    // Коррекция

    /// <summary>
    /// Тип коррекции
    /// </summary>
    public CorrectionTypes CorrectionType { get; set; } = CorrectionTypes.Самостоятельно;

    /// <summary>
    /// Описание коррекции.
    /// </summary>
    public string CorrectionDescription { get; set; } = "";

    /// <summary>
    /// Дата совершения корректируемого расчёта.
    /// </summary>
    public DateTime CorrectionDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Номер предписания налогового органа (для типа коррекции «По предписанию»).
    /// </summary>
    public string CorrectionNumber { get; set; } = "";

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
