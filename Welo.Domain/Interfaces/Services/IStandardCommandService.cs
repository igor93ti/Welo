using Welo.Domain.Entities;

namespace Welo.Domain.Interfaces.Services
{
    public interface IStandardCommandService : IService<StandardCommandEntity, int>
    {
        string GetResponseMessageToTrigger(string trigger);
    }
}