using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Models;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class StandardCommandsAppService : AppServiceBase<StandardCommandEntity, int>, IStandardCommandsAppService
    {
        private readonly IStandardCommandService _service;

        public StandardCommandsAppService() : base()
        {
            _service = new BotCommandsService(
                new StandardCommandRepository(),
                new CommandTextGoogle(new GSheetsService()));
        }

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

        private Option responseTrigger;
        private Config config;
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

        public void Init()
        {
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.BaseDirectory;
            var pathDirectory = Path.Combine(basePath, "DataFile");
            var path = Path.Combine(pathDirectory, "commands.json");

            foreach (var item in _service.GetAll())
                _service.Remove(item);

            var lista = JsonConvert.DeserializeObject<List<StandardCommandEntity>>(File.ReadAllText(path));

            foreach (var item in lista)
                _service.Add(item);

        }

        public CollectionOptions HelpCommand()
        {
            Config config = Configs;

            var commands = _service.Find(x => x.IsVisibleOnMenu).ToList();
            var response = new CollectionOptions();
            foreach (var cmd in commands)
            {
                response.options.Add(new Option
                {
                    Trigger = cmd.Trigger,
                    Title = cmd.Name ?? cmd.Trigger
                });
            }
            response.Description = config.MessageHelp;
            return response;
        }

        public Option RandomCommand()
        {
            var commands = _service.GetAll();
            var rdm = new Random();
            var trigger = commands.ElementAt(rdm.Next(commands.Count())).Trigger;
            return GetResponseTo(trigger).WithImageFromLink().Build;
        }

        public Option GeneralCommand(string trigger)
        {
            var response = GetResponseTo(trigger).Build;
            if (response == null)
                return null;
            
            response.Buttons = GetButtonsDefault();
            return response;
        }

        public IList<ButtonOption> GetButtonsDefault()
        {
            if (responseTrigger == null)
                return null;

            var lista = new List<ButtonOption>();
            var maisUm = new ButtonOption()
            {
                Value = responseTrigger.Trigger,
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

            lista.Add(maisUm);
            lista.Add(randomCommmand);
            lista.Add(help);

            return lista;
        }

        public StandardCommandsAppService GetWaitMessage(string trigger)
        {
            responseTrigger = _service.GetWaitMessage(trigger);
            return this;
        }

        private StandardCommandsAppService GetResponseTo(string trigger)
        {
            responseTrigger = _service.GetResponseMessageToTrigger(trigger);
            if (responseTrigger != null)
                responseTrigger.Trigger = trigger;

            return this;
        }

        private StandardCommandsAppService WithImageFromLink()
        {
            if (responseTrigger == null)
                return this;

            var link = responseTrigger.Link;
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
                    responseTrigger.Image = ogImage.FirstOrDefault().Attributes.FirstOrDefault(a => a.Name == "content").Value;
                else
                    responseTrigger.Image = document.DocumentNode.SelectNodes("//img")?.FirstOrDefault()?.Attributes?.FirstOrDefault(a => a.Name == "content")?.Value;
            }
            else
                responseTrigger.Image = document.DocumentNode.SelectNodes("//img")?.FirstOrDefault()?.Attributes?.FirstOrDefault(a => a != null && a.Name == "content")?.Value;

            return this;
        }

        private Option Build
            => responseTrigger;
    }
}