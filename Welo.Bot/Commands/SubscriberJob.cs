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
            //foreach (var leadEntity in leads)
            //{
            //    var activity = JsonConvert.DeserializeObject<Activity>(leadEntity.Activity);
            //    if (string.IsNullOrEmpty(leadEntity.Activity))
            //        continue;
                

            //    var reply = activity.CreateReply("");
            //    reply.Text = "Wake up!";
            //    ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));

            //    CreateCardMessage(reply, leadEntity.LastTriggerUsed);
                
            //    await connector.Conversations.ReplyToActivityAsync(reply);
            //}
        }
       
        private static void CreateCardMessage(IMessageActivity activity, string trigger)
        {
            if (string.IsNullOrEmpty(trigger))
                return;

            var response = StandardCommandsAppService.Intance.GeneralCommand(trigger);
            if (response == null)
                return;

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