using SkkmConnector.Internal;

namespace SkkmConnector;

// Сборка запросов из свойств: чек, коррекция, слип и служебные документы.
public sealed partial class ServerKkm
{
    /// <summary>
    /// Собирает тело обычного чека (продажа, возврат и т.п.) для отправки на сервер.
    /// </summary>
    private CheckParameters CheckBody()
    {
        return new CheckParameters
        {
            DeviceName = DeviceName,
            DocId = Guid.NewGuid().ToString(),
            PaymentType = PaymentType,
            TaxVariant = TaxVariant,
            Electronically = Electronically,
            Cashier = MakeCashier(),
            Customer = CustomerBody(),
            SaleLocation = SaleLocation,
            TextBefore = TextBefore,
            TextAfter = TextAfter,
            Payments = PaymentsBody(),
            Positions = BuildPositions()
        };
    }

    /// <summary>
    /// Собирает тело чека коррекции ФФД 1.2: те же поля, что у обычного чека,
    /// плюс блок данных коррекции и номер корректируемого чека.
    /// </summary>
    private Correction120Parameters CorrectionBody()
    {
        return new Correction120Parameters
        {
            DeviceName = DeviceName,
            DocId = Guid.NewGuid().ToString(),
            PaymentType = PaymentType,
            TaxVariant = TaxVariant,
            Electronically = Electronically,
            Cashier = MakeCashier(),
            Customer = CustomerBody(),
            SaleLocation = SaleLocation,
            TextBefore = TextBefore,
            TextAfter = TextAfter,
            Payments = PaymentsBody(),
            Positions = BuildPositions(),
            CorrectionData = new CorrectionData
            {
                Type = CorrectionType,
                Description = CorrectionDescription,
                Date = CorrectionDate,
                Number = CorrectionNumber
            },
            AdditionalAttribute = CorrectedCheckNumber
        };
    }

    /// <summary>
    /// Собирает тело слипа (нефискальная печать) из текста <see cref="TextForPrint"/>.
    /// Разметку строк (шрифт, выравнивание, штрихкоды) разбирает <see cref="SlipTextParser"/>.
    /// </summary>
    private DocumentParameters SlipBody()
    {
        return new DocumentParameters
        {
            DeviceName = DeviceName,
            DocId = Guid.NewGuid().ToString(),
            Cashier = MakeCashier(),
            Positions = SlipTextParser.Parse(TextForPrint)
        };
    }

    /// <summary>
    /// Собирает блок оплат чека из сумм по видам оплаты.
    /// </summary>
    private Payments PaymentsBody()
    {
        return new Payments
        {
            Cash = PayCash,
            ElectronicPayment = PayElectronic,
            Credit = PayCredit,
            AdvancePayment = PayAdvance,
            CashProvision = PayProvision
        };
    }

    /// <summary>
    /// Собирает позиции чека из строк <see cref="Positions"/>.
    /// </summary>
    private Position[] BuildPositions()
    {
        var result = new List<Position>();
        foreach (var line in Positions)
            result.AddRange(PositionsOf(line));
        return result.ToArray();
    }

    /// <summary>
    /// Собирает блок покупателя. Если ни одно поле покупателя не заполнено, возвращает null,
    /// чтобы блок Customer вообще не попал в запрос.
    /// </summary>
    private Customer? CustomerBody()
    {
        if (CustomerInfo.Length == 0 && CustomerVatin.Length == 0 &&
            CustomerEmail.Length == 0 && CustomerPhone.Length == 0)
            return null;

        return new Customer
        {
            Info = CustomerInfo,
            Vatin = CustomerVatin,
            Email = CustomerEmail,
            Phone = CustomerPhone
        };
    }

    /// <summary>
    /// Превращает одну строку чека в позиции запроса.
    /// Одна строка может дать несколько позиций: товар (или текст) + дополнительный текст + штрихкод.
    /// </summary>
    private static IEnumerable<Position> PositionsOf(CheckLine line)
    {
        if (line.IsFiscalLine)
            yield return new Position { FiscalString = FiscalStringOf(line) };
        else if (!string.IsNullOrWhiteSpace(line.Name))
            yield return new Position { TextString = new TextString { Text = line.Name } };

        if (!string.IsNullOrWhiteSpace(line.ExtraText))
            yield return new Position { TextString = new TextString { Text = line.ExtraText } };

        if (!string.IsNullOrWhiteSpace(line.BarcodeType))
            yield return new Position
            {
                Barcode = new Barcode { Type = line.BarcodeType.Trim(), Value = line.Barcode }
            };
    }

    /// <summary>
    /// Строит фискальную позицию из строки чека.
    /// Подставляет умолчания (единица «шт», ставка «none»), считает сумму как количество × цена,
    /// если сумма не задана явно, и добавляет необязательные блоки (агент, поставщик, маркировка, отраслевой реквизит).
    /// </summary>
    private static FiscalString FiscalStringOf(CheckLine line) => new()
    {
        Name = line.Name,
        Quantity = line.Quantity,
        PriceWithDiscount = line.Price,
        SumWithDiscount = line.Sum != 0
            ? Math.Round(line.Sum, 2)
            : Math.Round(line.Quantity * line.Price, 2),
        DiscountSum = line.DiscountSum,
        Department = line.Department,
        Tax = string.IsNullOrWhiteSpace(line.Tax) ? "none" : line.Tax.Trim(),
        TaxSum = line.TaxSum,
        SignMethodCalculation = line.SignMethodCalculation,
        SignCalculationObject = line.SignCalculationObject,
        MeasurementUnit = string.IsNullOrWhiteSpace(line.MeasurementUnit) ? "шт" : line.MeasurementUnit.Trim(),
        ExciseAmount = line.ExciseAmount != 0 ? line.ExciseAmount : null,
        CountryOfOrigin = line.CountryOfOrigin,
        CustomsDeclaration = line.CustomsDeclaration,
        SignSubjectCalculationAgent = line.AgentSign != 0 ? line.AgentSign : null,
        AgentData = AgentOf(line),
        Vendor = VendorOf(line),
        GoodCodeData = MarkingOf(line),
        MeasureOfQuantity = line.MeasureOfQuantity,
        FractionalQuantity = line.FractionalNumerator > 0 && line.FractionalDenominator > 0
            ? new FractionalQuantity { Numerator = line.FractionalNumerator, Denominator = line.FractionalDenominator }
            : null,
        AdditionalAttribute = line.AdditionalAttribute,
        IndustryAttribute = IndustryOf(line)
    };

    /// <summary>
    /// Блок данных платёжного агента. Возвращает null, если операция агента не указана.
    /// </summary>
    private static AgentData? AgentOf(CheckLine line)
    {
        if (string.IsNullOrWhiteSpace(line.PayingAgentOperation))
            return null;

        return new AgentData
        {
            PayingAgentOperation = line.PayingAgentOperation,
            PayingAgentPhone = PhoneArray(line.PayingAgentPhone),
            ReceivePaymentsOperatorPhone = PhoneArray(line.ReceivePaymentsOperatorPhone),
            MoneyTransferOperatorPhone = PhoneArray(line.MoneyTransferOperatorPhone),
            MoneyTransferOperatorName = line.MoneyTransferOperatorName,
            MoneyTransferOperatorAddress = line.MoneyTransferOperatorAddress,
            MoneyTransferOperatorVatin = line.MoneyTransferOperatorVatin
        };
    }

    /// <summary>
    /// Блок данных поставщика. Возвращает null, если наименование поставщика не указано.
    /// </summary>
    private static VendorData? VendorOf(CheckLine line)
    {
        if (string.IsNullOrWhiteSpace(line.PurveyorName))
            return null;

        return new VendorData
        {
            Name = line.PurveyorName,
            Phones = PhoneArray(line.PurveyorPhone),
            Vatin = line.PurveyorVatin
        };
    }

    /// <summary>
    /// Блок кода маркировки товара. Возвращает null, если не заполнен ни один из вариантов
    /// (GTIN/серийный номер, тип товара/КИЗ, код маркировки в Base64).
    /// </summary>
    private static GoodCodeData? MarkingOf(CheckLine line)
    {
        if (string.IsNullOrWhiteSpace(line.Gtin) &&
            string.IsNullOrWhiteSpace(line.SerialNumber) &&
            string.IsNullOrWhiteSpace(line.CommodityGroup) &&
            string.IsNullOrWhiteSpace(line.Kiz) &&
            string.IsNullOrWhiteSpace(line.KizBase64))
            return null;

        return new GoodCodeData
        {
            Gtin = line.Gtin,
            SerialNumber = line.SerialNumber,
            CommodityGroup = line.CommodityGroup,
            Barcode = line.Kiz,
            MarkingCode = line.KizBase64
        };
    }

    /// <summary>
    /// Отраслевой реквизит предмета расчёта. Возвращает null, если идентификатор ФОИВ не указан.
    /// </summary>
    private static IndustryAttribute? IndustryOf(CheckLine line)
    {
        if (string.IsNullOrWhiteSpace(line.IndustryFoiv))
            return null;

        return new IndustryAttribute
        {
            IdentifierFoiv = line.IndustryFoiv,
            DocumentDate = line.IndustryDocumentDate,
            DocumentNumber = line.IndustryDocumentNumber,
            AttributeValue = line.IndustryAttributeValue
        };
    }

    /// <summary>
    /// Минимальное тело запроса — только имя кассы. Для операций, которым не нужен кассир.
    /// </summary>
    private CheckbaseParameters DeviceOnly() => new()
    {
        DeviceName = DeviceName
    };

    /// <summary>
    /// Базовое тело запроса: касса, идентификатор документа и кассир.
    /// Используется операциями смены и денежного ящика (открыть/закрыть смену, X-отчёт и т.п.).
    /// </summary>
    private CheckbaseParameters BaseDocument() => new()
    {
        DeviceName = DeviceName,
        DocId = Guid.NewGuid().ToString(),
        Cashier = MakeCashier()
    };

    /// <summary>
    /// Тело запроса для внесения и выемки: базовые поля плюс сумма из <see cref="CashAmount"/>.
    /// </summary>
    private CashdrawParameters CashDocument() => new()
    {
        DeviceName = DeviceName,
        DocId = Guid.NewGuid().ToString(),
        Cashier = MakeCashier(),
        Sum = CashAmount
    };

    /// <summary>
    /// Блок кассира из свойств <see cref="CashierName"/> и <see cref="CashierVatin"/>.
    /// Пустые значения сервер трактует как «кассир не передан».
    /// </summary>
    private Cashier MakeCashier() => new()
    {
        Name = CashierName.Trim(),
        Vatin = CashierVatin.Trim()
    };

    /// <summary>
    /// Заворачивает телефон в массив строк — сервер ждёт телефоны именно так.
    /// Пустой телефон превращает в null, чтобы поле не попало в запрос.
    /// </summary>
    private static string[]? PhoneArray(string? phone)
    {
        phone = phone?.Trim();
        return string.IsNullOrEmpty(phone) ? null : [phone!];
    }
}
