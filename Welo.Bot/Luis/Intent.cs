using System;
using Newtonsoft.Json;

namespace Welo.Bot.Luis
{

    public class Intent
    {
        [JsonProperty("intent")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        public bool IsNone()
        {
            return Name.Equals("None", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}