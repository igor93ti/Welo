using System;

namespace Welo.Application.Models
{
    [Serializable]
    public class InfoCommandMaskModel
    {
        public int Title { get; set; }
        public int Author { get; set; }
        public int Quote { get; set; }
        public int Link { get; set; }
    }
}