using System;

namespace Welo.Application.Models
{
    [Serializable]
    public class QuotesResponsesViewModel
    {
        public string[] Quotes { get; set; }
        public StandardCommandModel Command { get; set; }
    }
}