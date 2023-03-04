using MyDesktopCards.View;
using PluginSDK;
using System;
using System.Collections.Generic;

namespace MyDesktopCards
{
    public class PluginInfo : IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";


        public PluginInfo()
        {
        }

        public static List<CardInfo> infos { get; } = new List<CardInfo>()
        {
            //DevTest.info
            DigitalClock.info
        };



        public string name => "×ÀÃæ¿¨Æ¬";


        public List<CardInfo> GetAllCards()
        {
            return infos;
        }


        public List<SideBarItemInfo> GetAllSBItems()
        {
            return new List<SideBarItemInfo>();
        }
    }
}
