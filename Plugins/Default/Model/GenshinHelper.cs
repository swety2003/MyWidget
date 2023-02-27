using Newtonsoft.Json;
using System.IO;

namespace Default.Model
{
    internal class GenshinHelper
    {
        public class Config
        {
            public string cookie;


            public static Config Load(string file)
            {
                if (File.Exists(file))
                {
                    var content = File.ReadAllText(file);
                    return JsonConvert.DeserializeObject<Config>(content) ?? new Config();
                }
                else
                {
                    return new Config();
                }
            }

            public void Save(string file)
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(this));
            }
        }

    }
}
