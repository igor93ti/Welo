using WeloBot.Domain.Entities;

namespace WeloBot.Application.Interfaces
{
    public interface IStandardCommandAppService : IAppServiceBase<StandardCommandEntity, int>
    {
        string GetResponseMessageToTrigger(string trigger);
    }
}