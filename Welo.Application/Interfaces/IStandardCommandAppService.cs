using Welo.Domain.Entities;

namespace Welo.Application.Interfaces
{
    public interface IStandardCommandsAppService : IAppServiceBase<StandardCommandEntity, int>
    {
        CollectionOptions HelpCommand();

        ResponseTrigger RandomCommand();

        ResponseTrigger GeneralCommand(string trigger);
    }
}