
using PluginSDK;
using System;
using System.Collections.Generic;
using Default.View;
namespace Default
{




    public class Class1 : IPlugin
    {

        public Class1()
        {
            HandyControl.Controls.Badge b = new HandyControl.Controls.Badge();
        }
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";




        public static List<CardInfo> cards { get; } = new List<CardInfo>()
        {
            //DevTest.info
            AISchedule.info,BiliHelper.info,CountDown.info,Gallery.info,GenshinHelper.info,MsToDo.info,

        };



        public string name => "Ä¬ÈÏ²å¼þ";



        public List<CardInfo> GetAllCards()
        {
            return cards;
        }


    }
}
