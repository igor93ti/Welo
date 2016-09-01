using WeloBot.Domain.Entities;
using WeloBot.Domain.Interfaces.Repositories.Base;

namespace WeloBot.Domain.Interfaces.Repositories
{
    public interface IStandardCommandRepository : IRepository<StandardCommandEntity, int>, IRepositoryAsync<StandardCommandEntity, int>
    {
    }
}