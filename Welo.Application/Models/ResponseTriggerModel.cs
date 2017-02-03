using System;
using System.Collections.Generic;

namespace Welo.Application.Models
{
    [Serializable]
    public class ResponseTriggerModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string Link { get; set; }

        public string MessageFormated { get; set; }
        public string Image { get; set; }
        public string Trigger { get; set; }

        public IList<ButtonOptionModel> Buttons { get; set; }
        public bool WithButtons { get; internal set; }
    }
}
