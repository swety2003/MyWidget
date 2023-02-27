using PluginDev.View;
using PluginSDK;
using System;
using System.Collections.Generic;

namespace PluginDev
{

    public class Class1 : IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";




        public static List<CardInfo> cards { get; } = new List<CardInfo>()
        {
            DevTest.info

        };



        public string name => "Ä¬ÈÏ²å¼þ";


        public static void Register(CardInfo t)
        {
            cards.Add(t);
        }

        public List<CardInfo> GetAllCards()
        {
            return cards;
        }
    }
}
