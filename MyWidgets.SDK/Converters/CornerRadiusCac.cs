using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyWidgets.SDK.Converters
{
    internal class CornerRadiusCac : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var f = float.Parse(value.ToString());
            return new CornerRadius(f / 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
