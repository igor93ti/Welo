using System;
using System.Collections.Generic;
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
    [Serializable]
    public class RandomCommand : IRandomCommand
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> obj)
        {
            var message = await obj;

            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var _service = scope.Resolve<IStandardCommandsAppService>();
                IMessageActivity activity = context.MakeMessage();
                var response = _service.RandomCommand();
                var card = CreateCardMessage(response);
                activity.Attachments.Add(card?.ToAttachment());

                context.Done(activity);
            }
        }

        private static HeroCard CreateCardMessage(ResponseTrigger response)
        {
            if (response == null)
                return null;

            var cardButtons = new List<CardAction>();

            var openUrl = new CardAction()
            {
                Value = response.Link,
                Type = "openUrl"
            };

            var maisUm = new CardAction()
            {
                Value = response.Trigger,
                Type = "postBack",
                Title = "Mais um"
            };

            var randomCommmand = new CardAction()
            {
                Value = "randomCommand",
                Type = "postBack",
                Title = "Random"
            };

            var help = new CardAction()
            {
                Value = "help",
                Type = "postBack",
                Title = "Help"
            };

            var subscribe = new CardAction()
            {
                Value = "subscribe|" + response.Trigger,
                Type = "postBack",
                Title = "Subscribe"
            };
            cardButtons.Add(maisUm);
            cardButtons.Add(randomCommmand);
            cardButtons.Add(subscribe);
            cardButtons.Add(help);

            return new HeroCard
            {
                Title = response.Title,
                Subtitle = response.Author,
                Text = response.Quote,
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