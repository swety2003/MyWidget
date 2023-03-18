using Microsoft.Extensions.Logging;
using MyDesktopCards.Common;
using MyDesktopCards.Model;
using MyDesktopCards.SettingView;
using MyDesktopCards.ViewModel;
using PluginSDK;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyDesktopCards.View
{
    /// <summary>
    /// AISchedule.xaml 的交互逻辑
    /// </summary>
    public partial class AISchedule : UserControl, ICard
    {

        public CardInfo Info => info;

        Model.AIScheduleConfig Config { get; set; }

        private ILogger<AISchedule> _logger => Logger.CreateLogger<AISchedule>();

        public AISchedule(Guid guid)
        {
            InitializeComponent();
            GUID = guid;
        }


        public int HeightPix => 6;

        public int WidthPix => 6;

        public Guid GUID { get; private set; }
        internal static CardInfo info = new CardInfo(null, "小爱课程表", "是一个课程表捏", typeof(AISchedule));
        private AIScheduleVM vm;

        public void OnDisabled()
        {
            vm.Active = false;
        }

        public void OnEnabled()
        {
            ConfigBase.Load<AIScheduleConfig>(this.GetPluginConfigFilePath());

            vm = new AIScheduleVM(this);
            DataContext = vm;
            vm.Active = true;
            this.TryLoadCustomeStyle();

        }

        public void ShowSetting()
        {
            new AIScheduleSetting(this).Show();
        }


        public UIElement GetUIElement()
        {
            return this as UIElement;
        }
    }
}
