using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class LeadEntity : Entity<int>
    {
        public string ChannelConversion { get; set; }
        public string LastTriggerUsed{ get; set; }
        public DateTime LastUpdate { get; set; }
        public string ServiceURL { get; set; }
        public string FromName { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string FromId { get; set; }
        public string Activity { get; set; }
        public string Context { get; set; }
    }
}
