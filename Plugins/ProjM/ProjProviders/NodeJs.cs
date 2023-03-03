using ProjM;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class NodeProjectM : IProjM
{
    static string selfp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
    public NodeProjectM()
    {

    }

    string[] baseFolder = { "D:/Source/Nodejs", };

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Icon { get; set; } = Path.Combine(selfp, "Assets/icon/node-app.ico");

    //= new BitmapImage(new Uri("/Assets/icon/vs-sln.ico",UriKind.Relative));

    public string Name { get; set; } = "NodeJS";

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
                    var f = Directory.GetFiles(d);

                    if (f.Where((s) => s.ToLower().EndsWith("package.json")).Count() != 0)
                    {
                        projInfos.Add(new ProjInfo(Path.GetFileName(d), d, Path.Combine(selfp, "Assets/icon/default.ico")));
                    }
                }
            }

            ProjInfos = projInfos;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjInfos)));
        });
    }

    public void Active(ProjInfo selected)
    {
        try
        {

            var StartInfo = new ProcessStartInfo()
            {
                FileName = "code",
                Arguments = selected.path,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            var p = new Process();
            p.StartInfo = StartInfo;

            p.Start();

        }
        catch { }
    }

    public void SetFolders(string[] folder)
    {
        baseFolder = folder;
    }

    public void OpenInExplorer(ProjInfo selected)
    {
        try
        {

            Process.Start("explorer.exe", Path.GetFullPath(selected.path));
        }
        catch { }
    }
}
