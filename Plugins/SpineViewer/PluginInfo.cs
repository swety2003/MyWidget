using MyWidgets.SDK;
using SpineViewer.View;
using System;
using System.Collections.Generic;

namespace SpineViewer
{
    public class PluginInfo : IPlugin
    {
        public string name => "Spine²é¿´Æ÷";

        public Version version => new Version();

        public string url => "";

        public string author => "SwetyCore";

        public List<CardInfo> GetAllCards()
        {
            //return new List<CardInfo>
            //{
            //    View.PlayerV.info,
            //};

            return new List<CardInfo>
            {
                SpinePlayer.info,
            };
        }

        public List<SideBarItemInfo> GetAllSBItems()
        {
            throw new NotImplementedException();
        }


    }
}
