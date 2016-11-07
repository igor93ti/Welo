using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Welo.Application.AppServices;
using Welo.Domain.Entities;

namespace Welo.Bot
{
    [Serializable]
    public class SubscribeCommand : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            IMessageActivity activity = context.MakeMessage();
            LeadAppService.Intance.SaveSubscriber(new LeadEntity()
            {
                IdUser = message.From.Id,
                Name = message.From.Name,
                LastTriggerUsed = message.Text.Substring(message.Text.LastIndexOf('|') + 1),
                ChannelConversion = message.ChannelId,
                Activity = JsonConvert.SerializeObject(message),
                Context = JsonConvert.SerializeObject(context)
            });
            activity.Text = "Parabéns você está inscrito";

            context.Done(activity);
        }
    }
}