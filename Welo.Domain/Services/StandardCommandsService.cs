using System;
using System.Collections.Generic;
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
        private readonly Dictionary<CommandType, Func<StandardCommandEntity, string>> _commands;

        public StandardCommandsService(IStandardCommandRepository standardCommandRepository, ICommandTextGoogle commandTextGoogle)
            : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
            _commandTextGoogle = commandTextGoogle;
            _commands = new Dictionary<CommandType, Func<StandardCommandEntity, string>>
            {
                {CommandType.GoogleDocs, CommandGoogle}
            };
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            var command = _standardCommandRepository.Find(x => x.Trigger == trigger).FirstOrDefault();
            return command != null ? _commands[command.CommandType](command) : string.Empty;
        }

        private string CommandGoogle(StandardCommandEntity command)
        {
            var row = _commandTextGoogle.GetRandomRowGSheets(new GSheetQuery()
            {
                Ranges = new[] { command.TableName }
            });

            return command.GetMessageWithRandomQuote(row);
        }
    }
}