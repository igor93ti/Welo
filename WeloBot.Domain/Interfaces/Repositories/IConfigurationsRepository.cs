using WeloBot.Domain.Entities;
using WeloBot.Domain.Interfaces.Repositories.Base;

namespace WeloBot.Domain.Interfaces.Repositories
{
    public interface IConfigurationsRepository : IRepository<ConfigurationEntity, int>, IRepositoryAsync<ConfigurationEntity, int>
    {
    }
}