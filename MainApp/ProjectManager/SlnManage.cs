using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MainApp.ProjectManager
{
    class SlnManage: IProjM
    {
        public SlnManage(string[] baseFolder)
        {
            this.baseFolder = baseFolder;
        }

        string[] baseFolder = { "D:\\Source\\Repos", };

        public string Icon { get; set; }= "/Assets/icon/vs-app.ico";
            //= new BitmapImage(new Uri("/Assets/icon/vs-sln.ico",UriKind.Relative));

        public string Name { get; set; } = "VisualStudio2022";

        public IEnumerable<ProjInfo> ProjInfos { get; set; }

        public void Update()
        {
            List<ProjInfo> projInfos = new List<ProjInfo>();
            foreach (var item in baseFolder)
            {
                var di=Directory.GetDirectories(item);
                foreach (var d in di)
                {
                    var f = Directory.GetFiles(d);

                    foreach (var i in f)
                    {
                        if (i.ToLower().EndsWith(".sln"))
                        {
                            projInfos.Add(new ProjInfo(Path.GetFileName(i),i, "Assets/icon/vs-sln.ico"));

                            break;
                        }
                    }
                }
            }

            ProjInfos = projInfos;
        }

        public void Active(ProjInfo selected)
        {
            Process.Start("explorer.exe", selected.path);
        }

    }


    public interface IProjM
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjInfo> ProjInfos { get; set; }

        public void Update();

        public void Active(ProjInfo selected);
    }

    public record ProjInfo(string name,string path,string ico);
}
