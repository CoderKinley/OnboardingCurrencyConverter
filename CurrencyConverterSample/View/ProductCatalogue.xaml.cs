using CurrencyConverterSample.Model;
using CurrencyConverterSample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverterSample.View
{
    /// <summary>
    /// Interaction logic for ProductCatalogue.xaml
    /// </summary>
    public partial class ProductCatalogue : UserControl
    {
        public ProductCatalogue()
        {
            InitializeComponent();
        }

        public string ProductNameText
        {
            get => (string)GetValue(ProductNameTextProperty);
            set => SetValue(ProductNameTextProperty, value);
        }

        public string OriginalPriceText
        {
            get => (string)GetValue(OriginalPriceTextProperty);
            set => SetValue(OriginalPriceTextProperty, value);
        }

        public string ConvertedPriceText
        {
            get => (string)GetValue(ConvertedPriceTextProperty);
            set => SetValue(ConvertedPriceTextProperty, value);
        }

        public static readonly DependencyProperty ProductNameTextProperty =
            DependencyProperty.Register(
                nameof(ProductNameText), 
                typeof(string), 
                typeof(ProductCatalogue),
                new PropertyMetadata(string.Empty, OnHeaderChanged));



        public static readonly DependencyProperty OriginalPriceTextProperty =
            DependencyProperty.Register(
                nameof(OriginalPriceText), 
                typeof(string), 
                typeof(ProductCatalogue),
                new PropertyMetadata(string.Empty, OnHeaderChanged));



        public static readonly DependencyProperty ConvertedPriceTextProperty =
            DependencyProperty.Register(
                nameof(ConvertedPriceText),
                typeof(string),
                typeof(ProductCatalogue),
                new PropertyMetadata(string.Empty, OnHeaderChanged));


        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ProductCatalogue)d;

            // Cast e.NewValue to string using (string) or e.NewValue?.ToString()
            string newHeaderValue = e.NewValue as string ?? string.Empty;

            if (e.Property == ProductNameTextProperty)
            {
                control.ProductNameHeader.Header = newHeaderValue;
            }
            else if (e.Property == OriginalPriceTextProperty)
            {
                control.OriginalPriceHeader.Header = newHeaderValue;
            }
            else if (e.Property == ConvertedPriceTextProperty)
            {
                control.ConvertedPriceHeader.Header = newHeaderValue;
            }
        }
    }
}
