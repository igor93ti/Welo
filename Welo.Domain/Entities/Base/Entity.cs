namespace Welo.Domain.Entities.Base
{
    public abstract class Entity<TIdentifier> : IEntity<TIdentifier> where TIdentifier : struct
    {
        public virtual TIdentifier Id { get; set; }
    }
}