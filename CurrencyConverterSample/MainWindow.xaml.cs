using System.Net.Http;
using System.Windows;
using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using CurrencyConverterSample.Mock_Provider;

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
            string AccessKey = "9c405fd189370fb1f44b28158e27b0d8";

            HttpClient client = new HttpClient();
            //CurrencyConversionProvider ccp = new CurrencyConversionProvider(AccessKey, client);

            MockCurrencyProvider ccp = new MockCurrencyProvider();
            MyCurrencyControl.ConversionProvider = ccp;

        }

       
    }
}