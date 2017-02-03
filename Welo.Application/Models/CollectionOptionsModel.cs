using System;
using System.Collections.Generic;

namespace Welo.Application.Models
{

    [Serializable]
    public class CollectionOptionsModel
    {
        public CollectionOptionsModel()
        {
            options = new List<ResponseTriggerModel>();
        }

        public string Description { get; set; }
        public IList<ResponseTriggerModel> options { get; set; }
    }
}