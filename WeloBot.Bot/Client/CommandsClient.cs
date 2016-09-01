using System.Threading.Tasks;
using System.Web.Configuration;
using Flurl;
using Flurl.Http;

namespace WeloBot.Bot.Client
{
    public class CommandsClient
    {
        public async static Task<string> RequestResponseFromTrigger(string trigger)
        {
            string urlBase = WebConfigurationManager.AppSettings["UrlBaseApi"];
            var url = urlBase
                .AppendPathSegment("api/Commands/Find")
                .SetQueryParams(new { trigger = trigger });

            return await url.GetStringAsync();
        }
    }
}