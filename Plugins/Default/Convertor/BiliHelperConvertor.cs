using System;
using System.Globalization;
using System.Windows.Data;

namespace Default.Convertor
{
    internal class BiliLevel2Img : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = value.ToString();
            string image = "";
            if (level != null)
            {
                image = $"pack://application:,,,/Default;component/Resources/BiliHelper/lv{level}.png";
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
