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

        public StandardCommandsService(IStandardCommandRepository standardCommandRepository, ICommandTextGoogle commandTextGoogle)
            : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
            _commandTextGoogle = commandTextGoogle;
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            var command = _standardCommandRepository.Find(x => x.Trigger == trigger).FirstOrDefault();
            IList<object> row = null;
            if (command != null && command.CommandType == CommandType.GoogleDocs)
            {
                row = _commandTextGoogle.GetRandomRowGSheets(new GSheetQuery()
                {
                    Ranges = new string[] {command.TableName}
                });
            }
            else
                return string.Empty;

            return GetMessage(row, command.FormatMask);
        }

        private static string GetMessage(IList<object> row, IList<int> formarMask)
        {
            if (row == null)
                return string.Empty;

            var text = string.Empty;
            foreach (var index in formarMask)
            {
                text += row.ElementAt(index).ToString();
                if (index != formarMask.Last())
                    text += " - ";
            }
            return text;
        }
    }
}