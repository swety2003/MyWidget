using Newtonsoft.Json;
using System;
using System.IO;

namespace Default.Model
{
    internal class CountDown
    {
        public class Config
        {
            public DateTime Date { get; set; }
            public string Event { get; set; }

            public static Config Load(string file)
            {
                if (File.Exists(file))
                {
                    var content = File.ReadAllText(file);
                    return JsonConvert.DeserializeObject<Config>(content) ?? new Config() { Date = DateTime.Now, Event = "事件" };
                }
                else
                {
                    return new Config() { Date = DateTime.Now, Event = "事件" };
                }
            }

            public void Save(string file)
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(this));
            }
        }

    }
}
