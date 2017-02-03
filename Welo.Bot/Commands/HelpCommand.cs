using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Welo.Application.Interfaces;
using Welo.Bot.Commands.Interfaces;
using Welo.Domain.Entities.Enums;

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

                var cardButtons = new List<CardAction>();
                foreach (var item in commands.options.Select(x => new ButtonsHelp { Title = x.Title, Trigger = x.Trigger }))
                {

                    cardButtons.Add(new CardAction
                    {
                        Value = item.Trigger,
                        Type = Mapper.Map<TypeButton, string>(TypeButton.PostBack),
                        Title = item.Title
                    });
                }

                var card = new HeroCard
                {
                    Buttons = cardButtons
                };

                var forwardMessage = context.MakeMessage();
                forwardMessage.Attachments.Add(card?.ToAttachment());
                context.Done(forwardMessage);
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

    public class ButtonsHelp
    {
        public string Title { get; set; }
        public string Trigger { get; set; }
    }
}