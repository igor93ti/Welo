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
    public class GoogleTextCommandsAppService
    {
        private readonly ICommandTextGoogle _commandTextGoogle;

        public GoogleTextCommandsAppService(ICommandTextGoogle commandTextGoogle)
        {
            _commandTextGoogle = commandTextGoogle;
            var gSheetContext = new GSheetContext
            {
                ApplicationName = "WeloBot",
                SpreadsheetId = "1TxL93syBaHLZnrXj_Ll8eTJ0O1hCx2iwJfJdqXtZUsU",
                PathConfig = "\\\\Mac\\Home\\Documents",
                NameFile = "client_secret.json",
                User = "eugenio00"
            };
        }

        public string GetResponseMessageToTrigger(GSheetQuery query) 
            => _commandTextGoogle.GetTextRandomRowGSheets(query);
    }
}