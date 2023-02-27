using System;
using System.Globalization;
using System.Windows.Data;

namespace Default.Convertor
{
    public class TodoStatueToBool : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (Microsoft.Graph.TaskStatus)value;
            if (v != null && v == Microsoft.Graph.TaskStatus.Completed)
            {
                return true;
            }
            return false;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return Microsoft.Graph.TaskStatus.Completed;
            }
            else
            {
                return Microsoft.Graph.TaskStatus.NotStarted;
            }
        }
    }

}
