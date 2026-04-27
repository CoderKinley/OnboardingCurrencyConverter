using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using CurrencyConverterSample.Mock_Provider;
using CurrencyConverterSample.Model;
using CurrencyConverterSample.ViewModel;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace CurrencyConverterSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<ProductCatalogueModel> _productCatalogue;
        private readonly ProductCatalogueViewModel _productViewModel;
        private readonly MockCurrencyProvider _mockCurrencyProvider;

        public MainWindow()
        {
            InitializeComponent();

            _productCatalogue = new ObservableCollection<ProductCatalogueModel>
        {
            new ProductCatalogueModel { ProductName = "Laptop", ProductPrice = 1200.00m },
            new ProductCatalogueModel { ProductName = "Mouse", ProductPrice = 25.50m },
            new ProductCatalogueModel { ProductName = "Keyboard", ProductPrice = 75.00m }
        };

            _productViewModel = new ProductCatalogueViewModel
            {
                ProductSource = _productCatalogue,
                ProductNameHeader = "Inventory Item",
                OriginalPriceHeader = "Price (USD)"
            };

            ProductCatelogueCtrl.DataContext = _productViewModel;

            _mockCurrencyProvider = new MockCurrencyProvider();
            MyCurrencyControl.ConversionProvider = _mockCurrencyProvider;

            MyCurrencyControl.TargetCurrency = "EUR";

            MyCurrencyControl.TargetValueChanged += MyCurrencyControl_TargetValueChanged;
            UpdateCatalogueAsync().Wait();
        }

        private async void MyCurrencyControl_TargetValueChanged(object? sender, EventArgs e)
        {
            await UpdateCatalogueAsync();
        }

        private async Task UpdateCatalogueAsync()
        {
            if (_mockCurrencyProvider == null) return;

            string targetCurrency = MyCurrencyControl.TargetCurrency;
            string sourceCurrency = "USD";

            _productViewModel.ConvertedPriceHeader = $"Price ({targetCurrency})";

            foreach (var prod in _productCatalogue)
            {
                prod.ConvertedPrice = await MyCurrencyControl.ConvertCurrency(
                    prod.ProductPrice,
                    sourceCurrency,
                    targetCurrency
                    );
            }

        }
    }
}