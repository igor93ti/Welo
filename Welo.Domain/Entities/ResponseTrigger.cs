using System;
using System.Collections.Generic;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class ResponseTrigger : Entity<int>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string Link { get; set; }

        public string MessageFormated { get; set; }
        public string Image { get; set; }
        public string Trigger { get; set; }

        public IList<ButtonOption> Buttons { get; set; }
        public bool WithButtons { get; internal set; }
    }
}
