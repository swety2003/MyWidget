using Newtonsoft.Json;
using System;
using System.IO;

namespace MyDesktopCards.Model
{
    //public class ConfigBase
    //{
    //    [JsonIgnore]
    //    public string file_path { get; set; }

    //    public static T? Load<T>(string path) where T : ConfigBase
    //    {
    //        try
    //        {
    //            var cfg = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    //            cfg.file_path = path;
    //            return cfg;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }

    //    //public static void Save<T>(string path, T obj)
    //    //{
    //    //    File.WriteAllText(JsonConvert.SerializeObject(obj), path);
    //    //}

    //    public void Save()
    //    {
    //        File.WriteAllText(file_path, JsonConvert.SerializeObject(this));
    //    }

    //}
}
