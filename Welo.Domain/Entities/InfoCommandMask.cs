using System;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class InfoCommandMask
    {
        public int Title { get; set; }
        public int Author { get; set; }
        public int Quote { get; set; }
        public int Link { get; set; }
    }
}