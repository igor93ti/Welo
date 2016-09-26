using System;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services.GSheets;

namespace Welo.Domain.Services.GSheets
{
    public class CommandTextGoogle : ICommandTextGoogle
    {
        private readonly IGSheetsService _service;

        public CommandTextGoogle(IGSheetsService service)
        {
            _service = service;
        }

        public string GetTextRandomRowGSheets(GSheetQuery query)
        {
            var formatMask = new int[1, 3, 2];

            var format = Number2String(1);

            var message = string.Empty;

            var sheet = _service.BatchGet(query.Ranges);
            var randomRow = new Random();
            var index = randomRow.Next(sheet.Count);

            foreach (var text in sheet[index])
                message += text.ToString();

            return message;
        }

        private static string Number2String(int number)
        {
            Char c = (char)((65) + (number - 1));
            return c.ToString();
        }
    }
}