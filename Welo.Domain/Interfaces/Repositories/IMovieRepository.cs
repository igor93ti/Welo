using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welo.Domain.Entities.WebApp;
using Welo.Domain.Interfaces.Repositories.Base;

namespace Welo.Domain.Interfaces.Repositories
{
    public interface IMovieRepository : IRepository<Movie, int>, IRepositoryAsync<Movie, int>
    {
    
    }
}
