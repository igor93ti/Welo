using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Activity = Microsoft.Bot.Connector.Activity;

namespace Welo.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly ILifetimeScope _scope;

        public MessagesController(ILifetimeScope scope)
        {
            SetField.NotNull(out this._scope, nameof(scope), scope);
        }

        public async Task<HttpResponseMessage> Post([FromBody] Activity activity, CancellationToken token)
        {
            try
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    using (var scope = DialogModule.BeginLifetimeScope(this._scope, activity))
                    {
                        var postToBot = scope.Resolve<IPostToBot>();
                        await postToBot.PostAsync(activity, token);
                    }
                }
                else
                {
                    HandleSystemMessage(activity);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.Typing)
            {
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}