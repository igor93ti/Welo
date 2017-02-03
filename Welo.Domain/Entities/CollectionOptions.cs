using System;
using System.Collections.Generic;

namespace Welo.Domain.Entities
{

    [Serializable]
    public class CollectionOptions
    {
        public CollectionOptions()
        {
            options = new List<ResponseTrigger>();
        }

        public string Description { get; set; }
        public IList<ResponseTrigger> options { get; set; }
    }
}