using PluginSDK;
using System;
using System.Collections.Generic;

namespace ChatGPT
{
    public class PluginInfo:IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";


        public PluginInfo()
        {
        }

        public static List<SideBarItemInfo> sbis { get; } = new List<SideBarItemInfo>()
        {
            //DevTest.info
            //ProjManager.info
            MainView.info
        };



        public string name => "ChatGPT-GUI";


        public List<CardInfo> GetAllCards()
        {
            return new List<CardInfo>(); ;
        }


        public List<SideBarItemInfo> GetAllSBItems()
        {
            return sbis;
        }
    }
}
