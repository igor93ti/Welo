using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities.WebApp

{
    public class Movie : Entity<int>
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set;}
        

    }
}
