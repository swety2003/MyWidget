using Microsoft.Extensions.Logging;
using MyDesktopCards.ViewModel;
using PluginSDK;
using System;
using System.Windows.Controls;

namespace MyDesktopCards.View
{
    /// <summary>
    /// DigitalClock.xaml 的交互逻辑
    /// </summary>
    public partial class DigitalClock : UserControl, ICard
    {
        internal static CardInfo info = new CardInfo(null, "数字时钟", "", typeof(DigitalClock));
        private DigitalClockVM vm;

        ILogger logger;

        public DigitalClock(Guid guid, ILoggerFactory loggerFactory)
        {
            GUID = guid;
            logger = loggerFactory.CreateLogger<DigitalClock>();
            InitializeComponent();

        }

        public int HeightPix => 5;

        public int WidthPix => 10;

        public Guid GUID { get; private set; }

        public void OnDisabled()
        {
            vm.Active = false;
        }

        public void OnEnabled()
        {
            vm = new DigitalClockVM();
            DataContext = vm;
            vm.Active = true;
        }
    }
}
