using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SkkmConnector.Ui
{
    /// <summary>
    /// Строка таблицы позиций чека на вкладке "Чеки".
    /// Реализует INotifyPropertyChanged, чтобы колонка "Сумма" пересчитывалась при редактировании.
    /// </summary>
    public class PositionRow : INotifyPropertyChanged
    {
        private string _name = "";
        private decimal _quantity = 1;
        private string _measurementUnit = "шт";
        private decimal _price;
        private decimal _discountSum;
        private int _department;
        private string _tax = "none";

        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Quantity
        {
            get => _quantity;
            set { SetField(ref _quantity, value); OnPropertyChanged(nameof(Sum)); }
        }

        /// <summary>
        /// Единица измерения предмета расчета
        /// </summary>
        public string MeasurementUnit
        {
            get => _measurementUnit;
            set => SetField(ref _measurementUnit, value);
        }

        /// <summary>
        /// Цена позиции
        /// </summary>
        public decimal Price
        {
            get => _price;
            set { SetField(ref _price, value); OnPropertyChanged(nameof(Sum)); }
        }

        /// <summary>
        /// Сумма скидки на позицию (>0 — скидка, &lt;0 — наценка).
        /// </summary>
        public decimal DiscountSum
        {
            get => _discountSum;
            set => SetField(ref _discountSum, value);
        }

        /// <summary>
        /// Департамент/отдел
        /// </summary>
        public int Department
        {
            get => _department;
            set => SetField(ref _department, value);
        }

        /// <summary>
        /// Ставка НДС: none, 0, 10, 20, 22, 10/110, 20/120, 22/122
        /// </summary>
        public string Tax
        {
            get => _tax;
            set => SetField(ref _tax, value);
        }

        /// <summary>
        /// Итог по позиции. Цена указывается уже со скидкой (PriceWithDiscount),
        /// поэтому сумма = количество * цена; DiscountSum — справочная сумма скидки.
        /// </summary>
        public decimal Sum => Quantity * Price;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
