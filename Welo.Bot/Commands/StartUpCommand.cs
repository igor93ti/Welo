using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Welo.Application.AppServices;

namespace Welo.Bot.Commands
{
    [Serializable]
    public class StartUpCommand : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            var _firebaseService = FireBaseService.Intance;
            _firebaseService.PushMessage(message);
            var response = StandardCommandsAppService.Intance.GetResponseMessageToTrigger(message.Text);

            if (string.IsNullOrEmpty(response))
            {
                context.Done(string.Empty);
            }
            else
            {
                await context.PostAsync(response);
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}