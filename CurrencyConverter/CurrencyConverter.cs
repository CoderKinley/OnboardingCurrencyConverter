using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter
{
    /// <summary>
    /// A control that facilitates currency conversion between different currency codes.
    /// </summary>
    public class CurrencyConverter : Control
    {
        #region Constructor
        static CurrencyConverter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CurrencyConverter),
                new FrameworkPropertyMetadata(typeof(CurrencyConverter))
            );
        }

        #endregion

        #region Property
        /// <summary>
        /// Gets or sets the source currency code.
        /// </summary>
        public string SourceCurrency 
        {
            get => (string)GetValue(SourceCurrencyProperty);
            set => SetValue(SourceCurrencyProperty, value);
        }

        /// <summary>
        /// Gets or sets the target currency code.
        /// </summary>
        public string TargetCurrency
        {
            get => (string)(GetValue(TargeCurrencyProperty));
            set => SetValue(TargeCurrencyProperty, value);
        }


        /// <summary>
        /// Gets or sets the source numeric value.
        /// </summary>
        public decimal SourceValue
        {
            get => (decimal)(GetValue(SourceValueProperty));
            set => SetValue(SourceValueProperty, value);
        }

        /// <summary>
        /// Gets the target numeric value.
        /// </summary>
        public decimal TargetValue
        {
            get => (decimal)(GetValue(TargetValueProperty));
            private set => SetValue(TargetValueProperty, value);    
        }

        /// <summary>
        /// Gets or sets the provider used for currency conversion.
        /// </summary>
        public ICurrencyProvider? ConversionProvider 
        {
            get => (ICurrencyProvider?)GetValue(ConversionProviderProperty);
            set => SetValue(ConversionProviderProperty, value);     
        }

        /// <summary>
        /// Gets the supported currencies list.
        /// </summary>
        public IEnumerable<CurrencyInfo> SupportedCurrencies => ConversionProvider?.SupportedCurrencies ?? new List<CurrencyInfo>();

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

        private static readonly DependencyProperty ConversionProviderProperty =
            DependencyProperty.Register(
                name: nameof(ConversionProvider),
                propertyType: typeof(ICurrencyProvider),
                ownerType: typeof(CurrencyConverter),
                typeMetadata: new PropertyMetadata(OnConversionProviderChanged)
            );
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="SourceCurrency"/> property changes.
        /// </summary>
        public event EventHandler? SourceCurrencyChanged;

        /// <summary>
        /// Occurs when the <see cref="TargetCurrency"/> property changes.
        /// </summary>
        public event EventHandler? TargetCurrencyChanged;

        /// <summary>
        /// Occurs when the <see cref="SourceValue"/> property changes.
        /// </summary>
        public event EventHandler? SourceValueChanged;

        /// <summary>
        /// Occurs when the <see cref="TargetValue"/> property changes.
        /// </summary>
        public event EventHandler? TargetValueChanged;

        /// <summary>
        /// Occurs when the <see cref="ConversionProvider"/> property changes.
        /// </summary>
        public event EventHandler? ConversionProviderChanged;
        #endregion

        #region Callbacks
        private static void OnSourceCurrencyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (CurrencyConverter)obj;
            control.SourceCurrencyChanged?.Invoke(control, EventArgs.Empty);
            _ = UpdateTargetValueAsync(control);
        }

        private static void OnTargetCurrencyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (CurrencyConverter)obj;
            control.TargetCurrencyChanged?.Invoke(control, EventArgs.Empty);
            _ = UpdateTargetValueAsync(control);
        }

        private static void OnSourceValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (CurrencyConverter)obj;
            control.SourceValueChanged?.Invoke(control, EventArgs.Empty);
            _ = UpdateTargetValueAsync(control);
        }

        private static async Task OnConversionProviderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (CurrencyConverter)obj;
            control.ConversionProviderChanged?.Invoke(control, EventArgs.Empty);

            if(args.NewValue is ICurrencyProvider cp)
            {
                _ = UpdateTargetValueAsync(control, cp);
            }
        }

        private static void OnTargetValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).TargetValueChanged?.Invoke(obj, EventArgs.Empty);
        } 

        private static async Task UpdateTargetValueAsync(CurrencyConverter control, ICurrencyProvider? currencyProvider = null)
        {
            if(control.ConversionProvider != null)
            {
                try
                {
                    control.TargetValue = await control.ConvertCurrency(control.SourceValue, control.SourceCurrency, control.TargetCurrency, currencyProvider);
                }
                catch { }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Converts a specific amount from one currency to another using the assigned provider.
        /// </summary>
        /// <param name="amount">The numeric value to convert.</param>
        /// <param name="fromCode">The source currency ISO code.</param>
        /// <param name="toCode">The target currency ISO code.</param>
        /// <returns>The converted value as a decimal.</returns>
        public async Task<decimal> ConvertCurrency(decimal amount, string fromCode, string toCode, ICurrencyProvider? currencyProvider = null )
        {
            var provider = currencyProvider ?? ConversionProvider;
            if (provider == null) return 0;

            decimal ratio = await provider.GetConversionRatio(fromCode, toCode);
            return amount * ratio;
        }
        #endregion
    }
}
