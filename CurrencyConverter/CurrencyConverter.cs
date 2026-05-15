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
            get => (string)(GetValue(TargetCurrencyProperty));
            set => SetValue(TargetCurrencyProperty, value);
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
        public IEnumerable<CurrencyInfo> SupportedCurrencies
        {
            get => (IEnumerable<CurrencyInfo>)GetValue(SupportedCurrenciesProperty);
            private set => SetValue(SupportedCurrenciesProperty, value);
        }

        /// <summary>
        /// Gets or sets the flag of the map
        /// </summary>
        public string SupportedFlag
        {
            get => (string)GetValue(SupportedFlagProperty);
            set => SetValue(SupportedFlagProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the source amount TextBox.
        /// </summary>
        public Style SourceAmountStyle
        {
            get => (Style)GetValue(SourceAmountStyleProperty);
            set => SetValue(SourceAmountStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the target amount TextBox.
        /// </summary>
        public Style TargetAmountStyle
        {
            get => (Style)GetValue(TargetAmountStyleProperty);
            set => SetValue(TargetAmountStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the source currency ComboBox.
        /// </summary>
        public Style SourceCurrencyStyle
        {
            get => (Style)GetValue(SourceCurrencyStyleProperty);
            set => SetValue(SourceCurrencyStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the target currency ComboBox.
        /// </summary>
        public Style TargetCurrencyStyle
        {
            get => (Style)GetValue(TargetCurrencyStyleProperty);
            set => SetValue(TargetCurrencyStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the source amount container Border.
        /// </summary>
        public Style SourceAmountBorderStyle
        {
            get => (Style)GetValue(SourceAmountBorderStyleProperty);
            set => SetValue(SourceAmountBorderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the target amount container Border.
        /// </summary>
        public Style TargetAmountBorderStyle
        {
            get => (Style)GetValue(TargetAmountBorderStyleProperty);
            set => SetValue(TargetAmountBorderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the source currency container Border.
        /// </summary>
        public Style SourceCurrencyBorderStyle
        {
            get => (Style)GetValue(SourceCurrencyBorderStyleProperty);
            set => SetValue(SourceCurrencyBorderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the target currency container Border.
        /// </summary>
        public Style TargetCurrencyBorderStyle
        {
            get => (Style)GetValue(TargetCurrencyBorderStyleProperty);
            set => SetValue(TargetCurrencyBorderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the main outer Border.
        /// </summary>
        public Style MainBorderStyle
        {
            get => (Style)GetValue(MainBorderStyleProperty);
            set => SetValue(MainBorderStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the template for currency items in the ComboBox.
        /// </summary>
        public DataTemplate CurrencyItemTemplate
        {
            get => (DataTemplate)GetValue(CurrencyItemTemplateProperty);
            set => SetValue(CurrencyItemTemplateProperty, value);
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


        private static readonly DependencyProperty TargetCurrencyProperty =
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
                typeMetadata: new PropertyMetadata(null, OnConversionProviderChanged)
            );


        public static readonly DependencyProperty SupportedCurrenciesProperty =
            DependencyProperty.Register(
                nameof(SupportedCurrencies),
                typeof(IEnumerable<CurrencyInfo>),
                typeof(CurrencyConverter),
                new PropertyMetadata(new List<CurrencyInfo>()));

        public static readonly DependencyProperty SupportedFlagProperty =
            DependencyProperty.Register(
                nameof(SupportedFlag),
                typeof(string),
                typeof(CurrencyConverter),
                new PropertyMetadata(null)
                );

        public static readonly DependencyProperty SourceAmountStyleProperty =
            DependencyProperty.Register(
                nameof(SourceAmountStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty TargetAmountStyleProperty =
            DependencyProperty.Register(
                nameof(TargetAmountStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty SourceCurrencyStyleProperty =
            DependencyProperty.Register(
                nameof(SourceCurrencyStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty TargetCurrencyStyleProperty =
            DependencyProperty.Register(
                nameof(TargetCurrencyStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty SourceAmountBorderStyleProperty =
            DependencyProperty.Register(
                nameof(SourceAmountBorderStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty TargetAmountBorderStyleProperty =
            DependencyProperty.Register(
                nameof(TargetAmountBorderStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty SourceCurrencyBorderStyleProperty =
            DependencyProperty.Register(
                nameof(SourceCurrencyBorderStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty TargetCurrencyBorderStyleProperty =
            DependencyProperty.Register(
                nameof(TargetCurrencyBorderStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty MainBorderStyleProperty =
            DependencyProperty.Register(
                nameof(MainBorderStyle), 
                typeof(Style), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty CurrencyItemTemplateProperty =
            DependencyProperty.Register(
                nameof(CurrencyItemTemplate), 
                typeof(DataTemplate), 
                typeof(CurrencyConverter), 
                new PropertyMetadata(null));
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

        /// <summary>
        /// Occurs when the <see cref="SupportedFlagProperty"/> property changes.
        /// </summary>
        public event EventHandler? SupportedFlagChanged;
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

        private static async void OnConversionProviderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (CurrencyConverter)obj;
            control.ConversionProviderChanged?.Invoke(control, EventArgs.Empty);

            if (args.NewValue is ICurrencyProvider cp)
            {
                // chaget currencies
                control.SupportedCurrencies = cp.SupportedCurrencies;

                _ = UpdateTargetValueAsync(control, cp);
            }

        }

        private static void OnTargetValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CurrencyConverter)obj).TargetValueChanged?.Invoke(obj, EventArgs.Empty);
        }

        private static async Task UpdateTargetValueAsync(CurrencyConverter control, ICurrencyProvider? currencyProvider = null)
        {
            if (control.ConversionProvider != null)
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
        public async Task<decimal> ConvertCurrency(decimal amount, string fromCode, string toCode, ICurrencyProvider? currencyProvider = null)
        {
            var provider = currencyProvider ?? ConversionProvider;
            if (provider == null) return 0;

            decimal ratio = await provider.GetConversionRatio(fromCode, toCode);
            return amount * ratio;
        }
        #endregion
    }
}
