using WeloBot.Domain.Entities.Base;

namespace WeloBot.Domain.Interfaces.Repositories.Base
{
    /// <summary>
    /// Interface for base repository with auto entity validation
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TIdentifier">Type of entity identifier</typeparam>
    public interface IValidatableRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : IValidateableEntity<TIdentifier>
        where TIdentifier : struct
    {

    }

}
