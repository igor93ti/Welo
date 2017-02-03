using Newtonsoft.Json;

namespace Welo.Bot.Luis
{

    public class Entity
    {
        [JsonProperty("entity")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}