using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Welo.Application.Interfaces;

namespace Welo.Bot.Commands
{
    [Serializable]
    public class StartUpCommand : IDialog<object>
    {
        private readonly IStandardCommandsAppService _appService;

        public StartUpCommand(IStandardCommandsAppService appService)
        {
            SetField.NotNull(out _appService, nameof(appService), appService);
        }

        public async Task StartAsync(IDialogContext context)
            => context.Wait(MessageReceivedAsync);

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var response = _appService.GetResponseMessageToTrigger(message.Text);
            if (response != null)
            {
                await context.PostAsync(response);
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync("Não deu");
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}