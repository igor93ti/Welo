using System;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandardCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandardCommandsAppService
    {
        private readonly IStandardCommandService _service;

        private static StandardCommandsAppService _instance;

        public static StandardCommandsAppService Intance
        {
            get
            {
                if (_instance == null)
                    lock (typeof(StandardCommandsAppService))
                        if (_instance == null)
                            _instance = new StandardCommandsAppService();

                return _instance;
            }
        }

        private StandardCommandsAppService() : base()
        {
            _service = new StandardCommandsService(new StandardCommandRepository(), new CommandTextGoogle(new GSheetsService()));
            Service = _service;
        }

        public string GetResponseMessageToTrigger(string trigger)
            => _service.GetResponseMessageToTrigger(trigger);
    }

    [Serializable]
    public class Lead

    {
        public string Name { get; set; }
        public string IdBot { get; set; }
        public string Channel { get; set; }
    }
}