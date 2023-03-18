using System;

namespace ChatGPT_GUI.Models
{
    public class ChatModel
    {
        public DateTime DateTime { get; set; }

        public string Message { get; set; }

        public ChatType Type { get; set; }
    }

    public enum ChatType
    {
        User, AI, System
    }
}
