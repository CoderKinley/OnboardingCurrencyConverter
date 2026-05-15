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
        public MainWindow()
        {
            InitializeComponent();

            var client = new HttpClient();
            var currencyProvider = new CurrencyConversionProvider("9c405fd189370fb1f44b28158e27b0d8", client);
            var mockCurrencyProvider = new MockCurrencyProvider();  

            this.DataContext = new MainWindowViewModel(mockCurrencyProvider);
        }
    }
}
