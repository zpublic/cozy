using Newtonsoft.Json;

namespace CozyRSS.FeedManage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FeedNode
    {
        [JsonProperty]
        public string name;

        [JsonProperty]
        public string url;

        public FeedCategory parent;
    }
}
