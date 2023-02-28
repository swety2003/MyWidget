using System;
using System.Collections.Generic;
using System.Windows;

namespace MainApp.Model
{
    public class AppConfig
    {
        public Dictionary<Guid, Card> instances = new Dictionary<Guid, Card>();


    }

    public class Card
    {
        public string Wid { get; set; }
        public Point Pos { get; set; }
        public Card(string wid, Point pos)
        {
            Wid = wid;
            Pos = pos;
        }
    }
}
