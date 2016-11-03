using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Welo.Application.AppServices;
using Welo.Domain.Entities;

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
            var activity = context.MakeMessage();

            var service = new StandardCommandsAppService();
            var response = service.GetResponseMessageToTrigger(message.Text);
            if (response == null)
            {
                context.Done(string.Empty);
            }
            var card = CreateCardMessage(response);
            //var url = response.Image.Substring(response.Image.LastIndexOf("http", StringComparison.Ordinal));
            //var extension = response.Image.Substring(response.Image.LastIndexOf('.'));
            //var image = new Attachment
            //{
            //    ContentUrl = url,
            //    ContentType = "image/" + extension,
            //    Name = response.Title
            //};


            //message.Attachments.Add(image);
            activity.Attachments.Add(card.ToAttachment());
            await context.PostAsync(activity);
            context.Wait(MessageReceivedAsync);
        }


        private static HeroCard CreateCardMessage(ResponseTrigger response)
        {
            var cardButtons = new List<CardAction>();

            var openUrl = new CardAction()
            {
                Value = response.Link,
                Type = "openUrl"
            };

            var plButton = new CardAction()
            {
                Value = response.Trigger,
                Type = "postBack",
                Title = "Mais um"
            };

            cardButtons.Add(plButton);

            var plCard = new HeroCard
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
            return plCard;

        }
    }
}