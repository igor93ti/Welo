using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Welo.Application.Interfaces;
using Welo.Bot.Commands.Interfaces;
using Welo.Domain.Entities;

namespace Welo.Bot
{
    public class SubscribeCommand : ISubscribeCommand
    {
        public async Task StartAsync(IDialogContext context) => context.Wait(MessageReceivedAsync);

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var service = scope.Resolve<ILeadAppService>();
                IMessageActivity activity = context.MakeMessage();
                var serviceUrl = message.ServiceUrl;

                var botId = message.From.Id;
                var botName = message.From.Name;

                var userId = message.Recipient.Id;
                var userName = message.Recipient.Name;

                service.SaveSubscriber(new LeadEntity
                {
                    BotId = message.From.Id,
                    BotName = message.From.Name,
                    LastTriggerUsed = message.Text.Substring(message.Text.LastIndexOf('|') + 1),
                    ServiceUrl = message.ServiceUrl,
                    UserId = message.Recipient.Id,
                    UserName = message.Recipient.Name
                });
                activity.Text = "Parabéns você está inscrito";
                context.Done(activity);

                //var leads = _leadAppService.GetAll();

                //foreach (var item in leads)
                //{
                //    var incomingMessageServiceUrl = item.ServiceUrl;
                //    var botAccount = new ChannelAccount(name: item.BotName, id: item.BotId);
                //    var userAccount = new ChannelAccount(name: item.UserName, id: item.UserId);

                //    var connector = new ConnectorClient(new Uri(incomingMessageServiceUrl));
                //    var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);
                //    IMessageActivity message = Activity.CreateMessageActivity();
                //    message.From = botAccount;
                //    message.Recipient = userAccount;
                //    message.Conversation = new ConversationAccount(id: conversationId.Id);
                //    message.Text = "Hello";
                //    message.Locale = "pt-Br";
                //    await connector.Conversations.SendToConversationAsync((Activity)message);
                //}
            }
        }
    }
}