using System.ComponentModel.DataAnnotations;
using Welo.Domain.Entities.Enums;

namespace Welo.WebApp.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        [Display(Name ="Nome")]
        public string Name { get; set; }
        [Display(Name = "Ano")]
        public int Year { get; set; }
        [Display(Name = "Gênero")]
        public Genre Genre { get; set; }


    }
}