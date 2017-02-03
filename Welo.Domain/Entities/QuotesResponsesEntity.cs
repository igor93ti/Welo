using System;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class QuotesResponsesEntity : Entity<int>
    {
        public string[] Quotes { get; set; }
        public StandardCommandEntity Command { get; set; }
    }
}