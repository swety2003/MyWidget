using ChatGPT.Models;
using ChatGPT_GUI;
using ChatGPT_GUI.ViewModels;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatGPT
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl, ISideBarItem
    {
        public Config AppConfig { get; set; }

        internal static SideBarItemInfo info = new SideBarItemInfo("ChatGPT-GUI","一个简单的ChatGPT对话程序",typeof(MainView));

        MainViewModel vm;

        public MainView(Popup pop)
        {
            InitializeComponent();
            vm = new MainViewModel(this);

            DataContext = vm;
            this.Popup = pop;
        }

        public Guid GUID => throw new NotImplementedException();

        public Popup Popup { get; }

        public void OnDisabled()
        {
        }

        public void OnEnabled()
        {
            AppConfig =  ConfigBase.Load<Config>(this.GetPluginConfigFilePath())??new Config();

            vm.LoadedCommand.Execute(this);
        }

        public void ShowSetting()
        {
            new SettingDialog(new SettingViewModel(this)).ShowDialog();
        }

        private PopupView pop_view = new PopupView();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pop_view.DataContext= vm;
            this.Show(pop_view);
        }
    }
}
