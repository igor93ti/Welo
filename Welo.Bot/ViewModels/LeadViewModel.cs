using System;

namespace Welo.Bot.ViewModels
{
    public class LeadViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string ChannelConversion { get; set; }
        public string LastTriggerUsed{ get; set; }
        public DateTime LastUpdate { get; set; }
        public string Name { get; set; }
    }
}
