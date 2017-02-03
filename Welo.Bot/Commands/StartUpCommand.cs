using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Welo.Application.Interfaces;
using Welo.Bot.Commands.Interfaces;
using Welo.Bot.Luis;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;

namespace Welo.Bot.Commands
{
    [Serializable]
    public class StartUpCommand : IStartUpCommand
    {
        private readonly LuisClient _luisClient;
        private const string Sad = "Ops! não entendi muito bem, pode repetir?";

        public StartUpCommand()
        {
            _luisClient = LuisClient.Create();
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var service = scope.Resolve<IStandardCommandsAppService>();
                var forwardMessage = context.MakeMessage();
                var response = service.GeneralCommand(message.Text);
                forwardMessage.Text = message.Text;

                if (response == null)
                {
                    var luisResponse = await _luisClient.SendQuery(message.Text);
                    var trigger = await ParseLuisResponse(luisResponse);
                    response = service.GeneralCommand(trigger);
                }

                if (response == null)
                {
                    context.Done(forwardMessage);
                    return;
                }

                if (response.WithButtons)
                {
                    var card = CreateCardMessage(response);
                    forwardMessage.Attachments.Add(card?.ToAttachment());
                }
                else
                    forwardMessage.Text = response.MessageFormated;

                context.Done(forwardMessage);
            }
        }

        private async Task<string> ParseLuisResponse(LuisResponse luisResponse)
        {
            Intent winner = luisResponse.Winner();

            if (winner == null || winner.IsNone())
                return Sad;

            return luisResponse.Winner().Name;
        }

        private static HeroCard CreateCardMessage(ResponseTrigger response)
        {
            if (response == null)
                return null;

            var cardButtons = new List<CardAction>();
            var lista = response.Buttons;

            foreach (var item in lista)
            {
                cardButtons.Add(new CardAction
                {
                    Value = item.Value,
                    Type = Mapper.Map<TypeButton, string>(item.Type),
                    Title = item.Title
                });
            }
            var openUrl = new CardAction()
            {
                Value = response.Link,
                Type = "openUrl"
            };

            return new HeroCard
            {
                Title = response.Title,
                Subtitle = response.Author,
                Text = response.MessageFormated,
                Images = new List<CardImage> {
                   new CardImage
                    {
                        Tap = openUrl,
                        Url = response.Image
                    }
                },
                Tap = openUrl,
                Buttons = cardButtons
            };

        }
    }
}