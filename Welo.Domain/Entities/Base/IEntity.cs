namespace Welo.Domain.Entities.Base
{
    public interface IEntity<TIdentifier> where TIdentifier : struct
    {
        TIdentifier Id { get; set; }
        string VersionObject { get; }
    }
}