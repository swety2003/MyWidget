using CommunityToolkit.Mvvm.ComponentModel;
using MyWidgets.APP.View;
using MyWidgets.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWidgets.APP.ViewModel
{
    internal partial class SideBarVM:ObservableObject
    {
        [ObservableProperty]
        UIElement itemContent;

        public SideBarVM()
        {

            SetAction();
        }



        public void SetAction()
        {
            ISideBarItemExt.SetMethod(new ShowCard(Show));
        }

        private void Show(UIElement view)
        {
            ItemContent = view;
        }
    }
}
