using System;
using System.Collections.Generic;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class StandardCommandEntity : Entity<int>
    {
        public string Trigger { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandType CommandType { get; set; }
        public IList<int> FormatMask { get; set; }
        public string TableName { get; set; }
        public string[] QuotesResponses { get; set; }
    }
}