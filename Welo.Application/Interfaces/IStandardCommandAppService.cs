using Welo.Domain.Entities;

namespace Welo.Application.Interfaces
{
    public interface IStandardCommandsAppService : IAppServiceBase<StandardCommandEntity, int>
    {
        string GetResponseMessageToTrigger(string trigger);
    }
}