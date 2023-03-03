using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjM;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Projm.ViewModel
{
    partial class ProjManagerVM : ObservableObject
    {
        [ObservableProperty]
        ProjMBase selectedType;

        [ObservableProperty]
        ObservableCollection<ProjMBase> projMs = new ObservableCollection<ProjMBase>();

        public ProjManagerVM()
        {
            //projMs.Add(new PycharmManage());
            //projMs.Add(new SlnManage());

            SelfLoadedCommand = new AsyncRelayCommand(LoadProvider);
        }

        public AsyncRelayCommand SelfLoadedCommand { get; set; }


        public async Task LoadProvider()
        {
            var scriptFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Assets/scripts");
            scriptFolder = Path.GetFullPath(scriptFolder);
            if (Directory.Exists(scriptFolder))
            {
                var scripts = Directory.GetFiles(scriptFolder).Where((p) => p.ToLower().EndsWith(".lua")).ToList();
                foreach (var script in scripts)
                {
                    try
                    {
                        ProjMs.Add(new ProjMBase(script));
                    }
                    catch { }
                }

            }

        }

        [ObservableProperty]
        ProjInfo selctedInfo;

        [RelayCommand]
        void Open()
        {
            if (SelctedInfo == null)
            {
                return;
            }
            try
            {

                SelectedType?.OnActive(SelctedInfo);
            }
            catch (System.Exception)
            {


            }
        }

        [RelayCommand]
        void SelChange()
        {
            if (SelectedType == null)
            {
                return;
            }
            SelectedType.OnUpdate();
        }

        [RelayCommand]
        void OpenInExplorer()
        {
            try
            {

                SelectedType.OpenInExplorer(SelctedInfo);
            }
            catch { }


        }
    }
}
