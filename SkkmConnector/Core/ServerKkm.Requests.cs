using SkkmConnector.Internal;

namespace SkkmConnector;

public sealed partial class ServerKkm
{
    /// <summary>
    /// Касса и кассир.
    /// </summary>
    private void FillBase(CheckbaseParameters check)
    {
        check.DeviceName = DeviceName;
        check.Cashier = Cashier;
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
        var check = new Correction120Parameters { CorrectionData = CorrectionData };
        FillCheck(check);
        return check;
    }

    /// <summary>
    /// Заполнение полей чека
    /// </summary>
    private void FillCheck(CheckParameters check)
    {
        FillBase(check);
        check.PaymentType = PaymentType;
        check.TaxVariant = TaxVariant;
        check.Customer = Customer;
        check.SenderEmail = SenderEmail;
        check.SaleAddress = SaleAddress;
        check.SaleLocation = SaleLocation;
        check.AgentSign = AgentSign;
        check.AgentData = Agent;
        check.Vendor = Vendor;
        check.Positions = BuildPositions();
        check.Payments = Payments;
        check.ElectronicPaymentInfo = ElectronicPayments.Count == 0 ? null : ElectronicPayments;
        check.TextBefore = TextBefore;
        check.TextAfter = TextAfter;
        check.Electronically = Electronically;
        check.OperationalAttribute = OperationalAttribute;
        check.IndustryAttribute = IndustryAttribute;
        check.UserAttribute = UserAttribute;
        check.TimeZone = TimeZone;
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
            CorrectionData = CorrectionData,
            PaymentType = PaymentType,
            TaxVariant = TaxVariant,
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
    {
        var result = new List<ApiPosition>();
        foreach (var position in Positions)
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
}
