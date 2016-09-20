namespace Welo.Bot.ViewModels
{
    public class FacebookChannelData
    {
        [Newtonsoft.Json.JsonProperty("quick_replies")]
        public FacebookQuickReply[] QuickReplies { get; set; }
    }
}