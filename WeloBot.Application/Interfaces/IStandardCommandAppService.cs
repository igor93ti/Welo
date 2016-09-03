using WeloBot.Domain.Entities;

namespace WeloBot.Application.Interfaces
{
    public interface IStandartCommandsAppService : IAppServiceBase<StandardCommandEntity, int>
    {
        string GetResponseMessageToTrigger(string trigger);
    }
}