using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using RollbarDotNet;
using Welo.Application.AppServices;
using Welo.Bot.Commands;
using Welo.Domain.Entities;
using Exception = System.Exception;

namespace Welo.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            try
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    LeadAppService.Intance.SaveSubscriber(new LeadEntity()
                    {
                        IdUser = activity.From.Id,
                        Name = activity.From.Name,
                        LastTriggerUsed = activity.Text,
                        ChannelConversion = activity.ChannelId,
                        Activity = JsonConvert.SerializeObject(activity)
                    });
                    
                    await Conversation.SendAsync(activity, () => new StartUpCommand());
                }
                else
                {
                    HandleSystemMessage(activity);
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