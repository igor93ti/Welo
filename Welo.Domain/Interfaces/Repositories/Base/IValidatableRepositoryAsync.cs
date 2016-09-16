using Welo.Domain.Entities.Base;

namespace Welo.Domain.Interfaces.Repositories.Base
{
    public interface IValidatableRepositoryAsync<TEntity, TIdentifier> : IRepositoryAsync<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : struct
    { }
}
