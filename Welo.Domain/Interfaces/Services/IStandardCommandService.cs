using Welo.Domain.Entities;

namespace Welo.Domain.Interfaces.Services
{
    public interface IStandardCommandService : IService<StandardCommandEntity, int>
    {
        Option GetResponseMessageToTrigger(string trigger);
        Option GetResponseMessageToTrigger(IBotCommand botCommand);
        Option GetWaitMessage(string trigger);
    }
}