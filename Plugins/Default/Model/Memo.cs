using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace Default.Model
{
    internal class Memo
    {
        public class Config
        {
            public string text;

            public Size size { get; set; }

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
