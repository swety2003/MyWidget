using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static MyDesktopCards.Model.ClassTableData;

namespace MyDesktopCards.Converters
{
    internal class ClassTime : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return null;
            }
            try
            {

                var sections = values[1] as List<sectionTime>;

                var times = values[0].ToString().Split(',');

                var first = int.Parse(times.FirstOrDefault());
                var last = int.Parse(times.LastOrDefault());

                var st = from section in sections where section.i == first select section.s;
                var et = from section in sections where section.i == last select section.e;

                var ret = $"{st.First()}\n{et.First()}";

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    internal class ClassColor : IValueConverter
    {

        private class Style
        {
            /// <summary>
            /// 
            /// </summary>
            public string color { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string background { get; set; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var style = JsonConvert.DeserializeObject<Style>(value.ToString());

            return style.color;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
