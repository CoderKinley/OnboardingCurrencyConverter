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
        
        #endregion

        #region Dependency Property
        private static readonly DependencyProperty SourceCurrencyProperty =
            DependencyProperty.Register(
                name: nameof(SourceCurrency),
                propertyType: typeof(bool),
                ownerType: typeof(CurrencyConverter),
                typeMetadata: new PropertyMetadata("USD", OnSourceCurrencyChanged)
            );
        #endregion

        #region Events
        public event EventHandler? SourceCurrencyChanged;
        #endregion

        #region Callbacks
        private static void OnSourceCurrencyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).SourceCurrencyChanged?.Invoke(obj, EventArgs.Empty);
        }
        #endregion
    }
}
