using System.Collections.Generic;

namespace DefaultWidgets.Utils.Weather
{
    public class DataType
    {
        //https://api.msn.cn/weather/current
        public class WeatherCurrent
        {
            public class Current
            {
                /// <summary>
                /// 
                /// </summary>
                public string baro { get; set; }
                /// <summary>
                /// 阴
                /// </summary>
                public string cap { get; set; }
                /// <summary>
                /// 阴
                /// </summary>
                public string capAbbr { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string daytime { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string dewPt { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string feels { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string rh { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string icon { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string pvdrIcon { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string urlIcon { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string sky { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string temp { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string vis { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string windDir { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string windSpd { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string created { get; set; }
                /// <summary>
                /// 多云
                /// </summary>
                public string pvdrCap { get; set; }
                /// <summary>
                /// 南风
                /// </summary>
                public string pvdrWindDir { get; set; }
                /// <summary>
                /// 4-5级
                /// </summary>
                public string pvdrWindSpd { get; set; }
                /// <summary>
                /// 
                /// </summary>
                //public string pm2.5 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string aqi { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string no2 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string o3 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string pm10 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string so2 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string co { get; set; }
                /// <summary>
                /// 空气良
                /// </summary>
                public string aqiSeverity { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string primaryPollutant { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string aqiValidTime { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public List<string> richCaps { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string alertCount { get; set; }
            }

            public class Provider
            {
                /// <summary>
                /// 中国天气网
                /// </summary>
                public string name { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string url { get; set; }
            }

            public class AqiProvider
            {
                /// <summary>
                /// 
                /// </summary>
                public string name { get; set; }
            }

            public class WeatherItem
            {
                /// <summary>
                /// 
                /// </summary>
                public Current current { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public Provider provider { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public AqiProvider aqiProvider { get; set; }
            }

            public class Coordinates
            {
                /// <summary>
                /// 
                /// </summary>
                public double lat { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public double lon { get; set; }
            }

            public class Location
            {
                /// <summary>
                /// 
                /// </summary>
                public string Name { get; set; }
                /// <summary>
                /// 江苏
                /// </summary>
                public string StateCode { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string CountryName { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string CountryCode { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string TimezoneName { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string TimezoneOffset { get; set; }
            }

            public class Source
            {
                /// <summary>
                /// 
                /// </summary>
                public string id { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public Coordinates coordinates { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public Location location { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string utcOffset { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string countryCode { get; set; }
            }

            public class ResponsesItem
            {
                /// <summary>
                /// 
                /// </summary>
                public List<WeatherItem> weather { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public Source source { get; set; }
            }

            public class Units
            {
                /// <summary>
                /// 
                /// </summary>
                public string system { get; set; }
                /// <summary>
                /// 百帕
                /// </summary>
                public string pressure { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string temperature { get; set; }
                /// <summary>
                /// 公里/小时
                /// </summary>
                public string speed { get; set; }
                /// <summary>
                /// 毫米
                /// </summary>
                public string height { get; set; }
                /// <summary>
                /// 公里
                /// </summary>
                public string distance { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string time { get; set; }
            }

            public class Root
            {
                /// <summary>
                /// 
                /// </summary>
                public List<ResponsesItem> responses { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public Units units { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string copyright { get; set; }
            }

        }


    }
}
