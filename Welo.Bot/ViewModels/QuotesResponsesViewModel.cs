using System;

namespace Welo.Bot.ViewModels
{
    [Serializable]
    public class QuotesResponsesViewModel
    {
        public string[] Quotes { get; set; }
        public StandardCommandViewModel Command { get; set; }
    }
}