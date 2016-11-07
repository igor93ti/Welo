using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Welo.Application.AppServices;
using Welo.Domain.Entities;

namespace Welo.Bot
{
    [Serializable]
    public class RandomCommand : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> message)
        {
            var service = new StandardCommandsAppService();
            IMessageActivity activity = context.MakeMessage();
            var cardButtons = new List<CardAction>();
            var response = service.RandomCommand();
            var card = CreateCardMessage(response);
            activity.Attachments.Add(card?.ToAttachment());

            context.Done(activity);
        }

        private static HeroCard CreateCardMessage(Option response)
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

            //var subscribe = new CardAction()
            //{
            //    Value = "subscribe|" + response.Trigger,
            //    Type = "postBack",
            //    Title = "Subscribe"
            //};
            cardButtons.Add(maisUm);
            cardButtons.Add(randomCommmand);
            //cardButtons.Add(subscribe);
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