using System;
using System.Collections.Generic;
using Welo.Domain.Entities;
using Welo.Domain.Services.GSheets;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class GoogleTextCommandsAppService
    {
        private readonly ICommandTextGoogle _commandTextGoogle;

        public GoogleTextCommandsAppService(ICommandTextGoogle commandTextGoogle)
        {
            _commandTextGoogle = commandTextGoogle;
        }

        public IList<object> GetResponseMessageToTrigger(GSheetQuery query)
            => _commandTextGoogle.GetRandomRowGSheets(query);
    }
}