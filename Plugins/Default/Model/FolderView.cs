using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Default.Model
{
    internal class FolderView
    {

        public class FileItem
        {

            public string Name { get; set; }

            public string Location { get; set; }

            public BitmapSource Icon { get; set; }
        }
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
