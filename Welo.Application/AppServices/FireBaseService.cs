using System;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Bot.Connector;

namespace Welo.Application.AppServices
{
    public class FireBaseService
    {
        private IFirebaseClient _client;

        private static FireBaseService _instance;

        public static FireBaseService Intance
        {
            get
            {
                if (Intance == null)
                    lock (typeof(FireBaseService))
                        _instance = new FireBaseService();

                return Intance;
            }
        }

        private FireBaseService()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "RMt9emrvYaookgS2S8n2lmX8liHY2RTWHjWO69Oo",
                BasePath = "https://welobot-1215.firebaseio.com"
            };

            _client = new FirebaseClient(config);
        }

        public async void Push(IMessageActivity message)
        {
            var todo = new Lead
            {
                Id = Guid.NewGuid(),
                Name = message.From.Name,
                IdBot = message.From.Id,
                Channel = message.ChannelId,
                Conversion = "teste"
            };

            var response = _client.PushAsync("welobot/leads/", todo);

            var result = response.Result.ResultAs<Lead>();
        }

        public async void Set()
        {
            var todo = new Lead
            {
                Id = Guid.NewGuid(),
                Name = "Execute SET",
                Conversion = "teste",
                IdBot = "teste"
            };

            var response = _client.SetAsync("welobot/leads/", todo);

            var result = response.Result.ResultAs<Lead>();
        }
    }
}