using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

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
                if (_instance == null)
                    lock (typeof(FireBaseService))
                        _instance = new FireBaseService();

                return _instance;
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

        //public async void PushMessage(IMessageActivity message)
        //{
        //    var lead = new LeadEntity
        //    {
        //        FromId = message.From.Id,
        //        FromName = message.From.Name,
        //        LastTriggerUsed = message.ChannelId
        //    };

        //    var commandStatics = await _client.GetAsync("welobot/statistics/" + message.Text.ToUpper());
        //    var statistics = commandStatics.ResultAs<CommandStatistics>();

        //    var temp = new CommandStatistics
        //    {
        //        Name = message.Text.ToUpper(),
        //        Usages = statistics?.Usages + 1 ?? 1
        //    };

        //    _client.SetAsync("welobot/leads/" + lead.IdUser, lead);
        //    _client.SetAsync("welobot/statistics/" + message.Text.ToUpper(), temp);
        //}
    }

    public class CommandStatistics
    {
        public string Name { get; set; }
        public int Usages { get; set; }
    }
}