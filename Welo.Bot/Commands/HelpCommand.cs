using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Welo.Application.AppServices;
using Welo.Bot.Commands;

namespace Welo.Bot
{
    [Serializable]
    public class HelpCommand : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            IMessageActivity activity = context.MakeMessage();

            var cardButtons = new List<CardAction>();
            var commands = StandardCommandsAppService.Intance.HelpCommand();

            PromptDialog.Choice(
                    context,
                    this.OnOptionSelected,
                    commands.options.Select(x => x.Trigger),
                    commands.Description,
                    null,
                    3,
                    PromptStyle.Keyboard,
                    commands.options.Select(x => x.Title));
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var optionSelected = await result;
                var activity = context.MakeMessage();
                activity.Text = optionSelected;
                var root = new RootDialog();
                await context.Forward(root, root.ResumeAfterDialog, activity, CancellationToken.None);

            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");
                context.Wait(this.MessageReceivedAsync);
            }
        }

    }
}