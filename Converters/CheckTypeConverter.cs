﻿using System.Globalization;
using System.Windows.Data;

namespace CryptoViewer.Converters;

public class CheckTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Type? expectedType = parameter as Type;
        if (expectedType == null)
            return false;

        return value != null && value.GetType() == expectedType;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
