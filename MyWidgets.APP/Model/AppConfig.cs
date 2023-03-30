using MaterialDesign3.Styles.Colors;
using MyWidgets.APP.Common;
using MyWidgets.APP.ViewModel;
using MyWidgets.SDK;
using MyWidgets.SDK.Core.Card;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyWidgets.APP.Model
{
    public class AppConfig
    {
        public Dictionary<Guid, Card> CardInstances = new Dictionary<Guid, Card>();

        public List<string> EnabledSideBarItems = new List<string>();

        public ThemeType ThemeType { get; set; } = ThemeType.Light;
    }

    public class Card
    {
        public string Wid { get; set; }
        public CardType CardType { get; }
        public Point Pos { get; set; }
        public string CanOverrideUI { get; set; }

        [JsonProperty("Locked")]
        private bool lockedProperty;

        [JsonIgnore]
        public bool Locked
        {
            get { return lockedProperty; }
            set 
            {
                lockedProperty = value;
                App.GetService<CardManageService>().SetLocked(GUID,value);
            }
        }


        [JsonProperty("Enabled")]
        public bool enabledProperty = false;

        public Guid GUID { get; set; }

        [JsonIgnore]
        public bool Enabled
        {
            get { return enabledProperty; }
            set 
            { 
                enabledProperty = value;
                if (value)
                {
                    App.GetService<CardManageService>().Enable(GUID);
                }
                else
                {
                    App.GetService<CardManageService>().Disable(GUID);
                }
            }
        }


        public Card(string wid, CardType CardType, Point pos,Guid guid)
        {
            Wid = wid;
            this.CardType = CardType;
            Pos = pos;
            GUID = guid;
        }

        [JsonIgnore]
        public CardInfo CardInfo => App.GetService<PluginLoader>().CardInfos
            .Where(x => x.MainView.FullName == Wid).Select(x=>x).First();
    }
}
