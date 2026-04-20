using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter
{
    public class CurrencyConverter : Control
    {
        static CurrencyConverter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CurrencyConverter),
                new FrameworkPropertyMetadata(typeof(CurrencyConverter))
            );
        }

        #region Property
        public string SourceCurrency 
        {
            get => (string)GetValue(SourceCurrencyProperty);
            set => SetValue(SourceCurrencyProperty, value);
        }

        public string TargetCurrency
        {
            get => (string)(GetValue(TargeCurrencyProperty));
            set => SetValue(TargeCurrencyProperty, value);
        }


        public decimal SourceValue
        {
            get => (decimal)(GetValue(SourceValueProperty));
            set => SetValue(SourceValueProperty, value);
        }

        public decimal TargetValue
        {
            get => (decimal)(GetValue(TargetValueProperty));
            set => SetValue(TargetValueProperty, value);
        }


        #endregion

        #region Dependency Property
        private static readonly DependencyProperty SourceCurrencyProperty =
            DependencyProperty.Register(
                name: nameof(SourceCurrency),
                propertyType: typeof(string),
                ownerType: typeof(CurrencyConverter),
                typeMetadata: new PropertyMetadata("USD", OnSourceCurrencyChanged)
            );


        private static readonly DependencyProperty TargeCurrencyProperty =
            DependencyProperty.Register(
                name: nameof(TargetCurrency),
                propertyType: typeof(string),
                ownerType: typeof(CurrencyConverter),       
                typeMetadata: new PropertyMetadata("EUR", OnTargetCurrencyChanged)
            );


        private static readonly DependencyProperty SourceValueProperty =
            DependencyProperty.Register(
                name: nameof(SourceValue),
                propertyType: typeof(decimal),
                ownerType: typeof(CurrencyConverter),
                typeMetadata: new PropertyMetadata(1.00m, OnSourceValueChanged)
            );

        private static readonly DependencyProperty TargetValueProperty =
            DependencyProperty.Register(
                name: nameof(TargetValue),
                propertyType: typeof(decimal),
                ownerType: typeof(CurrencyConverter),
                typeMetadata: new PropertyMetadata(1.00m, OnTargetValueChanged)
            );

        #endregion

        #region Events
        public event EventHandler? SourceCurrencyChanged;
        public event EventHandler? TargetCurrencyChanged;
        public event EventHandler? SourceValueChanged;
        public event EventHandler? TargetValueChanged;
        #endregion

        #region Callbacks
        private static void OnSourceCurrencyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).SourceCurrencyChanged?.Invoke(obj, EventArgs.Empty);
        }

        private static void OnTargetCurrencyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).TargetCurrencyChanged?.Invoke(obj, EventArgs.Empty);
        }

        private static void OnSourceValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).SourceValueChanged?.Invoke(obj, EventArgs.Empty);
        }

        private static void OnTargetValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).TargetValueChanged?.Invoke(obj, EventArgs.Empty);
        }
        #endregion
    }
}
