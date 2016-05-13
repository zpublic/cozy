using Newtonsoft.Json;

namespace CozyRSS.FeedManage
{
    public class FeedNode
    {
        public string name;

        public string url;

        [JsonIgnore]
        public FeedCategory parent;
    }
}
