namespace Welo.Bot.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Interfaces;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Dialogs.Internals;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class RootDialog : IRootDialog
    {
        ICommand _service;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
            {
                var messageText = message.Text;

                if (messageText.Contains('|'))
                    messageText = messageText.Substring(0, messageText.LastIndexOf('|'));

                if (messageText.Contains("random"))
                    _service = scope.Resolve<IRandomCommand>();
                else if (messageText.Contains("help"))
                    _service = scope.Resolve<IHelpCommand>();
                else if (messageText.Contains("subscribe"))
                    _service = scope.Resolve<ISubscribeCommand>();
                else
                    _service = scope.Resolve<IStartUpCommand>();


                await context.Forward(_service, this.ResumeAfterDialog, message, context.CancellationToken);
            }
        }

        public async Task ResumeAfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;

                if ((message is string && !string.IsNullOrEmpty((string)message)))
                    await context.PostAsync((string)message);


                await context.PostAsync((Activity)message);
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
