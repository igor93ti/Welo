using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Welo.Application.Interfaces;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Models;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandardCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandardCommandsAppService
    {
        private readonly IStandardCommandService _service;
        private ResponseTrigger ResponseTriggerModel;
        private Config config;

        public StandardCommandsAppService(IStandardCommandService service) : base(service)
        {
            _service = service;
        }

        private Config Configs
        {
            get
            {
                if (config != null)
                    return config;

                var appDomain = AppDomain.CurrentDomain;
                var basePath = appDomain.BaseDirectory;
                var pathDirectory = Path.Combine(basePath, "DataFile");
                var path = Path.Combine(pathDirectory, "config.json");
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
                return config;
            }
        }

        public CollectionOptions HelpCommand()
        {
            var config = Configs;

            var commands = _service.Find(x => x.IsVisibleOnMenu).ToList();
            var response = new CollectionOptions();
            foreach (var cmd in commands)
            {
                response.options.Add(new ResponseTrigger
                {
                    Trigger = cmd.Trigger,
                    Title = cmd.Name ?? cmd.Trigger
                });
            }
            response.Description = config.MessageHelp;
            return response;
        }

        public ResponseTrigger RandomCommand()
        {
            var commands = _service.GetAll();
            var rdm = new Random();
            var trigger = commands.ElementAt(rdm.Next(commands.Count())).Trigger;
            return GetResponseTo(trigger).WithImageFromLink().Build;
        }

        public ResponseTrigger GeneralCommand(string trigger)
        {
            var response = GetResponseTo(trigger).WithImageFromLink().Build;
            if (response == null)
                return null;

            response.Buttons = GetButtonsDefault();
            return response;
        }

        private IList<ButtonOption> GetButtonsDefault()
        {
            if (ResponseTriggerModel == null)
                return null;

            var lista = new List<ButtonOption>();
            var maisUm = new ButtonOption()
            {
                Value = ResponseTriggerModel.Trigger,
                Type = TypeButton.PostBack,
                Title = "Mais um"
            };

            var randomCommmand = new ButtonOption()
            {
                Value = "random",
                Type = TypeButton.PostBack,
                Title = "Random"
            };

            var help = new ButtonOption()
            {
                Value = "help",
                Type = TypeButton.PostBack,
                Title = "Help"
            };

            var subscribe = new ButtonOption()
            {
                Value = "subscribe|" + ResponseTriggerModel.Trigger,
                Type = TypeButton.PostBack,
                Title = "Subscribe"
            };

            lista.Add(maisUm);
            lista.Add(randomCommmand);
            lista.Add(subscribe);
            lista.Add(help);

            return lista;
        }

        public StandardCommandsAppService GetWaitMessage(string trigger)
        {
            ResponseTriggerModel = _service.GetResponseMessageToTrigger(trigger);
            return this;
        }

        private StandardCommandsAppService GetResponseTo(string trigger)
        {
            ResponseTriggerModel = _service.GetResponseMessageToTrigger(trigger);
            if (ResponseTriggerModel != null)
                ResponseTriggerModel.Trigger = trigger;

            return this;
        }

        private StandardCommandsAppService WithImageFromLink()
        {
            if (ResponseTriggerModel == null)
                return this;

            var link = ResponseTriggerModel.Link;
            Uri uriResult;

            bool uriValid = Uri.TryCreate(link, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (string.IsNullOrWhiteSpace(link) || !uriValid)
                return this;

            var request = HttpWebRequest.Create(uriResult) as HttpWebRequest;
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
                    ResponseTriggerModel.Image = ogImage.FirstOrDefault().Attributes.FirstOrDefault(a => a.Name == "content").Value;
                else
                    ResponseTriggerModel.Image = document.DocumentNode.SelectNodes("//img")?.FirstOrDefault()?.Attributes?.FirstOrDefault(a => a.Name == "content")?.Value;
            }
            else
                ResponseTriggerModel.Image = document.DocumentNode.SelectNodes("//img")?.FirstOrDefault()?.Attributes?.FirstOrDefault(a => a != null && a.Name == "content")?.Value;

            return this;
        }

        private ResponseTrigger Build
            => ResponseTriggerModel;
    }
}