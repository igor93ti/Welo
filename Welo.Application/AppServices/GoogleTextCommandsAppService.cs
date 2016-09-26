using System;
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

        public string GetResponseMessageToTrigger(GSheetQuery query)
            => _commandTextGoogle.GetTextRandomRowGSheets(query);
    }
}