using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CurrencyConverter
{
    public class DecimalToTwoPlacesConverter : IValueConverter
    {
        // Convert: Data -> UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal d)
            {
                // "N2" formats with 2 decimal places and thousands separators
                // "F2" formats with 2 decimal places only
                return d.ToString("N2");
            }
            return value;
        }

        // ConvertBack: UI -> Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value as string, out decimal result))
            {
                return result;
            }
            return value;
        }
    }
}
