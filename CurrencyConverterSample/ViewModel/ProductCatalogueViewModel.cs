using CurrencyConverterSample.Model;
using CurrencyConverterSample.ViewModel.Base;
using System.Collections.ObjectModel;

namespace CurrencyConverterSample.ViewModel
{
    public class ProductCatalogueViewModel : ViewModelBase
    {
        private ObservableCollection<ProductCatalogueModel> _productSource = new ObservableCollection<ProductCatalogueModel>();
        private string _productNameHeader = "Product Name";
        private string _originalPriceHeader = "Original Price";
        private string _convertedPriceHeader = "Converted Price";

        public ObservableCollection<ProductCatalogueModel> ProductSource
        {
            get => _productSource;
            set => SetProperty(ref _productSource, value);
        }

        public string ProductNameHeader
        {
            get => _productNameHeader;
            set => SetProperty(ref _productNameHeader, value);
        }

        public string OriginalPriceHeader
        {
            get => _originalPriceHeader;
            set => SetProperty(ref _originalPriceHeader, value);
        }

        public string ConvertedPriceHeader
        {
            get => _convertedPriceHeader;
            set => SetProperty(ref _convertedPriceHeader, value);
        }

        public void UpdateCurrencyHeaders(string currencyCode)
        {
            OriginalPriceHeader = "Price (USD)";
            ConvertedPriceHeader = $"Price ({currencyCode})";
        }
    }
}