using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;
using Welo.Domain.Interfaces.Repositories;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services.GSheets;

namespace Welo.Domain.Services
{
    [Serializable]
    public class BotCommandsService : ServiceBaseTEntity<StandardCommandEntity, int>, IStandardCommandService
    {
        private readonly IStandardCommandRepository _standardCommandRepository;
        private readonly ICommandTextGoogle _commandTextGoogle;
        private readonly Dictionary<CommandType, Func<IBotCommand, ResponseTrigger>> _commands;

        public BotCommandsService(IStandardCommandRepository standardCommandRepository, ICommandTextGoogle commandTextGoogle)
            : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
            _commandTextGoogle = commandTextGoogle;
            _commands = new Dictionary<CommandType, Func<IBotCommand, ResponseTrigger>>
            {
                {CommandType.GoogleDocs, CommandGoogle}
            };
        }

        public ResponseTrigger GetResponseMessageToTrigger(string trigger)
        {
            var command = _standardCommandRepository.Find(x => x.Trigger == trigger).FirstOrDefault();
            return GetResponseMessageToTrigger(command);
        }

        public ResponseTrigger GetResponseMessageToTrigger(IBotCommand botCommand) 
            => botCommand != null ? _commands[botCommand.CommandType](botCommand) : null;

        private ResponseTrigger CommandGoogle(IBotCommand command)
        {
            var row = _commandTextGoogle.GetRandomRowGSheets(new GSheetQuery()
            {
                Ranges = new[] { command.TableName }
            });
            var response = command.GetMessageWithRandomQuote(row);
            response.Trigger = command.Trigger;
            return response;
        }
    }
}