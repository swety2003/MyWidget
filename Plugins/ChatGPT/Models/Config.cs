using Newtonsoft.Json;
using System;
using System.IO;

namespace ChatGPT.Models
{
    public class Config : ConfigBase
    {
        public string API_Key { get; set; } = "";
    }

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
