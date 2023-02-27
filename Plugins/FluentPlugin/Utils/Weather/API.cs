using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultWidgets.Utils.Weather
{
    public class API
    {
        public API()
        {
            Request = new Request();
        }

        Request Request = new();

        public async Task<string> GetCurrent()
        {
            string api = "https://api.msn.cn/weather/current";

            /*
                locale: zh-cn
                units: C
                appId: 9e21380c-ff19-4c78-b4ea-19558e93a5d3
                apiKey: j5i4gDqHL6nGYwx5wi5kRhXjtf2c5qgFX9fzfk0TOo
                ocid: msftweather
                wrapOData: false
                getCmaAlert: true
             */
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("locale", "zh-cn");
            data.Add("units", "C");
            data.Add("appId", "9e21380c-ff19-4c78-b4ea-19558e93a5d3");
            data.Add("ocid", "msftweather");
            data.Add("apiKey", "j5i4gDqHL6nGYwx5wi5kRhXjtf2c5qgFX9fzfk0TOo");
            data.Add("wrapOData", "false");
            data.Add("getCmaAlert", "true");

            string resp = await Request.Get(api, data);
            return resp;

        }
    }
}
