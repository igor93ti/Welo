using Welo.Domain.Entities;

namespace Welo.Domain.Interfaces.Services
{
    public interface IStandardCommandService : IService<StandardCommandEntity, int>
    {
        ResponseTrigger GetResponseMessageToTrigger(string trigger);
        ResponseTrigger GetResponseMessageToTrigger(IBotCommand botCommand);
    }
}