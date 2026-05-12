using CurrencyConverter.Provider;
using CurrencyConverterSample.Model;
using CurrencyConverterSample.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConverterSample.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _targetCurrency = "EUR";
        private string _sourceCurrency = "USD";
        private string _welcomeMessage = "Welcome to the Currency Converter!";
        private readonly ICurrencyProvider _currencyProvider;

        public ObservableCollection<ProductCatalogueModel> ProductCatalogue { get; }
        public ProductCatalogueViewModel ProductViewModel { get; }
        public ICurrencyProvider CurrencyProvider => _currencyProvider;

        public string TargetCurrency
        {
            get => _targetCurrency;
            set
            {
                if (SetProperty(ref _targetCurrency, value))
                {
                    _ = UpdateCatalogueAsync();
                }
            }
        }

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        // Just trying out command pattern for learning purposes
        public ICommand ChangeWelcomeCommand { get; }

        public MainWindowViewModel(ICurrencyProvider currencyProvider)
        {
            _currencyProvider = currencyProvider ?? throw new ArgumentNullException(nameof(currencyProvider));

            ProductCatalogue = new ObservableCollection<ProductCatalogueModel>
            {
                new ProductCatalogueModel { ProductName = "Laptop", ProductPrice = 1200.00m },
                new ProductCatalogueModel { ProductName = "Mouse", ProductPrice = 25.50m },
                new ProductCatalogueModel { ProductName = "Keyboard", ProductPrice = 75.00m },
                new ProductCatalogueModel { ProductName = "Monitor", ProductPrice = 350.00m },
                new ProductCatalogueModel { ProductName = "Webcam", ProductPrice = 95.00m },
                new ProductCatalogueModel { ProductName = "Headphones", ProductPrice = 150.00m },
                new ProductCatalogueModel { ProductName = "Microphone", ProductPrice = 110.00m },
                new ProductCatalogueModel { ProductName = "USB-C Hub", ProductPrice = 45.00m },
                new ProductCatalogueModel { ProductName = "Desk Lamp", ProductPrice = 60.00m },
                new ProductCatalogueModel { ProductName = "External SSD", ProductPrice = 210.00m }
            };

            ProductViewModel = new ProductCatalogueViewModel
            {
                ProductSource = ProductCatalogue,
                ProductNameHeader = "Inventory Item",
                OriginalPriceHeader = "Price (USD)"
            };

            ChangeWelcomeCommand = new RelayCommand(_ => ChangeWelcomeMessage());
            
            _ = UpdateCatalogueAsync();
        }

        private void ChangeWelcomeMessage()
        {
            WelcomeMessage = $"Message changed at {DateTime.Now:HH:mm:ss}!";
        }

        public async Task UpdateCatalogueAsync()
        {
            ProductViewModel.ConvertedPriceHeader = $"Price ({TargetCurrency})";

            var convertedRatio = await _currencyProvider.GetConversionRatio(_sourceCurrency, TargetCurrency);
            
            foreach (var prod in ProductCatalogue)
            {
                prod.ConvertedPrice = Math.Round(prod.ProductPrice * convertedRatio, 2);
            }
        }

    }
}

