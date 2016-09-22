using System;
using System.Collections.Generic;
using System.Linq;
using Chronic;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandartCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandartCommandsAppService
    {
        private readonly IStandardCommandService _service;

        public StandartCommandsAppService() : base()
        {
            _service = new StandartCommandsService(new StandardCommandRepository());
        }

        public string GetResponseMessageToTrigger(string trigger)
        {
            var formatMask = new int[1, 3, 2];

            var fomart = Number2String(1);
            var message = string.Empty;

            var response = GoogleService.GetValues(new string[] { "Books" });

            var commandReply = response.Select(x => x.Values);
            var items = commandReply as IList<IList<object>>[] ?? commandReply.ToArray();
            var randomRow = new Random(1);
      
            var index = randomRow.Next(items.FirstOrDefault().Count);
            message = items
                .Aggregate(message, (seed, item) => item[index]
                .Aggregate(seed, (current, item3) => current +" "+ item3));
            
            var triggers = _service.Find(x => x.Trigger == trigger);
            var command = triggers.FirstOrDefault();
            return command != null ? message : "Ops, esse comando não existe";
        }
        private String Number2String(int number)
        {
            Char c = (Char)((65) + (number - 1));
            return c.ToString();
        }
    }
}