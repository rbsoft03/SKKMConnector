using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SkkmConnector.Ui;

namespace SkkmConnector;

public partial class MainWindow : Window
{
    private readonly ServerKkm _kkm = new();
    private readonly ObservableCollection<DeviceListResponse> _devices = new();
    private readonly ObservableCollection<PositionRow> _positions = new();
    private readonly ObservableCollection<ShiftOperationRow> _shiftOps = new();
    private readonly ObservableCollection<Picture> _pictures = new();
    private readonly ObservableCollection<KeyValueRow> _paramRows = new();
    private readonly ObservableCollection<KeyValueRow> _headerRows = new();

    private PositionRow? _draggedRow;
    private System.Windows.Point _dragStartPoint;
    private string? _pictureBase64;

    public MainWindow()
    {
        InitializeComponent();
        DevicesGrid.ItemsSource = _devices;
        DeviceComboBox.ItemsSource = _devices;
        PositionsGrid.ItemsSource = _positions;
        ShiftOpsGrid.ItemsSource = _shiftOps;
        PicturesGrid.ItemsSource = _pictures;
        ParamsGrid.ItemsSource = _paramRows;
        HeadersGrid.ItemsSource = _headerRows;

        // Период запроса смен по умолчанию — последняя неделя
        ShiftsFromDatePicker.SelectedDate = DateTime.Today.AddDays(-7);
        ShiftsToDatePicker.SelectedDate = DateTime.Today;

        // Статичные заголовки — видны всегда, даже до первого запроса
        FillStaticHeaders();
        Closed += (_, _) => _kkm.Dispose();
    }

    /// <summary>
    /// Заполняет таблицу заголовков постоянным набором: Content-Type, api_key, TerminalId.
    /// Значения берутся из текущих настроек доступа
    /// </summary>
    private void FillStaticHeaders()
    {
        _headerRows.Clear();
        _headerRows.Add(new KeyValueRow { Key = "Content-Type", Value = "application/json" });
        _headerRows.Add(new KeyValueRow { Key = "api_key", Value = TokenTextBox.Text.Trim() });
        _headerRows.Add(new KeyValueRow { Key = "TerminalId", Value = TerminalIdTextBox.Text.Trim() });
    }

    // Подключение: форма только заполняет коннектор и вызывает его методы

    /// <summary>
    /// Передаёт в коннектор адрес, токен, кассира и (если нужно) имя кассы.
    /// </summary>
    private bool BindConnector(bool needDevice = false)
    {
        string host = HostTextBox.Text.Trim();
        if (host.Length == 0)
        {
            ShowConsoleMessage("Укажите адрес сервера на вкладке «Настройка доступа».");
            return false;
        }

        _kkm.ServerAddress = host;
        _kkm.Token = TokenTextBox.Text.Trim();
        _kkm.TerminalId = TerminalIdTextBox.Text.Trim();
        _kkm.CashierName = CashierNameTextBox.Text.Trim();
        _kkm.CashierVatin = CashierVatinTextBox.Text.Trim();

        if (needDevice)
        {
            string deviceName = (DeviceComboBox.Text ?? "").Trim();
            if (deviceName.Length == 0)
            {
                ShowConsoleMessage("Укажите кассу в верхней панели (имя устройства на сервере ККМ).");
                return false;
            }
            _kkm.DeviceName = deviceName;
        }

        return true;
    }

    private async void PingButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector())
            return;

        PingButton.IsEnabled = false;
        try
        {
            await _kkm.Ping();
            var snapshot = CaptureConsole();

            if (!_kkm.Ok)
            {
                _devices.Clear();
                DeviceComboBox.SelectedIndex = -1;
                RenderConsole(snapshot);
                EnsureErrorDescription(_kkm.ErrorDescription, _kkm.LastStatusCode);
                return;
            }

            await _kkm.GetDeviceList();
            if (_kkm.Ok)
                FillDevices(_kkm.Devices);

            RenderConsole(snapshot);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            PingButton.IsEnabled = true;
        }
    }

    private async void GetDevicesButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector())
            return;

        GetDevicesButton.IsEnabled = false;
        try
        {
            await _kkm.GetDeviceList();
            UpdateConsole();

            _devices.Clear();
            if (_kkm.Ok)
                FillDevices(_kkm.Devices);
            else
            {
                DeviceComboBox.SelectedIndex = -1;
                EnsureErrorDescription(_kkm.ErrorDescription, _kkm.LastStatusCode);
            }
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            GetDevicesButton.IsEnabled = true;
        }
    }

    private void FillDevices(IEnumerable<DeviceListResponse> devices)
    {
        _devices.Clear();
        foreach (var device in devices)
            _devices.Add(device);

        if (_devices.Count > 0 && DeviceComboBox.SelectedIndex < 0)
            DeviceComboBox.SelectedIndex = 0;

        if (MarkingDeviceTextBox.Text.Trim().Length == 0 && DeviceComboBox.Text != null)
            MarkingDeviceTextBox.Text = DeviceComboBox.Text.Trim();
    }

    // Состояние ККМ

    private async void ConnectKktButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector())
            return;

        ConnectKktButton.IsEnabled = false;
        try
        {
            if (_devices.Count == 0)
            {
                await _kkm.GetDeviceList();
                if (!_kkm.Ok)
                {
                    UpdateConsole();
                    EnsureErrorDescription(_kkm.ErrorDescription, _kkm.LastStatusCode);
                    return;
                }

                FillDevices(_kkm.Devices);
            }

            if (!BindConnector(needDevice: true))
                return;

            await _kkm.Connect();
            var snapshot = CaptureConsole();

            if (_kkm.Ok && _kkm.Kkt != null)
            {
                ApplyDataKkt(_kkm.Kkt);
                await RefreshBacklog();
                RenderConsole(snapshot);
            }
            else
            {
                SetShiftStatusUnknown();
                RenderConsole(snapshot);
                EnsureErrorDescription(_kkm.ErrorDescription, _kkm.LastStatusCode);
            }
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
            SetShiftStatusUnknown();
        }
        finally
        {
            ConnectKktButton.IsEnabled = true;
        }
    }

    private async void GetStatusButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        GetStatusButton.IsEnabled = false;
        try
        {
            await _kkm.GetStatus();
            var snapshot = CaptureConsole();

            if (_kkm.Ok && _kkm.Status != null)
            {
                ApplyKktStatus(_kkm.Status);
                SrvLineLengthTextBox.Text = _kkm.LineLength.ToString();
                await RefreshBacklog();
            }
            else
            {
                SetShiftStatusUnknown();
            }

            RenderConsole(snapshot);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
            SetShiftStatusUnknown();
        }
        finally
        {
            GetStatusButton.IsEnabled = true;
        }
    }

    /// <summary>
    /// Запрашивает краткий статус смены и заполняет поля непереданных в ОФД документов.
    /// Ошибки не выводятся — обновление вспомогательное
    /// </summary>
    private async Task RefreshBacklog()
    {
        try
        {
            await _kkm.GetShiftStatus();
            if (_kkm.Ok)
                FillBacklog(_kkm.ShiftStatus?.Backlog);
        }
        catch
        {
            // Поля непереданных документов не обновятся — не критично
        }
    }

    /// <summary>
    /// Заполняет поля сведений о непереданных в ОФД документах в блоке "ФН"
    /// </summary>
    private void FillBacklog(Backlog? backlog)
    {
        FnBacklogCountTextBox.Text = backlog?.DocumentsCounter.ToString() ?? "—";
        FnBacklogFirstNumTextBox.Text = backlog?.DocumentFirstNumber.ToString() ?? "—";
        FnBacklogDateTextBox.Text = backlog == null || backlog.DocumentFirstDateTime == default
            ? "—"
            : backlog.DocumentFirstDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    }

    /// <summary>
    /// Заполняет верхнюю панель, датчики и предупреждения данными GET kkt/status
    /// </summary>
    private void ApplyKktStatus(KktStatus status)
    {
        FiscalCheckBox.IsChecked = status.IsFiscal;
        SetShiftStatus(status.ShiftState);

        // Показываем номер смены и последний номер чека в верхней панели
        LastOpShiftBox.Text = status.ShiftNumber > 0 ? status.ShiftNumber.ToString() : "";
        LastOpDocNumberBox.Text = status.DocNumber > 0 ? status.DocNumber.ToString() : "";

        FnPresentCheckBox.IsChecked = status.IsFnPresent;
        FnErrorCheckBox.IsChecked = status.IsFnError;

        SrvComputerTimeTextBox.Text = status.ComputerTime == default
            ? "—"
            : status.ComputerTime.ToString("dd.MM.yyyy HH:mm:ss");
        SrvDeviceTimeTextBox.Text = status.DeviceTime == default
            ? "—"
            : status.DeviceTime.ToString("dd.MM.yyyy HH:mm:ss");

        OfdTimeoutCheckBox.IsChecked = status.Warnings?.OfdTimeout ?? false;
        MemoryOverflowCheckBox.IsChecked = status.Warnings?.MemoryOverflow ?? false;
        NeedReplacementCheckBox.IsChecked = status.Warnings?.NeedReplacement ?? false;
        ResourceExhaustedCheckBox.IsChecked = status.Warnings?.ResourceExhausted ?? false;
        CriticalErrorCheckBox.IsChecked = status.Warnings?.CriticalError ?? false;

        OfdDisconnectedCheckBox.IsChecked = status.IsOfdDisconnected;
        IsmDisconnectedCheckBox.IsChecked = status.IsIsmDisconnected;
        DrawerOpenedCheckBox.IsChecked = status.IsDrawerOpened;
        CheckPaperCheckBox.IsChecked = status.IsCheckPaperPresent;
        CoverOpenedCheckBox.IsChecked = status.IsCoverOpened;
        BatteryLowCheckBox.IsChecked = status.IsBatteryLow;
        OpenDocumentCheckBox.IsChecked = status.IsOpenDocument;
    }

    /// <summary>
    /// Заполняет верхнюю панель и блоки "Сервер" / "ФН" / "Режимы и состояние ФН"
    /// данными GET kkt (данные регистрации, устройство, драйвер, состояние)
    /// </summary>
    private void ApplyDataKkt(DataKkt data)
    {
        // Блок "Сервер"
        SrvVersionTextBox.Text = data.ServerVersion ?? "";
        SrvDriverTextBox.Text = data.Driver?.Type ?? "";
        SrvDriverVersionTextBox.Text = data.Driver?.Version ?? "";
        SrvVendorTextBox.Text = data.Driver?.Vendor ?? "";
        SrvModelTextBox.Text = data.Device?.Model ?? "";
        SrvSerialTextBox.Text = data.Device?.SerialNumber ?? "";
        SrvFirmwareTextBox.Text = data.Device?.FirmwareVersion ?? "";
        SrvConfigTextBox.Text = data.Device?.ConfigurationVersion ?? "";
        SrvLineLengthTextBox.Text = (data.Device?.LineLength ?? data.Status?.LineLength ?? 0).ToString();
        SrvOrgTextBox.Text = data.Fn?.OrganizationName ?? "";
        SrvOrgVatinTextBox.Text = data.Fn?.Vatin ?? "";
        SrvAddressTextBox.Text = data.Fn?.SaleAddress ?? "";
        SrvLocationTextBox.Text = data.Fn?.SaleLocation ?? "";

        FiscalCheckBox.IsChecked = data.Device?.IsFiscal ?? data.Status?.IsFiscal ?? false;

        // Блок "ФН": данные регистрации
        FnSerialTextBox.Text = data.Fn?.SerialNumber ?? "";
        FnRnNumberTextBox.Text = data.Fn?.RnNumber ?? "";
        FnFfdTextBox.Text = data.Fn?.FfdVersion ?? "";
        FnFiscCountTextBox.Text = data.Fn?.FiscalizationsCount.ToString() ?? "";
        FnRegDateTextBox.Text = data.Fn == null || data.Fn.FiscalizationDateTime == default
            ? "—"
            : data.Fn.FiscalizationDateTime.ToString("dd.MM.yyyy");
        FnValidityTextBox.Text = data.Fn == null || data.Fn.ValidityDate == default
            ? "—"
            : data.Fn.ValidityDate.ToString("dd.MM.yyyy");
        FnFnsUrlTextBox.Text = data.Fn?.FnsUrl ?? "";
        FnTimeZoneTextBox.Text = data.Device?.TimeZone.ToString() ?? "";
        FnOfdNameTextBox.Text = data.Fn?.Ofd?.Name ?? "";
        FnOfdVatinTextBox.Text = data.Fn?.Ofd?.Vatin ?? "";

        // Блок "Режимы и состояние ФН"
        SignOfAgentTextBox.Text = data.Fn?.SignOfAgent.ToString() ?? "";
        AutomaticNumberTextBox.Text = data.Fn?.AutomaticNumber ?? "";
        DaysLeftTextBox.Text = data.Fn == null || data.Fn.ValidityDate == default
            ? "—"
            : Math.Max(0, (data.Fn.ValidityDate.Date - DateTime.Today).Days).ToString();

        var modes = data.Fn?.Modes;
        ModeOfflineCheckBox.IsChecked = modes?.OfflineMode ?? false;
        ModeEncryptionCheckBox.IsChecked = modes?.DataEncryption ?? false;
        ModeServiceCheckBox.IsChecked = modes?.ServiceSign ?? false;
        ModeBsoCheckBox.IsChecked = modes?.BsoSign ?? false;
        ModeCalcOnlineCheckBox.IsChecked = modes?.CalcOnlineSign ?? false;
        ModeExcisableCheckBox.IsChecked = modes?.SaleExcisableGoods ?? false;
        ModeGamblingCheckBox.IsChecked = modes?.SignOfGambling ?? false;
        ModeLotteryCheckBox.IsChecked = modes?.SignOfLottery ?? false;
        ModePawnshopCheckBox.IsChecked = modes?.Pawnshop ?? false;
        ModeAssuranceCheckBox.IsChecked = modes?.Assurance ?? false;
        ModeMarkingCheckBox.IsChecked = modes?.Marking ?? false;
        ModeVendingCheckBox.IsChecked = modes?.VendingMachine ?? false;
        ModeCateringCheckBox.IsChecked = modes?.CateringServices ?? false;
        ModeWholesaleCheckBox.IsChecked = modes?.WholesaleTrade ?? false;
        ModeAutomaticCheckBox.IsChecked = modes?.AutomaticMode ?? false;
        ModePrinterAutoCheckBox.IsChecked = modes?.PrinterAutomatic ?? false;

        var warnings = data.Fn?.Warnings ?? data.Status?.Warnings;
        OfdTimeoutCheckBox.IsChecked = warnings?.OfdTimeout ?? false;
        MemoryOverflowCheckBox.IsChecked = warnings?.MemoryOverflow ?? false;
        NeedReplacementCheckBox.IsChecked = warnings?.NeedReplacement ?? false;
        ResourceExhaustedCheckBox.IsChecked = warnings?.ResourceExhausted ?? false;
        CriticalErrorCheckBox.IsChecked = warnings?.CriticalError ?? false;

        // Текущее состояние
        if (data.Status != null)
            ApplyKktStatus(data.Status);
    }

    private void SetShiftStatus(ShiftState state)
    {
        // В поле кода отображаем значение enum ShiftState (1 - закрыта, 2 - открыта, 3 - истекла)
        ShiftStateNumberBox.Text = ((int)state).ToString();
        switch (state)
        {
            case ShiftState.Opened:
                ShiftStateTextBox.Text = "Открыта";
                ShiftStateTextBox.Foreground = Brushes.Green;
                break;
            case ShiftState.Closed:
                ShiftStateTextBox.Text = "Закрыта";
                ShiftStateTextBox.Foreground = Brushes.Firebrick;
                break;
            case ShiftState.Expired:
                ShiftStateTextBox.Text = "24 часа истекли";
                ShiftStateTextBox.Foreground = Brushes.DarkOrange;
                break;
            default:
                SetShiftStatusUnknown();
                break;
        }
    }

    private void SetShiftStatusUnknown()
    {
        ShiftStateNumberBox.Text = "";
        ShiftStateTextBox.Text = "";
        ShiftStateTextBox.Foreground = Brushes.Black;
    }

    // Последняя операция 

    /// <summary>
    /// Заполняет поля "Последняя операция" данными из Result фискального ответа:
    /// время, смена, номер чека, фискальный признак, DocId
    /// </summary>
    private void UpdateLastOperation()
    {
        if (!_kkm.Ok || _kkm.LastResult.ValueKind != JsonValueKind.Object)
            return;

        string? GetField(params string[] names)
        {
            foreach (var property in _kkm.LastResult.EnumerateObject())
                foreach (var name in names)
                    if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase) &&
                        property.Value.ValueKind is not (JsonValueKind.Null or JsonValueKind.Undefined))
                        return property.Value.ToString();
            return null;
        }

        string? datetime = GetField("datetime", "fiscalDatetime", "fiscalDocumentDateTime", "fiscalDate", "date");
        if (datetime != null && DateTime.TryParse(datetime, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed))
            LastOpTimeBox.Text = parsed.ToString("dd.MM.yyyy HH:mm:ss");
        else if (datetime != null)
            LastOpTimeBox.Text = datetime;

        LastOpShiftBox.Text = GetField("shiftNumber") ?? LastOpShiftBox.Text;
        LastOpDocNumberBox.Text = GetField("fiscalNumber", "checkNumber", "fiscalDocumentNumber", "docNumber") ?? LastOpDocNumberBox.Text;
        LastOpSignBox.Text = GetField("fiscalSign", "fiscalDocumentSign") ?? LastOpSignBox.Text;
        LastOpDocIdBox.Text = GetField("docId") ?? LastOpDocIdBox.Text;
    }

    // Кассовые смены

    private async void OpenShiftButton_Click(object sender, RoutedEventArgs e)
        => await RunShiftOperation(OpenShiftButton, () => _kkm.OpenShift());

    private async void CloseShiftButton_Click(object sender, RoutedEventArgs e)
        => await RunShiftOperation(CloseShiftButton, () => _kkm.CloseShift());

    private async void XReportButton_Click(object sender, RoutedEventArgs e)
        => await RunShiftOperation(XReportButton, () => _kkm.ReportX());

    private async void SettlementReportButton_Click(object sender, RoutedEventArgs e)
        => await RunShiftOperation(SettlementReportButton, () => _kkm.ReportSettlement());

    private async Task RunShiftOperation(Button button, Func<Task> operation)
    {
        if (!BindConnector(needDevice: true))
            return;

        button.IsEnabled = false;
        try
        {
            await operation();
            var snapshot = CaptureConsole();
            UpdateLastOperation();

            if (_kkm.Ok)
            {
                if (ReferenceEquals(button, OpenShiftButton))
                    SetShiftStatus(ShiftState.Opened);
                else if (ReferenceEquals(button, CloseShiftButton))
                    SetShiftStatus(ShiftState.Closed);

                await RefreshShiftOperations();
            }

            RenderConsole(snapshot);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            button.IsEnabled = true;
        }
    }

    /// <summary>
    /// Запрашивает счетчики смены и заполняет таблицу фискальных операций.
    /// Ошибки не выводятся — обновление вспомогательное
    /// </summary>
    private async Task RefreshShiftOperations()
    {
        try
        {
            await _kkm.GetTotals();
            if (_kkm.Ok)
                FillShiftOperations(_kkm.ShiftTotals);
        }
        catch
        {
            // Таблица операций не обновится — не критично
        }
    }

    /// <summary>
    /// Заполняет таблицу операций за смену. Показываются только фискальные операции
    /// с ненулевым количеством документов
    /// </summary>
    private void FillShiftOperations(ResShiftTotal? totals)
    {
        _shiftOps.Clear();
        var counters = totals?.Counters;
        if (counters == null)
            return;

        void Add(string operation, DocData? data)
        {
            if (data != null && data.Count > 0)
                _shiftOps.Add(new ShiftOperationRow { Operation = operation, Count = data.Count, Sum = data.Sum });
        }

        Add("Приход", counters.Sales);
        Add("Возврат прихода", counters.SalesReturn);
        Add("Расход", counters.Purchases);
        Add("Возврат расхода", counters.PurchasesReturn);
        Add("Коррекция прихода", counters.SalesCorrection);
        Add("Коррекция возврата прихода", counters.SalesReturnCorrection);
        Add("Коррекция расхода", counters.PurchasesCorrection);
        Add("Коррекция возврата расхода", counters.PurchasesReturnCorrection);
    }

    //  Запросы документов

    private async void ShiftStatusButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        ShiftStatusButton.IsEnabled = false;
        try
        {
            await _kkm.GetShiftStatus();
            UpdateConsole();

            if (_kkm.Ok && _kkm.ShiftStatus != null)
                SetShiftStatus(_kkm.ShiftStatus.ShiftState);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            ShiftStatusButton.IsEnabled = true;
        }
    }

    private async void OverallCountersButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        OverallCountersButton.IsEnabled = false;
        try
        {
            await _kkm.GetOverAll();
            UpdateConsole();
            if (_kkm.Ok)
                ShowConsoleMessage($"Необнуляемая сумма: {_kkm.NonZeroSum}");
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            OverallCountersButton.IsEnabled = true;
        }
    }

    private async void ShiftCountersButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        ShiftCountersButton.IsEnabled = false;
        try
        {
            await _kkm.GetTotals();
            UpdateConsole();

            if (_kkm.Ok)
                FillShiftOperations(_kkm.ShiftTotals);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            ShiftCountersButton.IsEnabled = true;
        }
    }

    private async void GetShiftsButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        if (ShiftsFromDatePicker.SelectedDate == null || ShiftsToDatePicker.SelectedDate == null)
        {
            ShowConsoleMessage("Укажите период: даты «с» и «по».");
            return;
        }

        _kkm.ShiftsFrom = ShiftsFromDatePicker.SelectedDate.Value;
        _kkm.ShiftsTo = ShiftsToDatePicker.SelectedDate.Value;

        GetShiftsButton.IsEnabled = false;
        try
        {
            await _kkm.GetShiftList();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            GetShiftsButton.IsEnabled = true;
        }
    }

    private async void GetChecksButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        if (!int.TryParse(ChecksShiftNumberTextBox.Text.Trim(), out int shiftNumber))
        {
            ShowConsoleMessage("Укажите корректный номер смены.");
            return;
        }

        _kkm.ShiftNumber = shiftNumber;

        GetChecksButton.IsEnabled = false;
        try
        {
            await _kkm.GetChecksByShift();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            GetChecksButton.IsEnabled = true;
        }
    }

    private async void GetTaskStatusButton_Click(object sender, RoutedEventArgs e)
        => await RunDocIdRequest(GetTaskStatusButton, () => _kkm.GetTaskStatus());

    private async void GetCheckByIdButton_Click(object sender, RoutedEventArgs e)
        => await RunDocIdRequest(GetCheckByIdButton, () => _kkm.GetCheck());

    private async Task RunDocIdRequest(Button button, Func<Task> operation)
    {
        if (!BindConnector())
            return;

        string docId = DocIdTextBox.Text.Trim();
        if (docId.Length == 0)
        {
            ShowConsoleMessage("Укажите идентификатор документа (DocId).");
            return;
        }

        _kkm.DocumentId = docId;

        button.IsEnabled = false;
        try
        {
            await operation();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            button.IsEnabled = true;
        }
    }

    // Чеки: шаблоны

    private void FillTemplateButton_Click(object sender, RoutedEventArgs e)
    {
        _positions.Clear();
        ClearCheckFields();

        switch (TemplateComboBox.SelectedIndex)
        {
            case 0: // Чек с полной оплатой (НДС 20)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек с полной оплатой (НДС 20)";
                TextAfterTextBox.Text = "РБ-Софт: Сервер ККМ\nhttp://www.rbsoft.ru";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 100, Tax = "20" });
                SetPayments(cash: 100);
                break;

            case 1: // Чек с полной оплатой со скидкой (Без НДС) — как в 1С-обработке
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.УСНД_Р);
                TextBeforeTextBox.Text = "Чек с полной оплатой со скидкой (Без НДС)";
                TextAfterTextBox.Text = "РБ-Софт: Сервер ККМ\nhttp://www.rbsoft.ru";
                _positions.Add(new PositionRow { Name = "Пакет", Quantity = 2, Price = 5, Department = 1, Tax = "none" });
                _positions.Add(new PositionRow { Name = "Доставка", Quantity = 3, Price = 2, DiscountSum = 2, Department = 1, Tax = "none" });
                SetPayments(cash: 16);
                break;

            case 2: // Чек безналичной оплаты (карта)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек безналичной оплаты";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 250, Tax = "20" });
                SetPayments(electronic: 250);
                break;

            case 3: // Чек со смешанной оплатой (нал + безнал)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек со смешанной оплатой";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 300, Tax = "20" });
                SetPayments(cash: 100, electronic: 200);
                break;

            case 4: // Чек с зачетом аванса (предоплата)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек с зачетом аванса";
                _positions.Add(new PositionRow { Name = "Товар по предоплате", Quantity = 1, Price = 100, Tax = "20" });
                SetPayments(advance: 100);
                break;

            case 5: // Чек в кредит (постоплата)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек в кредит";
                _positions.Add(new PositionRow { Name = "Товар в кредит", Quantity = 1, Price = 100, Tax = "20" });
                SetPayments(credit: 100);
                break;

            case 6: // Чек продажи юрлицу за наличные
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек продажи юрлицу за наличные";
                CustomerInfoTextBox.Text = "ООО \"Тестовая организация\"";
                CustomerVatinTextBox.Text = "7707083893";
                _positions.Add(new PositionRow { Name = "Товар оптом", Quantity = 10, Price = 50, Tax = "20" });
                SetPayments(cash: 500);
                break;

            case 7: // Чек физлицу с уведомлением СМС
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек с уведомлением по СМС";
                CustomerPhoneTextBox.Text = "+79990000000";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 150, Tax = "20" });
                SetPayments(electronic: 150);
                break;

            case 8: // Электронный чек (на почту, без печати)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Sale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                ElectronicallyCheckBox.IsChecked = true;
                CustomerEmailTextBox.Text = "customer@example.org";
                _positions.Add(new PositionRow { Name = "Услуга", Quantity = 1, Price = 200, Tax = "20" });
                SetPayments(electronic: 200);
                break;

            case 9: // Возврат прихода
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.SaleReturn);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Возврат прихода";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 50, Tax = "20" });
                SetPayments(cash: 50);
                break;

            case 10: // Расход (выдача средств)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.Purchase);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Расход: выдача средств";
                _positions.Add(new PositionRow { Name = "Выплата за товар", Quantity = 1, Price = 50, Tax = "none" });
                SetPayments(cash: 50);
                break;

            case 11: // Чек коррекции прихода (ФФД 1.2)
                SelectComboByTag(PaymentTypeComboBox, (int)CheckType.CorrectionSale);
                SelectComboByTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
                TextBeforeTextBox.Text = "Чек коррекции прихода";
                _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 100, Tax = "20" });
                SetPayments(cash: 100);
                // Заполняем реквизиты корректировки
                IsCorrectionCheckBox.IsChecked = true;
                SelectComboByTag(CorrectionTypeComboBox, (int)CorrectionTypes.Самостоятельно);
                CorrectionDatePicker.SelectedDate = DateTime.Today;
                CorrectionDescriptionTextBox.Text = "Коррекция расчёта";
                break;
        }

        UpdateTotal();
    }

    private void ClearCheckFields()
    {
        TextBeforeTextBox.Text = "";
        TextAfterTextBox.Text = "";
        CustomerInfoTextBox.Text = "";
        CustomerVatinTextBox.Text = "";
        CustomerEmailTextBox.Text = "";
        CustomerPhoneTextBox.Text = "";
        ElectronicallyCheckBox.IsChecked = false;
        IsCorrectionCheckBox.IsChecked = false;
        CorrectionNumberTextBox.Text = "";
        CorrectionDescriptionTextBox.Text = "";
        CorrectedCheckNumberTextBox.Text = "";
    }

    private static void SelectComboByTag(ComboBox comboBox, int value)
    {
        foreach (var item in comboBox.Items.OfType<ComboBoxItem>())
        {
            if (item.Tag is string tag && int.TryParse(tag, out int tagValue) && tagValue == value)
            {
                comboBox.SelectedItem = item;
                return;
            }
        }
    }

    private static int GetComboTag(ComboBox comboBox, int fallback = 0)
    {
        if (comboBox.SelectedItem is ComboBoxItem item && item.Tag is string tag && int.TryParse(tag, out int value))
            return value;
        return fallback;
    }

    private void SetPayments(decimal cash = 0, decimal electronic = 0, decimal credit = 0, decimal advance = 0, decimal provision = 0)
    {
        PayCashTextBox.Text = cash.ToString(CultureInfo.CurrentCulture);
        PayElectronicTextBox.Text = electronic.ToString(CultureInfo.CurrentCulture);
        PayCreditTextBox.Text = credit.ToString(CultureInfo.CurrentCulture);
        PayAdvanceTextBox.Text = advance.ToString(CultureInfo.CurrentCulture);
        PayProvisionTextBox.Text = provision.ToString(CultureInfo.CurrentCulture);
    }

    // Чеки: позиции

    private void AddPositionButton_Click(object sender, RoutedEventArgs e)
    {
        _positions.Add(new PositionRow { Name = "Товар", Quantity = 1, Price = 0, Tax = "none" });
        UpdateTotal();
    }

    private void RemovePositionButton_Click(object sender, RoutedEventArgs e)
    {
        if (PositionsGrid.SelectedItem is PositionRow row)
        {
            _positions.Remove(row);
            UpdateTotal();
        }
    }

    private void MoveUpPositionButton_Click(object sender, RoutedEventArgs e)
        => MoveSelectedPosition(-1);

    private void MoveDownPositionButton_Click(object sender, RoutedEventArgs e)
        => MoveSelectedPosition(1);

    private void MoveSelectedPosition(int delta)
    {
        int index = PositionsGrid.SelectedIndex;
        int newIndex = index + delta;
        if (index < 0 || newIndex < 0 || newIndex >= _positions.Count)
            return;

        _positions.Move(index, newIndex);
        PositionsGrid.SelectedIndex = newIndex;
    }

    private void PositionsGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Перетаскивание начинаем только с заголовка строки (левый край),
        // чтобы не мешать редактированию ячеек
        _draggedRow = null;
        if (e.OriginalSource is DependencyObject source &&
            FindAncestor<System.Windows.Controls.Primitives.DataGridRowHeader>(source) != null &&
            FindAncestor<DataGridRow>(source)?.Item is PositionRow row)
        {
            _draggedRow = row;
            _dragStartPoint = e.GetPosition(null);
        }
    }

    private void PositionsGrid_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (_draggedRow == null || e.LeftButton != MouseButtonState.Pressed)
            return;

        // Запускаем перетаскивание только после смещения на порог — иначе мешает выделению
        var pos = e.GetPosition(null);
        if (Math.Abs(pos.X - _dragStartPoint.X) < SystemParameters.MinimumHorizontalDragDistance &&
            Math.Abs(pos.Y - _dragStartPoint.Y) < SystemParameters.MinimumVerticalDragDistance)
            return;

        DragDrop.DoDragDrop(PositionsGrid, _draggedRow, DragDropEffects.Move);
        _draggedRow = null;
    }

    private void PositionsGrid_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(typeof(PositionRow)) is not PositionRow dragged)
            return;

        e.Effects = DragDropEffects.Move;

        // Живое перемещение: пока тянем строку над другой позицией, сразу
        // меняем её положение — видно, как позиции перестраиваются (как в шаблонах сервера ККМ)
        if (e.OriginalSource is DependencyObject source &&
            FindAncestor<DataGridRow>(source)?.Item is PositionRow target &&
            !ReferenceEquals(target, dragged))
        {
            int oldIndex = _positions.IndexOf(dragged);
            int newIndex = _positions.IndexOf(target);
            if (oldIndex >= 0 && newIndex >= 0 && oldIndex != newIndex)
            {
                _positions.Move(oldIndex, newIndex);
                PositionsGrid.SelectedItem = dragged;
            }
        }
        e.Handled = true;
    }

    private void PositionsGrid_Drop(object sender, DragEventArgs e)
    {
        // Позиция уже перемещена в DragOver — фиксируем выделение и пересчёт итога
        if (e.Data.GetData(typeof(PositionRow)) is PositionRow dragged)
            PositionsGrid.SelectedItem = dragged;
        UpdateTotal();
    }

    private static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
    {
        while (current != null)
        {
            if (current is T match)
                return match;
            current = VisualTreeHelper.GetParent(current);
        }
        return null;
    }

    private void PositionsGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    {
        // Пересчет итога после фиксации значения ячейки.
        // Items.Refresh() здесь вызывать нельзя — грид еще в режиме редактирования,
        // колонка "Сумма" обновляется сама через INotifyPropertyChanged.
        Dispatcher.BeginInvoke(new Action(UpdateTotal), System.Windows.Threading.DispatcherPriority.Background);
    }

    private void UpdateTotal()
    {
        decimal total = _positions.Sum(p => p.Sum);
        TotalTextBlock.Text = $"Итого: {total.ToString("F2", CultureInfo.CurrentCulture)}";
    }

    // Чеки: печать

    private async void PrintCheckButton_Click(object sender, RoutedEventArgs e)
        => await PrintCheck(isAsync: false);

    private async void PrintCheckAsyncButton_Click(object sender, RoutedEventArgs e)
        => await PrintCheck(isAsync: true);

    private async void PrintCheckCorrection120Button_Click(object sender, RoutedEventArgs e)
        => await PrintCheck(correction120: true);

    /// <summary>
    /// Максимальная сумма чека в демо-режиме (как в Сервере ККМ без лицензии)
    /// </summary>
    private const decimal DemoModeMaxCheckSum = 1000m;

    private async Task PrintCheck(bool isAsync = false, bool correction120 = false)
    {
        if (!FillCheckOnConnector())
            return;

        if (DemoModeCheckBox.IsChecked == true)
        {
            decimal total = _positions.Sum(p => p.Sum);
            if (total > DemoModeMaxCheckSum)
            {
                ShowConsoleMessage($"Демо-режим: сумма чека {total:F2} ₽ превышает лимит {DemoModeMaxCheckSum:F2} ₽. " +
                                   "Снимите флажок «Демо-режим» или уменьшите сумму.");
                return;
            }
        }

        PrintCheckButton.IsEnabled = false;
        PrintCheckAsyncButton.IsEnabled = false;
        PrintCheckCorrection120Button.IsEnabled = false;
        try
        {
            if (correction120)
                await _kkm.PrintCheckCorrection120();
            else if (isAsync)
                await _kkm.PrintCheckAsync();
            else
                await _kkm.PrintCheck();

            UpdateConsole();
            UpdateLastOperation();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            PrintCheckButton.IsEnabled = true;
            PrintCheckAsyncButton.IsEnabled = true;
            PrintCheckCorrection120Button.IsEnabled = true;
        }
    }

    /// <summary>
    /// Передаёт поля чека в коннектор (как заполнение реквизитов обработки в 1С).
    /// </summary>
    private bool FillCheckOnConnector()
    {
        if (!BindConnector(needDevice: true))
            return false;

        if (_positions.Count == 0)
        {
            ShowConsoleMessage("Добавьте хотя бы одну позицию чека.");
            return false;
        }

        _kkm.NewRequest();
        _kkm.PaymentType = GetComboTag(PaymentTypeComboBox, (int)CheckType.Sale);
        _kkm.TaxVariant = GetComboTag(TaxVariantComboBox, (int)TaxSystem.ОСН);
        _kkm.Electronically = ElectronicallyCheckBox.IsChecked == true;
        _kkm.TextBefore = TextBeforeTextBox.Text.Trim();
        _kkm.TextAfter = TextAfterTextBox.Text.Trim();
        _kkm.CustomerInfo = CustomerInfoTextBox.Text.Trim();
        _kkm.CustomerVatin = CustomerVatinTextBox.Text.Trim();
        _kkm.CustomerEmail = CustomerEmailTextBox.Text.Trim();
        _kkm.CustomerPhone = CustomerPhoneTextBox.Text.Trim();
        _kkm.PayCash = ParseDecimal(PayCashTextBox.Text);
        _kkm.PayElectronic = ParseDecimal(PayElectronicTextBox.Text);
        _kkm.PayCredit = ParseDecimal(PayCreditTextBox.Text);
        _kkm.PayAdvance = ParseDecimal(PayAdvanceTextBox.Text);
        _kkm.PayProvision = ParseDecimal(PayProvisionTextBox.Text);
        _kkm.IsCorrection = IsCorrectionCheckBox.IsChecked == true;
        _kkm.CorrectionType = (CorrectionTypes)GetComboTag(CorrectionTypeComboBox, (int)CorrectionTypes.Самостоятельно);
        _kkm.CorrectionDescription = CorrectionDescriptionTextBox.Text.Trim();
        _kkm.CorrectionDate = CorrectionDatePicker.SelectedDate ?? DateTime.Today;
        _kkm.CorrectionNumber = CorrectionNumberTextBox.Text.Trim();
        _kkm.CorrectedCheckNumber = CorrectedCheckNumberTextBox.Text.Trim();

        foreach (var p in _positions)
        {
            _kkm.AddFiscalLine(
                p.Name,
                p.Quantity,
                p.MeasurementUnit,
                p.Price,
                Math.Round(p.Sum, 2),
                p.DiscountSum,
                string.IsNullOrWhiteSpace(p.Tax) ? "none" : p.Tax.Trim(),
                0,
                p.Department);
        }

        return true;
    }

    // Картинки

    private void PictureBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Выбор картинки BMP для загрузки на сервер ККМ",
            Filter = "Изображения BMP (*.bmp)|*.bmp"
        };

        if (dialog.ShowDialog() != true)
            return;

        try
        {
            if (!string.Equals(Path.GetExtension(dialog.FileName), ".bmp", StringComparison.OrdinalIgnoreCase))
            {
                ShowConsoleMessage("Нужен файл в формате BMP.");
                return;
            }

            byte[] bytes = File.ReadAllBytes(dialog.FileName);
            if (bytes.Length < 2 || bytes[0] != (byte)'B' || bytes[1] != (byte)'M')
            {
                ShowConsoleMessage("Выбранный файл не является BMP.");
                return;
            }

            _pictureBase64 = Convert.ToBase64String(bytes);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(dialog.FileName);
            bitmap.EndInit();

            PicturePreviewImage.Source = bitmap;
            PicturePlaceholderText.Visibility = Visibility.Collapsed;
            PictureFileTextBox.Text = Path.GetFileName(dialog.FileName);

            if (PictureNameTextBox.Text.Trim().Length == 0)
                PictureNameTextBox.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Не удалось загрузить файл: {ex.Message}");
        }
    }

    private async void UploadPictureButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        if (_pictureBase64 == null)
        {
            ShowConsoleMessage("Выберите файл картинки.");
            return;
        }

        string pictureName = PictureNameTextBox.Text.Trim();
        if (pictureName.Length == 0)
        {
            ShowConsoleMessage("Укажите имя картинки на сервере.");
            return;
        }

        _kkm.PictureName = pictureName;
        _kkm.PictureBase64 = _pictureBase64;
        _kkm.PictureAlignment = 2;

        UploadPictureButton.IsEnabled = false;
        try
        {
            await _kkm.SendPicture();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            UploadPictureButton.IsEnabled = true;
        }
    }

    private async void GetPicturesButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        GetPicturesButton.IsEnabled = false;
        try
        {
            await _kkm.GetPictureList();
            UpdateConsole();

            _pictures.Clear();
            if (_kkm.Ok)
                foreach (var picture in _kkm.Pictures)
                    _pictures.Add(picture);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            GetPicturesButton.IsEnabled = true;
        }
    }

    // Маркировка

    private bool BindMarking()
    {
        if (!BindConnector())
            return false;

        string device = MarkingDeviceTextBox.Text.Trim();
        if (device.Length == 0)
            device = (DeviceComboBox.Text ?? "").Trim();
        if (device.Length == 0)
        {
            ShowConsoleMessage("Укажите кассу в верхней панели или в поле «Имя ККМ».");
            return false;
        }

        if (MarkingDeviceTextBox.Text.Trim().Length == 0)
            MarkingDeviceTextBox.Text = device;

        _kkm.DeviceName = device;
        _kkm.MarkingQuantity = ParseDecimal(MarkingQuantityTextBox.Text);
        _kkm.PlannedStatus = ParseInt(PlannedStatusTextBox.Text);
        _kkm.MeasureOfQuantity = ParseInt(MarkingMeasureTextBox.Text);
        _kkm.FractionalQuantityNumerator = ParseInt(MarkingFractionNumTextBox.Text);
        _kkm.FractionalQuantityDenominator = ParseInt(MarkingFractionDenTextBox.Text);
        _kkm.MarkingCode = MarkingCodeTextBox.Text.Trim();
        return true;
    }

    private void SetMarkingButtonsEnabled(bool enabled)
    {
        OpenKmSessionButton.IsEnabled = enabled;
        RequestKmButton.IsEnabled = enabled;
        GetKmResultButton.IsEnabled = enabled;
        CloseKmSessionButton.IsEnabled = enabled;
    }

    private async void OpenKmSessionButton_Click(object sender, RoutedEventArgs e)
        => await RunMarkingAsync(() => _kkm.OpenSessionRegistrationKM());

    private async void RequestKmButton_Click(object sender, RoutedEventArgs e)
        => await RunMarkingAsync(() => _kkm.RequestKM());

    private async void GetKmResultButton_Click(object sender, RoutedEventArgs e)
        => await RunMarkingAsync(() => _kkm.GetProcessingKMResult());

    private async void CloseKmSessionButton_Click(object sender, RoutedEventArgs e)
        => await RunMarkingAsync(() => _kkm.CloseSessionRegistrationKM());

    private async Task RunMarkingAsync(Func<Task> action)
    {
        if (!BindMarking())
            return;

        SetMarkingButtonsEnabled(false);
        try
        {
            await action();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            SetMarkingButtonsEnabled(true);
        }
    }

    // Нефискальный текст

    private void FillSlipTextButton_Click(object sender, RoutedEventArgs e)
    {
        SlipTextBox.Text = string.Join(Environment.NewLine,
            "[center,bold]РБ-Софт: Сервер ККМ",
            "[center]Нефискальный документ",
            "[line]",
            "Обычная строка текста",
            "[right]Строка справа",
            "[center,big]Крупный текст",
            "[bold]Жирная строка",
            "[line,dashed]",
            "[QR,center]https://www.rbsoft.ru",
            "[center,small]Спасибо за покупку!");
    }

    private async void PrintCopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        _kkm.FiscalSign = CopyFiscalSignTextBox.Text.Trim();

        PrintCopyButton.IsEnabled = false;
        try
        {
            await _kkm.PrintCheckCopy();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            PrintCopyButton.IsEnabled = true;
        }
    }

    private async void GetFormButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector())
            return;

        string id = CopyFiscalSignTextBox.Text.Trim();
        if (id.Length == 0)
            id = LastOpDocIdBox.Text.Trim();

        if (id.Length == 0)
        {
            ShowConsoleMessage("Укажите фискальный признак или DocId документа для получения печатной формы.");
            return;
        }

        _kkm.DocumentId = id;

        GetFormButton.IsEnabled = false;
        try
        {
            await _kkm.GetPrintForm();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            GetFormButton.IsEnabled = true;
        }
    }

    // Денежный ящик

    private async void CashInButton_Click(object sender, RoutedEventArgs e)
        => await RunCashOperation(CashInButton, () => _kkm.CashIn());

    private async void CashOutButton_Click(object sender, RoutedEventArgs e)
        => await RunCashOperation(CashOutButton, () => _kkm.CashOut());

    private async Task RunCashOperation(Button button, Func<Task> operation)
    {
        if (!BindConnector(needDevice: true))
            return;

        decimal amount = ParseDecimal(CashAmountTextBox.Text);
        if (amount <= 0)
        {
            ShowConsoleMessage("Укажите положительную сумму внесения/выемки.");
            return;
        }

        _kkm.CashAmount = amount;

        button.IsEnabled = false;
        try
        {
            await operation();
            UpdateConsole();
            UpdateLastOperation();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            button.IsEnabled = true;
        }
    }

    private async void CashBalanceButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        CashBalanceButton.IsEnabled = false;
        try
        {
            await _kkm.GetCash();
            UpdateConsole();

            if (_kkm.Ok)
                CashBalanceTextBox.Text = _kkm.CashBalance.ToString("F2", CultureInfo.CurrentCulture);
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            CashBalanceButton.IsEnabled = true;
        }
    }

    private async void OpenDrawerButton_Click(object sender, RoutedEventArgs e)
    {
        if (!BindConnector(needDevice: true))
            return;

        OpenDrawerButton.IsEnabled = false;
        try
        {
            await _kkm.OpenCashdrawer();
            UpdateConsole();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            OpenDrawerButton.IsEnabled = true;
        }
    }

    private async void PrintSlipButton_Click(object sender, RoutedEventArgs e)
        => await PrintSlip(isAsync: false);

    private async void PrintSlipAsyncButton_Click(object sender, RoutedEventArgs e)
        => await PrintSlip(isAsync: true);

    private async Task PrintSlip(bool isAsync)
    {
        if (!BindConnector(needDevice: true))
            return;

        if (SlipTextBox.Text.Trim().Length == 0)
        {
            ShowConsoleMessage("Введите текст для печати (или нажмите «Заполнить текст»).");
            return;
        }

        _kkm.TextForPrint = SlipTextBox.Text;

        PrintSlipButton.IsEnabled = false;
        PrintSlipAsyncButton.IsEnabled = false;
        try
        {
            if (isAsync)
                await _kkm.PrintSlipAsync();
            else
                await _kkm.PrintSlip();
            UpdateConsole();
            UpdateLastOperation();
        }
        catch (Exception ex)
        {
            ShowConsoleMessage($"Ошибка: {ex.Message}");
        }
        finally
        {
            PrintSlipButton.IsEnabled = true;
            PrintSlipAsyncButton.IsEnabled = true;
        }
    }

    // Вспомогательные

    private static decimal ParseDecimal(string text)
    {
        text = text.Trim();
        if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal value))
            return value;
        if (decimal.TryParse(text.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            return value;
        return 0;
    }

    private static int ParseInt(string text)
    {
        text = text.Trim();
        if (int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out int value))
            return value;
        return int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ? value : 0;
    }

    // Консоль

    /// <summary>
    /// Снимок данных запроса/ответа для отложенного вывода в консоль
    /// (когда после основной операции выполняются вспомогательные запросы)
    /// </summary>
    private sealed record ConsoleSnapshot(
        string? RequestInfo,
        string? RequestBody,
        IReadOnlyList<KeyValuePair<string, string>> RequestHeaders,
        string? ResponseBody,
        long DurationMs,
        int StatusCode);

    private ConsoleSnapshot CaptureConsole() => new(
        _kkm.LastRequestInfo,
        _kkm.LastRequestBody,
        _kkm.LastRequestHeaders,
        _kkm.LastResponseBody,
        _kkm.LastDurationMs,
        _kkm.LastStatusCode);

    /// <summary>
    /// Обновляет консоль по данным последнего вызова коннектора
    /// </summary>
    private void UpdateConsole()
    {
        if (_kkm.LastRequestInfo == null)
            return;

        RenderConsole(CaptureConsole());
    }

    private void RenderConsole(ConsoleSnapshot snapshot)
    {
        var parts = (snapshot.RequestInfo ?? "").Split(' ', 2);
        string url = parts.Length > 1 ? parts[1] : "";

        RequestMethodTextBlock.Text = parts.Length > 0 ? parts[0] : "";
        RequestUrlTextBlock.Text = url;

        string status = snapshot.StatusCode > 0 ? $"HTTP {snapshot.StatusCode}" : "нет ответа";
        RequestStatsTextBlock.Text = $"{status} · {snapshot.DurationMs} мс";

        // Вкладка "Параметры": параметры строки запроса (таблица, как в Postman)
        _paramRows.Clear();
        int queryStart = url.IndexOf('?');
        if (queryStart >= 0)
        {
            foreach (var pair in url[(queryStart + 1)..].Split('&', StringSplitOptions.RemoveEmptyEntries))
            {
                var keyValue = pair.Split('=', 2);
                _paramRows.Add(new KeyValueRow
                {
                    Key = Uri.UnescapeDataString(keyValue[0]),
                    Value = keyValue.Length > 1 ? Uri.UnescapeDataString(keyValue[1]) : ""
                });
            }
        }

        // Вкладка "Тело": JSON тела POST-запроса
        JsonHighlighter.Render(RequestBox, snapshot.RequestBody);

        // Вкладка "Заголовки" (таблица)
        _headerRows.Clear();
        foreach (var header in snapshot.RequestHeaders)
            _headerRows.Add(new KeyValueRow { Key = header.Key, Value = header.Value });

        // Код, время и описание результата (как в 1С)
        DurationTextBlock.Text = snapshot.DurationMs.ToString();
        ResultCodeTextBox.Text = "";
        ResultDescriptionTextBox.Text = "";
        if (snapshot.ResponseBody != null)
        {
            try
            {
                using var parsed = JsonDocument.Parse(snapshot.ResponseBody);
                if (parsed.RootElement.ValueKind == JsonValueKind.Object)
                {
                    if (parsed.RootElement.TryGetProperty("Code", out var code))
                        ResultCodeTextBox.Text = code.ToString();
                    if (parsed.RootElement.TryGetProperty("Description", out var description))
                        ResultDescriptionTextBox.Text = description.ToString();
                }
            }
            catch (JsonException)
            {
                // Ответ не JSON — код и описание остаются пустыми
            }
        }

        // Ответ
        JsonHighlighter.Render(ConsoleBox, snapshot.ResponseBody);
    }

    private void ShowConsoleMessage(string message)
    {
        RequestMethodTextBlock.Text = "";
        RequestUrlTextBlock.Text = "";
        RequestStatsTextBlock.Text = "";
        DurationTextBlock.Text = "";
        ResultCodeTextBox.Text = "";
        ResultDescriptionTextBox.Text = message;
        _paramRows.Clear();
        FillStaticHeaders();
        JsonHighlighter.Render(RequestBox, "");
        JsonHighlighter.Render(ConsoleBox, message);
    }

    /// <summary>
    /// Если в ответе нет Description — подставляет понятный текст ошибки (в т.ч. 401 без токена)
    /// </summary>
    private void EnsureErrorDescription(string? description, int statusCode)
    {
        if (!string.IsNullOrWhiteSpace(ResultDescriptionTextBox.Text))
            return;

        if (!string.IsNullOrWhiteSpace(description))
        {
            ResultDescriptionTextBox.Text = description;
            return;
        }

        ResultDescriptionTextBox.Text = statusCode switch
        {
            401 => "Ошибка авторизации. Укажите токен API или включите анонимный доступ REST API на сервере ККМ.",
            403 => "Доступ запрещён. Проверьте токен API.",
            > 0 => $"Ошибка HTTP {statusCode}.",
            _ => "Не удалось выполнить запрос к серверу ККМ."
        };

        if (string.IsNullOrWhiteSpace(ResultCodeTextBox.Text) && statusCode > 0)
            ResultCodeTextBox.Text = statusCode.ToString();
    }
}
