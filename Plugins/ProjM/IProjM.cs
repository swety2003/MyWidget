using NLua;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProjM
{


    public class ProjMBase : INotifyPropertyChanged
    {
        public string script_path;

        public ProjMBase(string script_path)
        {
            this.script_path = script_path;

            //var info = env["info"] as ProjInfo;
            LoadScript();
        }

        public void LoadScript()
        {

            Lua env = new Lua();
            env.LoadCLRPackage();
            env.State.Encoding = Encoding.UTF8;

            env["item"] = this;
            env["selfp"] = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            env.DoString("import('System')");
            env.DoString("import('System.IO')");
            env.DoString("import('System.String')");
            env.DoString("import('System.Collections')");
            env.DoString("import('System.Diagnostics')");
            env.DoString("import = function () end");
            env.DoFile(script_path);

            Update = env["OnUpdate"] as LuaFunction;
            Active = env["OnActive"] as LuaFunction;
        }

        public string Icon { get; set; } = "";

        public string Name { get; set; } = "";

        public void AddInfo(string name, string path, string icon)
        {
            ProjInfos.Add(new ProjInfo(name, path, icon));
        }

        public IList<ProjInfo> ProjInfos { get; set; } = new List<ProjInfo>();

        public event PropertyChangedEventHandler? PropertyChanged;

        LuaFunction? Update;

        LuaFunction? Active;

        public void OnUpdate()
        {
            ProjInfos.Clear();
            Update?.Call();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjInfos)));
        }

        public void OnActive(ProjInfo selected)
        {
            Active?.Call(selected);

        }

        public void OpenInExplorer(ProjInfo selected)
        {
            string dir = "";
            if (Directory.Exists(selected.path)) dir = selected.path;
            
            else dir = Path.GetDirectoryName(selected.path);

            Process.Start("explorer.exe", dir);

        }
    }

    public record ProjInfo(string name, string path, string ico);

}
