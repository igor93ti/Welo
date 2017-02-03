using System;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class LeadEntity : Entity<int>
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
