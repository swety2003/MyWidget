using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DefaultWidgets.Convertor
{


    internal class WeatherIconConvertor : IValueConverter
    {
        Dictionary<int, string> _icons = new Dictionary<int, string>
        {
            {23, "RainShowersDayV2" },
            {26, "SnowShowersDayV2"},
            {6, "BlowingHailV2"},
            {5, "CloudyV3"},
            {20, "LightSnowV2"},
            {91, "WindyV2"},
            {27, "ThunderstormsV2"},
            {10, "FreezingRainV2"},
            {77, "RainSnowV2"},
            {12, "HazySmokeV2"},
            {39, "HazeSmokeNightV2_106"},
            {24, "RainSnowV2"},
            {78, "RainSnowShowersNightV2"},
            {9, "FogV2"},
            {3, "PartlyCloudyDayV3"},
            {43, "HailNightV2"},
            {16, "HailDayV2"},
            {8, "LightRainV2"},
            {15, "HeavySnowV2"},
            {28, "ClearNightV3"},
            {30, "PartlyCloudyNightV2"},
            {14, "ModerateRainV2"},
            {1, "SunnyDayV3"},
            {7, "BlowingSnowV2"},
            {50, "RainShowersNightV2"},
            {82, "SnowShowersNightV2"},
            {2, "MostlySunnyDay"},
            {29, "MostlyClearNight"},
            {4, "MostlyCloudyDayV2"},
            {31, "MostlyCloudyNightV2"},
            {19, "LightRainV3"},
            {17, "LightRainShowerDay"},
            {44, "LightRainShowerNight" }

            };


        Dictionary<int, int> _iconsR = new Dictionary<int, int>()
        {
            {1,1},
            {2,1},
            {3,1},
            {4,4},
            {5,4},
            {6,6},
            {7,7},
            {8,8},
            {9,9},
            {10,8},
            {11,8},
            {12,9},
            {13,8},
            {14,8},
            {15,7},
            {16,7},
            {17,8},
            {18,9},
            {19,8},
            {20,7},
            {21,9},
            {22,8},
            {23,8},
            {24,7},
            {25,7},
            {26,7},
            {27,27},
            {28,1},
            {29,1},
            {30,4},
            {31,4},
            {32,4},
            {33,6},
            {34,7},
            {35,8},
            {36,9},
            {37,8},
            {38,8},
            {39,9},
            {40,8},
            {41,8},
            {42,7},
            {43,7},
            {44,8},
            {45,9},
            {46,8},
            {47,7},
            {48,9},
            {49,8},
            {50,8},
            {51,8},
            {52,7},
            {53,7},
            {54,27},
            {57,7},
            {58,7},
            {59,7},
            {60,7},
            {61,6},
            {62,6},
            {63,9},
            {64,9},
            {65,9},
            {66,9},
            {67,27},
            {68,27},
            {69,8},
            {70,8},
            {71,8},
            {72,8},
            {73,9},
            {74,9},
            {75,8},
            {76,8},
            {77,8},
            {78,8},
            {79,8},
            {80,8},
            {81,7},
            {82,7},
            {83,8},
            {84,8},
            {85,8},
            {86,8},
            {87,6},
            {88,6},
            {89,9},
            {90,9},
            {91,6},
            {92,6},
            {93,6},
            {94,6},
            {95,9},
            {96,9},
            {101,1},
            {102,1 }
        };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var iconIndex =(int) float.Parse(value.ToString());
            var iconIndex = (int)float.Parse(value.ToString());
            if (_icons.Keys.Contains(iconIndex))
            {
                return $"https://assets.msn.cn/bundles/v1/weather/latest/{_icons[iconIndex]}.svg";

            }
            else
            {
                return $"https://assets.msn.cn/bundles/v1/weather/latest/{_icons[_iconsR[iconIndex]]}.svg";

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
