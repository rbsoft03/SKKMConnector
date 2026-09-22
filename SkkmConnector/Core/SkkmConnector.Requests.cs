using RBSoftSkkm.Internal;

namespace RBSoftSkkm;

public sealed partial class SkkmConnector
{
    /// <summary>
    /// Код ошибки для проверок на стороне клиента.
    /// </summary>
    private const int ValidationErrorCode = -1;

    /// <summary>
    /// Проверка обязательных полей чека перед отправкой: имя кассы, наличие хотя бы одной
    /// фискальной строки и заполненность её ключевых полей (наименование, ставка НДС, количество).
    /// Если что-то не заполнено — чек не отправляется, а причина кладётся в
    /// <see cref="Ok"/> = false и <see cref="ErrorDescription"/>.
    /// </summary>
    private bool ValidateCheck()
    {
        if (string.IsNullOrWhiteSpace(DeviceName))
            return FailValidation("Не указано имя кассы.");

        return ValidatePositions();
    }

    /// <summary>
    /// Проверка товарной части: в документе должна быть хотя бы одна фискальная строка
    /// с наименованием, количеством и признаками расчёта.
    /// </summary>
    private bool ValidatePositions()
    {
        var fiscalCount = 0;
        var number = 0;
        foreach (var position in _positions)
        {
            number++;
            if (position is not FiscalLine line)
                continue;

            fiscalCount++;
            if (string.IsNullOrWhiteSpace(line.Name))
                return FailValidation($"Не заполнено наименование товара в позиции №{number} (FiscalLine.Name).");
            if (line.Quantity <= 0)
                return FailValidation($"Количество товара в позиции №{number} должно быть больше нуля (FiscalLine.Quantity).");
            if (line.SignMethodCalculation == null)
                return FailValidation($"Не указан признак способа расчёта в позиции №{number} (FiscalLine.SignMethodCalculaion)");
            if (line.SignCalculationObject == null)
                return FailValidation($"Не указан признак предмета расчёта в позиции №{number} (FiscalLine.SignCalculationObject)");
        }

        if (fiscalCount == 0)
            return FailValidation("В чеке нет ни одной фискальной строки");

        return true;
    }

    /// <summary>
    /// Проверка шаблона печати перед отправкой: имя шаблона и наличие хотя бы одной строки.
    /// </summary>
    private bool ValidateTemplate()
    {
        if (!ValidateTemplateName(TemplateName, "шаблона печати"))
            return false;

        if (!_positions.Any(position => position is not FiscalLine))
            return FailValidation("В шаблоне печати нет ни одной строки");

        return true;
    }

    /// <summary>
    /// Проверка шаблона чека перед отправкой: имя шаблона и товарная часть чека.
    /// </summary>
    private bool ValidateCheckTemplate(string name)
        => ValidateTemplateName(name, "шаблона чека") && ValidatePositions();

    /// <summary>
    /// Имя шаблона — уникальный идентификатор на сервере: непустое и без пробелов.
    /// </summary>
    private bool ValidateTemplateName(string name, string kind)
    {
        if (string.IsNullOrWhiteSpace(name))
            return FailValidation($"Не указано имя {kind} (TemplateName).");

        if (name.Any(char.IsWhiteSpace))
            return FailValidation($"Имя {kind} «{name}» содержит пробелы — сервер такое имя не примет.");

        return true;
    }

    /// <summary>
    /// Фиксация ошибки проверки: чек не отправляется
    /// </summary>
    private bool FailValidation(string message)
    {
        Ok = false;
        ErrorCode = ValidationErrorCode;
        ErrorDescription = message;
        LastResult = default;
        FiscalResult = null;
        return false;
    }

    /// <summary>
    /// Касса и кассир.
    /// </summary>
    private void FillBase(CheckbaseParameters check)
    {
        check.DeviceName = DeviceName;
        check.Cashier = BuildCashier();
    }

    /// <summary>
    /// Кассир из плоских полей CashierName / CashierVatin.
    /// Если оба пусты, кассир в чек не пишется.
    /// </summary>
    private Cashier? BuildCashier()
    {
        if (string.IsNullOrEmpty(CashierName) && string.IsNullOrEmpty(CashierVatin))
            return null;

        return new Cashier
        {
            Name = CashierName,
            Vatin = CashierVatin
        };
    }

    /// <summary>
    /// Покупатель (клиент) из плоских полей Customer.
    /// Если ни одно поле не заполнено, сведения о покупателе не отправляются.
    /// </summary>
    private Customer? BuildCustomer()
    {
        if (string.IsNullOrEmpty(CustomerInfo)
            && string.IsNullOrEmpty(CustomerVatin)
            && string.IsNullOrEmpty(CustomerEmail)
            && string.IsNullOrEmpty(CustomerPhone)
            && string.IsNullOrEmpty(CustomerDateOfBirth)
            && string.IsNullOrEmpty(CustomerCitizenship)
            && string.IsNullOrEmpty(CustomerDocumentTypeCode)
            && string.IsNullOrEmpty(CustomerDocumentData)
            && string.IsNullOrEmpty(CustomerAddress))
            return null;

        return new Customer
        {
            Info = CustomerInfo,
            Vatin = CustomerVatin,
            Email = CustomerEmail,
            Phone = CustomerPhone,
            DateOfBirth = CustomerDateOfBirth,
            Citizenship = CustomerCitizenship,
            DocumentTypeCode = CustomerDocumentTypeCode,
            DocumentData = CustomerDocumentData,
            Address = CustomerAddress
        };
    }

    /// <summary>
    /// Данные агента уровня чека из плоских полей Agent.
    /// Если ни одно поле не заполнено, данные агента не отправляются.
    /// </summary>
    private Agent? BuildAgent()
    {
        if (string.IsNullOrEmpty(AgentPayingAgentOperation)
            && (AgentPayingAgentPhone == null || AgentPayingAgentPhone.Length == 0)
            && (AgentReceivePaymentsOperatorPhone == null || AgentReceivePaymentsOperatorPhone.Length == 0)
            && (AgentMoneyTransferOperatorPhone == null || AgentMoneyTransferOperatorPhone.Length == 0)
            && string.IsNullOrEmpty(AgentMoneyTransferOperatorName)
            && string.IsNullOrEmpty(AgentMoneyTransferOperatorAddress)
            && string.IsNullOrEmpty(AgentMoneyTransferOperatorVatin))
            return null;

        return new Agent
        {
            PayingAgentOperation = AgentPayingAgentOperation,
            PayingAgentPhone = AgentPayingAgentPhone,
            ReceivePaymentsOperatorPhone = AgentReceivePaymentsOperatorPhone,
            MoneyTransferOperatorPhone = AgentMoneyTransferOperatorPhone,
            MoneyTransferOperatorName = AgentMoneyTransferOperatorName,
            MoneyTransferOperatorAddress = AgentMoneyTransferOperatorAddress,
            MoneyTransferOperatorVatin = AgentMoneyTransferOperatorVatin
        };
    }

    /// <summary>
    /// Данные поставщика уровня чека из плоских полей Vendor.
    /// Если ни одно поле не заполнено, данные поставщика не отправляются.
    /// </summary>
    private Vendor? BuildVendor()
    {
        if (string.IsNullOrEmpty(VendorName)
            && string.IsNullOrEmpty(VendorVatin)
            && (VendorPhones == null || VendorPhones.Length == 0))
            return null;

        return new Vendor
        {
            Name = VendorName,
            Phones = VendorPhones,
            Vatin = VendorVatin
        };
    }

    /// <summary>
    /// Отраслевой реквизит чека из плоских полей Industry.
    /// Если ни одно поле не заполнено, реквизит не отправляется.
    /// </summary>
    private Industry? BuildIndustry()
    {
        if (string.IsNullOrEmpty(IndustryIdentifierFoiv)
            && string.IsNullOrEmpty(IndustryDocumentDate)
            && string.IsNullOrEmpty(IndustryDocumentNumber)
            && string.IsNullOrEmpty(IndustryAttributeValue))
            return null;

        return new Industry
        {
            IdentifierFoiv = IndustryIdentifierFoiv,
            DocumentDate = IndustryDocumentDate,
            DocumentNumber = IndustryDocumentNumber,
            AttributeValue = IndustryAttributeValue
        };
    }

    /// <summary>
    /// Дополнительный реквизит пользователя из плоских полей UserAttribute.
    /// Если оба поля пусты, реквизит не отправляется.
    /// </summary>
    private UserAttribute? BuildUserAttribute()
    {
        if (string.IsNullOrEmpty(UserAttributeName) && string.IsNullOrEmpty(UserAttributeValue))
            return null;

        return new UserAttribute
        {
            Name = UserAttributeName,
            Value = UserAttributeValue
        };
    }

    /// <summary>
    /// Операционный реквизит чека из плоских полей OperationalAttribute.
    /// Если ни одно поле не заполнено, реквизит не отправляется.
    /// </summary>
    private OperationalAttribute? BuildOperationalAttribute()
    {
        if (string.IsNullOrEmpty(OperationalAttributeDateTime)
            && string.IsNullOrEmpty(OperationalAttributeData)
            && !OperationalAttributeOperationId.HasValue)
            return null;

        return new OperationalAttribute
        {
            DateTime = OperationalAttributeDateTime,
            OperationId = OperationalAttributeOperationId,
            OperationData = OperationalAttributeData
        };
    }

    /// <summary>
    /// Смена, X/Z - отчёт, отчёт о расчётах, денежный ящик.
    /// </summary>
    private CheckbaseParameters CheckBase()
    {
        var check = new CheckbaseParameters();
        FillBase(check);
        return check;
    }

    /// <summary>
    /// Обычный чек.
    /// </summary>
    private CheckParameters CheckBody()
    {
        var check = new CheckParameters();
        FillCheck(check);
        return check;
    }

    /// <summary>
    /// Чек коррекции ФФД 1.2.
    /// </summary>
    private Correction120Parameters Correction120Body()
    {
        var check = new Correction120Parameters { CorrectionData = BuildCorrection() };
        FillCheck(check);
        return check;
    }

    /// <summary>
    /// Данные коррекции из плоских полей Correction.
    /// </summary>
    private CorrectionData BuildCorrection()
    {
        return new CorrectionData
        {
            Type = CorrectionType,
            Description = CorrectionDescription,
            Date = CorrectionDate,
            Number = CorrectionNumber
        };
    }

    /// <summary>
    /// Заполнение полей чека
    /// </summary>
    private void FillCheck(CheckParameters check)
    {
        FillBase(check);
        check.PaymentType = (int)PaymentType;
        check.TaxVariant = (int)TaxVariant;
        check.Customer = BuildCustomer();
        check.SenderEmail = SenderEmail;
        check.SaleAddress = SaleAddress;
        check.SaleLocation = SaleLocation;
        check.AgentSign = AgentSign.HasValue ? (int)AgentSign.Value : null;
        check.AgentData = BuildAgent();
        check.Vendor = BuildVendor();
        check.Positions = BuildPositions();
        check.Payments = Payments;
        check.ElectronicPaymentInfo = ElectronicPayments.Count == 0 ? null : ElectronicPayments;
        check.TextBefore = TextBefore;
        check.TextAfter = TextAfter;
        check.Electronically = Electronically;
        check.OperationalAttribute = BuildOperationalAttribute();
        check.IndustryAttribute = BuildIndustry();
        check.UserAttribute = BuildUserAttribute();
        check.TimeZone = TimeZone.HasValue ? (int)TimeZone.Value : null;
        check.OperationOnline = OperationOnline ? true : null;
        check.AdditionalAttribute = AdditionalAttribute;
    }

    /// <summary>
    /// Чек коррекции ФФД 1.05.
    /// </summary>
    private Correction105Parameters Correction105Body()
    {
        var taxes = Correction105Taxes;
        var check = new Correction105Parameters
        {
            CorrectionData = BuildCorrection(),
            PaymentType = (int)PaymentType,
            TaxVariant = (int)TaxVariant,
            Payments = Payments,
            SumTaxNone = taxes?.SumTaxNone,
            SumTax0 = taxes?.SumTax0,
            SumTax5 = taxes?.SumTax5,
            SumTax7 = taxes?.SumTax7,
            SumTax10 = taxes?.SumTax10,
            SumTax105 = taxes?.SumTax105,
            SumTax107 = taxes?.SumTax107,
            SumTax110 = taxes?.SumTax110,
            SumTax118 = taxes?.SumTax118,
            SumTax18 = taxes?.SumTax18,
            SumTax20 = taxes?.SumTax20,
            SumTax120 = taxes?.SumTax120,
            SumTax22 = taxes?.SumTax22,
            SumTax122 = taxes?.SumTax122,
            AdditionalAttribute = AdditionalAttribute
        };
        FillBase(check);
        return check;
    }

    /// <summary>
    /// Слип.
    /// </summary>
    private DocumentParameters SlipBody()
    {
        var check = new DocumentParameters { Positions = SlipTextParser.Parse(TextForPrint) };
        FillBase(check);
        return check;
    }

    /// <summary>
    /// Внесение / выемка.
    /// </summary>
    private CashdrawParameters CashBody()
    {
        var check = new CashdrawParameters { Sum = CashAmount };
        FillBase(check);
        return check;
    }

    /// <summary>
    /// Позиции чека в модель запроса.
    /// </summary>
    private ApiPosition[] BuildPositions()
        => ToApiPositions(_positions);

    /// <summary>
    /// Тело template: имя, тип и строки
    /// </summary>
    private TemplateRequest TemplateBody()
    {
        return new TemplateRequest
        {
            Name = TemplateName,
            Type = (int)TemplateType,
            TemplateItems = BuildPrintLines(_positions)
                .Select(line => new TemplateItem { PrintLine = line })
                .ToArray()
        };
    }

    /// <summary>
    /// Тело checkTemplate: собирается из текущего состояния чека
    /// только с именем шаблона.
    /// </summary>
    private CheckTemplateRequest CheckTemplateBody(string name)
    {
        var check = CheckBody();
        return new CheckTemplateRequest
        {
            Name = name,
            Document = new CheckTemplateDocumentRequest
            {
                PaymentType = check.PaymentType,
                TaxVariant = check.TaxVariant,
                Customer = check.Customer,
                SenderEmail = check.SenderEmail,
                SaleAddress = check.SaleAddress,
                SaleLocation = check.SaleLocation,
                Positions = check.Positions,
                Payments = check.Payments,
                ElectronicPaymentInfo = check.ElectronicPaymentInfo,
                Electronically = check.Electronically,
                OperationalAttribute = check.OperationalAttribute,
                IndustryAttribute = check.IndustryAttribute,
                UserAttribute = check.UserAttribute,
                TimeZone = check.TimeZone,
                OperationOnline = check.OperationOnline ?? false,
                AdditionalAttribute = check.AdditionalAttribute,
                CorrectionData = HasCorrectionData() ? BuildCorrection() : null
            }
        };
    }

    private bool HasCorrectionData()
        => !string.IsNullOrWhiteSpace(CorrectionDescription)
           || !string.IsNullOrWhiteSpace(CorrectionNumber)
           || CorrectionDate != default && CorrectionDate != DateTime.Today
           || CorrectionType != CorrectionTypes.Самостоятельно;

    private static ApiPosition[] ToApiPositions(IEnumerable<Position>? positions)
    {
        if (positions == null)
            return [];

        var result = new List<ApiPosition>();
        foreach (var position in positions)
            result.Add(ToApi(position));
        return result.ToArray();
    }

    /// <summary>
    /// Одна позиция чека в модель запроса по её типу.
    /// </summary>
    private static ApiPosition ToApi(Position position) => position switch
    {
        FiscalLine fiscal => new ApiPosition { FiscalString = fiscal },
        TextLine text => TextToApi(text),
        BarcodeLine barcode => new ApiPosition { Barcode = barcode },
        SeparatorLine separator => new ApiPosition { SeparatorLine = separator },
        PictureLine picture => new ApiPosition { Picture = picture },
        _ => throw new InvalidOperationException(
            $"Неизвестный тип позиции «{position.GetType().Name}». " +
            "Допустимы FiscalLine, TextLine, BarcodeLine, SeparatorLine, PictureLine.")
    };

    /// <summary>
    /// Текст с префиксом стиля линии ([dotted], [line], [line,dashed]) уходит как SeparatorLine.
    /// </summary>
    private static ApiPosition TextToApi(TextLine text)
    {
        var parsed = SlipTextParser.ParseLine(text.Text, text.Font, text.Alignment);
        return new ApiPosition
        {
            TextString = parsed.TextString,
            Barcode = parsed.Barcode,
            SeparatorLine = parsed.SeparatorLine,
            Picture = parsed.Picture
        };
    }

    /// <summary>
    /// Позиции чека в строки печатного шаблона (фискальные строки пропускаются).
    /// </summary>
    private static List<PrintLine> BuildPrintLines(IEnumerable<Position> positions)
    {
        var lines = new List<PrintLine>();
        foreach (var position in positions)
        {
            var line = ToPrintLine(position);
            if (line != null)
                lines.Add(line);
        }
        return lines;
    }

    private static PrintLine? ToPrintLine(Position position) => position switch
    {
        TextLine text => new PrintLine
        {
            Type = PrintLineType.Text,
            Line = text.Text,
            LineRight = text.LineRight,
            Font = text.Font ?? PrintFont.Normal,
            Alignment = text.Alignment ?? PrintAlignment.Left,
            Wrap = text.Wrap
        },
        SeparatorLine separator => new PrintLine
        {
            Type = PrintLineType.Separator,
            SeparatorLine = separator
        },
        BarcodeLine barcode => new PrintLine
        {
            Type = PrintLineType.Barcode,
            Alignment = barcode.Alignment ?? PrintAlignment.Center,
            Barcode = new PrintFormBarcode
            {
                Type = barcode.Type,
                Value = barcode.Barcode,
                Height = barcode.Height,
                BarWidth = barcode.BarWidth,
                PrintText = barcode.PrintText
            }
        },
        PictureLine picture => new PrintLine
        {
            Type = PrintLineType.Picture,
            Picture = new Picture
            {
                PictureBase64 = picture.Value,
                Alignment = picture.Alignment,
                Width = picture.Width,
                Height = picture.Height
            }
        },
        _ => null
    };

    private void RestorePositionsFromPrintLines(IEnumerable<PrintLine> lines)
    {
        _positions.Clear();
        foreach (var line in lines)
        {
            switch (line.Type)
            {
                case PrintLineType.Separator:
                    AddSeparatorLine(line.SeparatorLine?.LineStyle ?? LineStyle.Solid);
                    break;
                case PrintLineType.Barcode when line.Barcode != null:
                    AddBarcode(
                        line.Barcode.Type ?? BarcodeType.QR,
                        line.Barcode.Value ?? "",
                        line.Alignment,
                        line.Barcode.Height,
                        line.Barcode.BarWidth,
                        line.Barcode.PrintText);
                    break;
                case PrintLineType.Picture when line.Picture != null:
                    AddPicture(
                        line.Picture.PictureBase64 ?? "",
                        line.Picture.Alignment,
                        line.Picture.Width ?? 0,
                        line.Picture.Height ?? 0);
                    break;
                default:
                    if (!string.IsNullOrEmpty(line.LineRight))
                        AddText(line.Line ?? "", line.LineRight, line.Font);
                    else
                        AddText(line.Line ?? "", line.Font, line.Alignment);
                    break;
            }
        }
    }

    private void RestoreCheckFromTemplate(CheckTemplateDocument document)
    {
        PaymentType = document.PaymentType;
        TaxVariant = document.TaxVariant;
        Electronically = document.Electronically;
        OperationOnline = document.OperationOnline;
        TimeZone = document.TimeZone;
        if (!string.IsNullOrEmpty(document.AdditionalAttribute))
            AdditionalAttribute = document.AdditionalAttribute!;

        if (document.Payments != null)
        {
            Payments = new Payments
            {
                Cash = document.Payments.Cash,
                ElectronicPayment = document.Payments.Electronic,
                AdvancePayment = document.Payments.PrePaid,
                Credit = document.Payments.Credit,
                CashProvision = document.Payments.Barter
            };
        }

        if (document.CorrectionData != null)
        {
            CorrectionType = document.CorrectionData.Type;
            CorrectionDescription = document.CorrectionData.Description ?? "";
            CorrectionDate = document.CorrectionData.Date;
            CorrectionNumber = document.CorrectionData.Number ?? "";
        }

        _positions.Clear();
        foreach (var item in document.CheckItems)
        {
            if (!item.IsFiscal && string.IsNullOrWhiteSpace(item.Name))
                continue;

            var line = AddPosition(
                item.Name ?? "",
                item.Quantity > 0 ? item.Quantity : 1,
                item.Sum,
                TaxRateFromValue(item.TaxValue),
                Enum.IsDefined(typeof(SignCalculationObject), item.ItemType)
                    ? (SignCalculationObject)item.ItemType
                    : null,
                Enum.IsDefined(typeof(SignMethodCalculation), item.PaymentMode)
                    ? (SignMethodCalculation)item.PaymentMode
                    : null);
            line.Price = item.Price;
            line.TaxSum = item.TaxSum;
            if (item.Department.HasValue)
                line.Department = item.Department.Value;
            if (item.ExciseAmount.HasValue)
                line.ExciseAmount = item.ExciseAmount;
            if (item.MeasureOfQuantity.HasValue && Enum.IsDefined(typeof(MeasureOfQuantity), item.MeasureOfQuantity.Value))
                line.MeasureOfQuantity = (MeasureOfQuantity)item.MeasureOfQuantity.Value;
            line.AdditionalAttribute = item.AdditionalAttribute;
            line.Marking = item.MarkingCode;
            line.ProductCode = item.ProductCode;
        }
    }

    private static TaxRate TaxRateFromValue(int value) => value switch
    {
        0 => TaxRate.Vat0,
        5 => TaxRate.Vat5,
        7 => TaxRate.Vat7,
        10 => TaxRate.Vat10,
        18 => TaxRate.Vat20,
        20 => TaxRate.Vat20,
        22 => TaxRate.Vat22,
        105 => TaxRate.Vat5_105,
        107 => TaxRate.Vat7_107,
        110 => TaxRate.Vat10_110,
        120 => TaxRate.Vat20_120,
        122 => TaxRate.Vat22_122,
        _ => TaxRate.None
    };
}
