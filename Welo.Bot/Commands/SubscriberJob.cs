using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Quartz;
using Welo.Application.AppServices;
using Welo.Domain.Entities;

namespace Welo.Bot.Commands
{

    public class SubscriberJob : IJob
    {
        private LeadAppService _leadAppService;
        private StartUpCommand _command;
        public SubscriberJob()
        {
            _leadAppService = LeadAppService.Intance;
            _command = new StartUpCommand();
        }

        public async void Execute(IJobExecutionContext context)
        {
            //var leads = _leadAppService.GetAll().ToList();
            //var command = RaffleCommand();
            //if (command == null)
            //    return;

            //foreach (var leadEntity in leads)
            //{
            //    var activity = JsonConvert.DeserializeObject<Activity>(leadEntity.Activity);
            //    if (string.IsNullOrEmpty(leadEntity.Activity))
            //        continue;

            //    CreateCardMessage(activity, leadEntity.LastTriggerUsed);

            //    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            //    await connector.Conversations.SendToConversationAsync((Activity)activity);
            //}

        }

        private static StandardCommandEntity RaffleCommand()
        {
            var listCommmands = StandardCommandsAppService.Intance.GetAll();
            if (!listCommmands.Any())
                return null;

            var rdn = new Random();
            return listCommmands.ElementAt(rdn.Next(listCommmands.Count()));
        }

        private static void CreateCardMessage(IMessageActivity activity, string trigger)
        {
            var response = StandardCommandsAppService.Intance.GetResponseMessageToTrigger(trigger);
            var cardButtons = new List<CardAction>();
            var plButton = new CardAction()
            {
                Value = trigger,
                Type = "postBack",
                Title = "Mais um"
            };
            cardButtons.Add(plButton);

            var plCard = new HeroCard
            {
                Title = response.Title,
                Subtitle = response.Author,
                Text = response.Quote,
                Tap = new CardAction()
                {
                    Value = response.Link,
                    Type = "openUrl"
                },
                Buttons = cardButtons
            };
            activity.Attachments.Add(plCard.ToAttachment());
        }
    }
}