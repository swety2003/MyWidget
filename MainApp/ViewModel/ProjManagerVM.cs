using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.ProjectManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModel
{
    partial class ProjManagerVM:ObservableObject
    {
        [ObservableProperty]
        IProjM selectedType;

        [ObservableProperty]
        ObservableCollection<IProjM> projMs = new ObservableCollection<IProjM>();

        public ProjManagerVM()
        {
            //var basef = new string[] { "D:\\Source\\Repos" };
            //selectedType = new SlnManage();
            //selectedType.SetFolders(basef);
            //selectedType.Update();
            projMs.Add(new PycharmManage());
            projMs.Add(new SlnManage());
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
            SelectedType?.Active(SelctedInfo);
        }

        [RelayCommand]
        void SelChange()
        {
            if (SelectedType == null)
            {
                return;
            }
            SelectedType.Update();
        }
    }
}
