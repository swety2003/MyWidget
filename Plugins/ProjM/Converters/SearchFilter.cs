using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ProjM.Converters
{
    internal class SearchFilter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return null;
            }
            string word = values[1] as string ?? "";
            IEnumerable<ProjInfo>? infos = values[0] as IEnumerable<ProjInfo>;
            return infos?.Where((x) => x.name.ToLower().Contains(word));
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
