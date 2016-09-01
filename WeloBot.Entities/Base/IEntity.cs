namespace WeloBot.Entities.Base
{
    public interface IEntity<TIdentifier> where TIdentifier : struct
    {
        TIdentifier Id { get; set; }
    }
}