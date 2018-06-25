using System;
using Welo.Domain.Entities.WebApp;
using Welo.Domain.Interfaces.Services.WebApp;
using Welo.Domain.Interfaces.Repositories.Base;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.Domain.Services.WebApp
{
    public class MovieService : ServiceBaseTEntity<Movie, int>, IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        : base(movieRepository)
        {
            _movieRepository = movieRepository;
        }
    }
}

