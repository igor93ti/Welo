using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories.Base;

namespace Welo.Domain.Interfaces.Repositories
{
    public interface IConfigurationsRepository : IRepository<ConfigurationEntity, int>, IRepositoryAsync<ConfigurationEntity, int>
    {
    }
}