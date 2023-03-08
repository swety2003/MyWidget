using Microsoft.Extensions.Logging;
using MyDesktopCards.SettingView;
using MyDesktopCards.ViewModel;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MyDesktopCards.Model.ClassTableData;

namespace MyDesktopCards.View
{
    /// <summary>
    /// AISchedule.xaml 的交互逻辑
    /// </summary>
    public partial class AISchedule : UserControl,ICard,ICanOverrideUI
    {

        private ILogger<AISchedule>_logger=> Logger.CreateLogger<AISchedule>();

        public AISchedule(Guid guid)
        {
            InitializeComponent();
            GUID=guid;
        }


        public int HeightPix =>6;

        public int WidthPix => 6;

        public Guid GUID { get; private set; }
        internal static CardInfo info = new CardInfo(null,"小爱课程表","是一个课程表捏",typeof(AISchedule));
        private AIScheduleVM vm;

        public void OnDisabled()
        {
            vm.Active = false;
        }

        public void OnEnabled()
        {

            vm = new AIScheduleVM(this);
            DataContext = vm;
            vm.Active = true;

        }

        public void ShowSetting()
        {
            new AIScheduleSetting(this).Show();
        }

        public void OverrideUI(string xaml_file_path)
        {

            DependencyObject rootElement;
            using (FileStream fs = new FileStream(xaml_file_path, FileMode.Open))
            {
                rootElement = (DependencyObject)XamlReader.Load(fs);
            }
            Content = rootElement;
            

        }
    }
}
