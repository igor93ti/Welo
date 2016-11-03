using System;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandardCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandardCommandsAppService
    {
        private readonly IStandardCommandService _service;

        private static StandardCommandsAppService _instance;

        public static StandardCommandsAppService Intance
        {
            get
            {
                if (_instance == null)
                    lock (typeof(StandardCommandsAppService))
                        if (_instance == null)
                            _instance = new StandardCommandsAppService();

                return _instance;
            }
        }

        public StandardCommandsAppService() : base()
        {
            _service = new BotCommandsService(
                new StandardCommandRepository(),
                new CommandTextGoogle(new GSheetsService()));
            Service = _service;
        }

        public ResponseTrigger GetResponseMessageToTrigger(string trigger)
        {
            var responseMessageToTrigger = _service.GetResponseMessageToTrigger(trigger);
            if (responseMessageToTrigger != null)
                responseMessageToTrigger.Image = GetFirstImageLink(responseMessageToTrigger.Link);
            return responseMessageToTrigger;
        }

        public string GetFirstImageLink(string url)
        {
            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            var ResponseStream = response.GetResponseStream();

            var document = new HtmlDocument();
            document.Load(ResponseStream);
            var ogMeta = document.DocumentNode.SelectNodes("//meta[@property]");

            if (ogMeta != null)
            {
                var ogImage = document.DocumentNode.SelectNodes("//meta[@property]")
                                      .Where(x => x.Attributes["property"].Value == "og:image");
                if (ogImage.Any())
                    return ogImage.FirstOrDefault().Attributes.FirstOrDefault(a => a.Name == "content").Value;
                else
                    return document.DocumentNode.SelectNodes("//img").FirstOrDefault()?.Attributes.FirstOrDefault(a => a.Name == "content")?.Value;
            }
            else
                return document.DocumentNode.SelectNodes("//img").FirstOrDefault()?.Attributes.FirstOrDefault(a => a.Name == "content")?.Value;
        }
    }
}