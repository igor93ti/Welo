using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Welo.Application.Interfaces;
using Welo.Bot.Commands.Interfaces;

namespace Welo.Bot
{
    [Serializable]
    public class HelpCommand : IHelpCommand
    {
        public HelpCommand()
        {
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var service = scope.Resolve<IStandardCommandsAppService>();

                var commands = service.HelpCommand();
                PromptDialog.Choice(context, this.OnOptionSelected, commands.options.Select(x => x.Trigger), commands.Description, null, 3, PromptStyle.Keyboard, commands.options.Select(x => x.Title));
            }
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var optionSelected = await result;
                var activity = context.MakeMessage();
                activity.Text = optionSelected;
                context.Done(activity);

            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}