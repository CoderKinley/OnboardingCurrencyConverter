using CurrencyConverterSample.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverterSample.Model
{
    public class ProductCatalogueModel : INotifyPropertyChanged
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        private decimal _convertedPrice;
        public decimal ConvertedPrice
        {
            get => _convertedPrice;
            set
            {
                _convertedPrice = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
