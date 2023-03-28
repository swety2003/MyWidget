using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWidgets.Updater.Utils
{
    public static class FileSizeHelper
    {

        public static string AutoUnit(double numByte)
        {
            // kb
            numByte /= 1024;
            string unit = "KiB";
            if (numByte >= 1024)
            {
                unit = "MiB";
                numByte /= 1024;
            }

            return $"{Math.Round(numByte, 2)}{unit}";
        }
    }
}
