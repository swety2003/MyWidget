using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDesktopCards.Model
{
    public class ConfigBase
    {
        public static T? Load<T>(string path) where T : class
        {
            try
            {

                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void Save<T>(string path, T obj)
        {
            File.WriteAllText(JsonConvert.SerializeObject(obj), path);
        }

        public void Save(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    
    }
}
