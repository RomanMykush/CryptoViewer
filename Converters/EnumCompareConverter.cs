using CryptoViewer.Utils;
using System.Globalization;
using System.Windows.Data;

namespace CryptoViewer.Converters;

public class DataStateCompareConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (DataState)value == (DataState)parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}