using System;
using System.Linq;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;
using Welo.Domain.Interfaces.Repositories;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services.GSheets;

namespace Welo.Domain.Services
{
    [Serializable]
    public class StandardCommandsService : ServiceBaseTEntity<StandardCommandEntity, int>, IStandardCommandService
    {
        private readonly IStandardCommandRepository _standardCommandRepository;
        private readonly ICommandTextGoogle _commandTextGoogle;

        public StandardCommandsService(IStandardCommandRepository standardCommandRepository, ICommandTextGoogle commandTextGoogle)
            : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
            _commandTextGoogle = commandTextGoogle;
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            var command = _standardCommandRepository.Find(x => x.Trigger == trigger).FirstOrDefault();
            var message = string.Empty;
            if (command != null && command.CommandType == CommandType.GoogleDocs)
            {
                message = _commandTextGoogle.GetTextRandomRowGSheets(new GSheetQuery()
                {
                    Ranges = new string[] { command.TableName }
                });
            }

            return message;
        }
    }
}