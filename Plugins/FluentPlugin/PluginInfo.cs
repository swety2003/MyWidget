using DefaultWidgets.Widgets;
using PluginSDK;
using SharpVectors.Converters;
using System;
using System.Collections.Generic;
namespace DefaultWidgets
{
    public class Class1 : IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";


        public Class1()
        {
            Wpf.Ui.Controls.Badge badge = new Wpf.Ui.Controls.Badge();

            SvgViewbox vb = new SvgViewbox();
        }

        public static List<CardInfo> cards { get; } = new List<CardInfo>()
        {
            //DevTest.info
            BatteryInfo.info,
            DigitalClock.info,
            MediaControl.info,
            MemMonitor.info,
            Weather.info,


        };



        public string name => "Fluent²å¼þ";


        public static void Register(CardInfo t)
        {
            cards.Add(t);
        }

        public List<CardInfo> GetAllCards()
        {
            return cards;
        }

        public List<SideBarItemInfo> GetAllSBItems()
        {
            return new List<SideBarItemInfo>();
        }
    }
}
