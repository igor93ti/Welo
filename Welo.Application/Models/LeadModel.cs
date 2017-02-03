using System;

namespace Welo.Application.Models
{
    public class LeadModel
    {
        public string LastTriggerUsed { get; set; }
        public DateTime LastUpdate { get; set; }

        public string ServiceUrl { get; set; }

        public string BotId { get; set; }
        public string BotName { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
