using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ProjectManager
{
    internal class PycharmManage:IProjM
    {
        public PycharmManage()
        {
        }

        string[] baseFolder = { "D:/Source/PythonProjects", };

        public string Icon { get; set; } = "/Assets/icon/pycharm-app.ico";

        //= new BitmapImage(new Uri("/Assets/icon/vs-sln.ico",UriKind.Relative));

        public string Name { get; set; } = "Pycharm";

        public IEnumerable<ProjInfo> ProjInfos { get; set; }

        public async Task Update()
        {
            await Task.Run(() =>
            {

                List<ProjInfo> projInfos = new List<ProjInfo>();
                foreach (var item in baseFolder)
                {
                    var di = Directory.GetDirectories(item);
                    foreach (var d in di)
                    {
                        var f = Directory.GetDirectories(d);

                        if (f.Select((s) => s.ToLower() == ".idea").Count() != 0)
                        {
                            projInfos.Add(new ProjInfo(Path.GetFileName(d), d, "/Assets/icon/python-py.ico"));
                        }
                    }
                }

                ProjInfos = projInfos;
            });
        }

        public void Active(ProjInfo selected)
        {
            Process.Start("explorer.exe", selected.path);
        }

        public void SetFolders(string[] folder)
        {
            baseFolder = folder;
        }
    }
}
