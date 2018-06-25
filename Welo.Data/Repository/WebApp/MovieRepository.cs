using Welo.Data.Repository.LiteDB;
using Welo.Domain.Entities.WebApp;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.Data.Repository.WebApp
{
    public class MovieRepository : LiteDBRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository()
        {
            base.DbContext = new CommandsContext();
        }
    }
}
