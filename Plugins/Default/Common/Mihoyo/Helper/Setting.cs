using DGP.Genshin.GamebarWidget.Model;
using System.Collections.Generic;

namespace DGP.Genshin.GamebarWidget.Helper
{
    internal static class Setting
    {
        public static List<Cookie> Cookies
        {
            get
            {
                return new List<Cookie>();
                //return localSettings.Values.TryGetValue("cookies", out object cookieString)
                //    ? Json.ToObject<List<Cookie>>(cookieString as string)
                //    : new List<Cookie>();
            }
            set
            {
                //localSettings.Values["cookies"] = Json.Stringify(value);
            }
        }
    }
}
