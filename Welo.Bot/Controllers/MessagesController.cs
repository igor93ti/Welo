using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using RollbarDotNet;
using Welo.Bot.Commands;
using Exception = System.Exception;

namespace Welo.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity, CancellationToken token)
        {
            try
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                    {
                        var dialog = scope.Resolve<IRootDialog>();
                        await Conversation.SendAsync(activity, () => dialog);
                    }
                }
                else
                {
                    await HandleSystemMessage(activity);
                }
            }
            catch (Exception e)
            {

                var st = new StackTrace(e, true); // create the stack trace
                var query = st.GetFrames()         // get the frames
                              .Select(frame => new
                              {                   // get the info
                                  FileName = frame.GetFileName(),
                                  LineNumber = frame.GetFileLineNumber(),
                                  ColumnNumber = frame.GetFileColumnNumber(),
                                  Method = frame.GetMethod(),
                                  Class = frame.GetMethod().DeclaringType,
                                  execption = e
                              });

                var builder = new StringBuilder();
                foreach (var qr in query)
                {
                    builder.AppendLine($"file: {qr.FileName}, line: {qr.LineNumber}, column: {qr.ColumnNumber}");
                    builder.AppendLine($"class: {qr.Class}");
                    builder.AppendLine($"method: {qr.Method}");
                    builder.AppendLine();
                    builder.AppendLine();
                    builder.AppendLine($"execption: {qr.execption}");
                }

                Rollbar.Report(builder.ToString());
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task HandleSystemMessage(Activity activity)
        {
            if (activity.Type == ActivityTypes.DeleteUserData)
            {
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                IConversationUpdateActivity update = activity;
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    if (update.MembersAdded.Any())
                    {
                        var reply = activity.CreateReply();
                        foreach (var newMember in update.MembersAdded)
                        {
                            if (newMember.Id != activity.Recipient.Id)
                            {
                                reply.Text = $"Welcome {newMember.Name}!";
                            }
                            else
                            {
                                reply.Text = $"Welcome {activity.From.Name}";
                            }
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                var reply = activity.CreateReply("Bem vindos 2x");

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                var reply = activity.CreateReply("digitando");

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Type == ActivityTypes.Ping)
            {
            }
        }
    }
}