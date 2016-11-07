
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Welo.Bot.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string FlightsOption = "Flights";

        private const string HotelsOption = "Hotels";
        private Dictionary<string, IDialog<object>> commandsDefault;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var messageText = message.Text;
            commandsDefault = new Dictionary<string, IDialog<object>>
                    {
                        { "random",new RandomCommand()},
                        { "help",new HelpCommand()},
                        { "subscribe",new SubscribeCommand ()}
                    };

            if (messageText.Contains('|'))
                messageText = messageText.Substring(0, messageText.LastIndexOf('|'));

            IDialog<object> command;

            if (commandsDefault.Keys.Contains(messageText))
                command = commandsDefault[messageText];
            else
                command = new StartUpCommand();

            //ShowOptions(context);

            await context.Forward(command, this.ResumeAfterDialog, message, CancellationToken.None);

        }

        public async Task ResumeAfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;

                if ((message.GetType() == typeof(string) && string.IsNullOrEmpty((string)message)) || message == null)
                    return;

                //activity.TextFormat = TextFormatTypes.Markdown;
                await context.PostAsync((Activity)message);

                //var message = await result;
                //var activity = (Activity)message;
                //activity.TextFormat = TextFormatTypes.Plain;
                //var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //await connector.Conversations.ReplyToActivityAsync(activity);
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}