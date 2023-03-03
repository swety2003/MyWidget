using PluginSDK;
using Projm.Controls;
using System;
using System.Collections.Generic;

namespace ProjM
{
    public class Class1 : IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";


        public Class1()
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
            return new List<CardInfo> { };
        }


        List<SideBarItemInfo> IPlugin.GetAllSBItems()
        {
            return sbis;
        }
    }
}
