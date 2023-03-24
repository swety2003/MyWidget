using MaterialDesign3.Styles.Colors;
using MyWidgets.SDK;
using System;
using System.Collections.Generic;
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
        public bool Locked { get; set; }

        public Card(string wid, CardType CardType, Point pos)
        {
            Wid = wid;
            this.CardType = CardType;
            Pos = pos;
        }
    }
}
