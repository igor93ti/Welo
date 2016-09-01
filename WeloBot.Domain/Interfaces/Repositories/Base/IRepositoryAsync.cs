using System.Collections.Generic;
using System.Threading.Tasks;
using WeloBot.Domain.Entities.Base;

namespace WeloBot.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryAsync<TEntity, TIdentifier>
       where TEntity : IEntity<TIdentifier>
       where TIdentifier : struct
    {
        Task<bool> ExistsAsync(TIdentifier id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task RemoveAsync(TIdentifier id);

        Task<TEntity> GetAsync(TIdentifier id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<long> CountAsync();
    }
}