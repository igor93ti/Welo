using System;
using System.Collections.Generic;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{

    [Serializable]
    public class CollectionOptions
    {
        public CollectionOptions()
        {
            options = new List<Option>();
        }

        public string Description { get; set; }
        public IList<Option> options { get; set; }
    }
}