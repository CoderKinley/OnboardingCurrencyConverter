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
            this.DataContext = new MainWindowViewModel(new MockCurrencyProvider());
        }
    }
}
