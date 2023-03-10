using PluginSDK;
using Projm.Controls;
using System;
using System.Collections.Generic;

namespace ProjM
{
    public class PluginInfo : IPlugin
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
            ProjManager.info

        };



        public string name => "项目管理器";


        public List<CardInfo> GetAllCards()
        {
            return new List<CardInfo>(); ;
        }


        public List<SideBarItemInfo> GetAllSBItems()
        {
            return sbis;
        }

        public List<CardInfo> GetAllWindows()
        {
            throw new NotImplementedException();
        }
    }
}
