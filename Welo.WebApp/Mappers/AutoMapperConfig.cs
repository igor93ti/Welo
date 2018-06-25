using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Welo.Domain.Entities.WebApp;
using Welo.WebApp.Models;

namespace Welo.WebApp.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Movie, MovieModel>();
            CreateMap<MovieModel, Movie>();
        }
    }
}