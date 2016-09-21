using System;
using System.Linq;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandartCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandartCommandsAppService
    {
        private readonly IStandardCommandService _service;

        public StandartCommandsAppService() : base()
        {
            _service = new StandartCommandsService(new StandardCommandRepository());
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            GoogleService.GetMessage();
            var triggers = _service.Find(x => x.Trigger == trigger);
            var command = triggers.FirstOrDefault();
            if (command != null)
                return command.ResponseMessages;
            else
                return "Ops, esse comando não existe";
        }
    }
}