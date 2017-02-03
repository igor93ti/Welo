using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories.Base;

namespace Welo.Domain.Interfaces.Repositories
{
    public interface IStandardCommandRepository : IRepository<StandardCommandEntity, int>, IRepositoryAsync<StandardCommandEntity, int>
    {
    }
}