using System.Linq;
using Welo.Application.Interfaces;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;

namespace Welo.Application.AppServices
{
    public class StandartCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandartCommandsAppService
    {
        private readonly IStandardCommandService _service;

        public StandartCommandsAppService(IStandardCommandService service) : base(service)
        {
            _service = service;
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            var triggers = _service.Find(x => x.Trigger == trigger);
            var command = triggers.FirstOrDefault();
            if (command != null)
                return command.ResponseMessages;
            else
                return "Ops, esse comando não existe";
        }
    }
}