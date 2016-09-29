using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services.GSheets;

namespace Welo.Domain.Services.GSheets
{
    [Serializable]
    public class CommandTextGoogle : ICommandTextGoogle
    {
        private readonly IGSheetsService _service;

        public CommandTextGoogle(IGSheetsService service)
        {
            _service = service;
        }

        public IList<object> GetRandomRowGSheets(GSheetQuery query)
        {
            var sheet = _service.BatchGet(query.Ranges);
            var randomRow = new Random();
            var index = randomRow.Next(1, sheet.Count);

            return sheet[index];
        }
    }
}