using System.Collections.Generic;

namespace Welo.Domain.Entities.Base
{
    public interface IValidateableEntity<TIdentifier> : IEntity<TIdentifier> where TIdentifier : struct
    {
        bool IsValid();

        bool IsValid(out IEnumerable<EntityValidationResult> validationResults);

        IEnumerable<EntityValidationResult> Validate();
    }
}