using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using Quartz;
using Welo.Application.Interfaces;
using Welo.Bot.Commands.Interfaces;

namespace Welo.Bot.Commands
{

    public class SubscriberJob : IJob
    {
        private IStartUpCommand _command;
        private ILeadAppService _leadAppService;
        IStandardCommandsAppService _service;

        public SubscriberJob(IStartUpCommand command, IStandardCommandsAppService service, ILeadAppService leadAppService)
        {
            _leadAppService = leadAppService;
            _command = command;
            _service = service;
        }
    
        public async void Execute(IJobExecutionContext context)
        {
            var leads = _leadAppService.GetAll();

            foreach (var item in leads)
            {
                var incomingMessageServiceUrl = item.ServiceUrl;
                var botAccount = new ChannelAccount(name: item.BotName, id: item.BotId);
                var userAccount = new ChannelAccount(name: item.UserName, id: item.UserId);

                var connector = new ConnectorClient(new Uri(incomingMessageServiceUrl));
                var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);
                IMessageActivity message = Activity.CreateMessageActivity();
                message.From = botAccount;
                message.Recipient = userAccount;
                message.Conversation = new ConversationAccount(id: conversationId.Id);
                message.Text = "Hello";
                message.Locale = "pt-Br";
                await connector.Conversations.SendToConversationAsync((Activity)message);
            }
        }

        private void CreateCardMessage(IMessageActivity activity, string trigger)
        {
            if (string.IsNullOrEmpty(trigger))
                return;

            var response = _service.GeneralCommand(trigger);
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