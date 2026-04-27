using CurrencyConverterSample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterSample.ViewModel
{
    public class ProductCatalogueViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductCatalogueModel> _productSource = new ObservableCollection<ProductCatalogueModel>();

        // Backing fields for headers
        private string _productNameHeader = "Product Name";
        private string _originalPriceHeader = "Original Price";
        private string _convertedPriceHeader = "Converted Price";

        public ObservableCollection<ProductCatalogueModel> ProductSource
        {
            get => _productSource;
            set { _productSource = value; OnPropertyChanged(); }
        }

        public string ProductNameHeader
        {
            get => _productNameHeader;
            set { _productNameHeader = value; OnPropertyChanged(); }
        }

        public string OriginalPriceHeader
        {
            get => _originalPriceHeader;
            set { _originalPriceHeader = value; OnPropertyChanged(); }
        }

        public string ConvertedPriceHeader
        {
            get => _convertedPriceHeader;
            set { _convertedPriceHeader = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Example method to show how to change them dynamically
        public void UpdateCurrencyHeaders(string currencyCode)
        {
            OriginalPriceHeader = "Price (USD)";
            ConvertedPriceHeader = $"Price ({currencyCode})";
        }
    }
}