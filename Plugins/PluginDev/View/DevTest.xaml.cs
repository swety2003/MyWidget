using PluginSDK;
using System;
using System.Windows.Controls;

namespace PluginDev.View
{
    /// <summary>
    /// DevTest.xaml 的交互逻辑
    /// </summary>
    public partial class DevTest : UserControl, ICard
    {

        public int HeightPix => 2;

        public int WidthPix => 2;

        public Guid GUID { get; private set; }

        public static CardInfo info = new CardInfo(null, "测试卡片", "描述", typeof(DevTest));

        public DevTest(Guid guid)
        {
            InitializeComponent();

            GUID = guid;
        }


        public CardInfo GetInfo()
        {
            return info;
        }

        public void OnDisabled()
        {
            //throw new NotImplementedException();
        }

        public void OnEnabled()
        {
            //throw new NotImplementedException();
        }

        //public static CardInfo Info => throw new NotImplementedException();

        //public static ImageSource icon => null;
        //public static string name => "测试1";
        //public static string description => "测试描述";
        //public static Type settingPage => typeof(DevTest);
    }
}
