using System.Collections.Generic;
using Newtonsoft.Json;

namespace Welo.Bot.Luis
{

    public class LuisResponse
    {
        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; }

        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; }
    }
}