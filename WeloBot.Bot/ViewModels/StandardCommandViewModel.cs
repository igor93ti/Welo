using System;

namespace WeloBot.Bot.ViewModels
{
    [Serializable]
    public class StandardCommandViewModel
    {
        public string Trigger { get; set; }
        public string ResponseMessages { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandTypeViewModel CommandType { get; set; }
    }
}