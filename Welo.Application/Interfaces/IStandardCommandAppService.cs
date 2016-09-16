using Welo.Domain.Entities;

namespace Welo.Application.Interfaces
{
    public interface IStandartCommandsAppService : IAppServiceBase<StandardCommandEntity, int>
    {
        string GetResponseMessageToTrigger(string trigger);
    }
}