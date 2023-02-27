using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Default.Convertor
{
    internal class ClassTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            var times = value.ToString().Split(',');

            var first = int.Parse(times.FirstOrDefault());
            var last = int.Parse(times.LastOrDefault());

            var st = from section in View.AISchedule.sections where section.i == first select section.s; ;
            var et = from section in View.AISchedule.sections where section.i == last select section.e;

            var ret = $"{st.First()}\n{et.First()}";

            return ret;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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
