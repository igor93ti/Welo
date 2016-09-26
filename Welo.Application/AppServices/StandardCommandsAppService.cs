using System;
using System.Collections.Generic;
using System.Linq;
using Chronic;
using Welo.Application.Interfaces;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandardCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandardCommandsAppService
    {
        private readonly IStandardCommandService _service;

        public StandardCommandsAppService(IStandardCommandService service) : base(service)
        {
            _service = service;
        }

        public string GetResponseMessageToTrigger(string trigger) 
            => _service.GetResponseMessageToTrigger(trigger);
    }
}