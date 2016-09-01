using WeloBot.Domain.Entities.Base;

namespace WeloBot.Domain.Interfaces.Repositories.Base
{
    public interface IValidatableRepositoryAsync<TEntity, TIdentifier> : IRepositoryAsync<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : struct
    { }
}
