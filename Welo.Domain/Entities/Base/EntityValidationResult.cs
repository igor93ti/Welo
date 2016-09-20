using System;

namespace Welo.Domain.Entities.Base
{
    [Serializable]
    public class EntityValidationResult
    {
        public String Property { get; set; }

        public String Message { get; set; }

        public EntityValidationResult()
        {

        }

        public EntityValidationResult( string property, string message ) : this()
        {
            Property = property;
            Message = message;
        }
    }
}
