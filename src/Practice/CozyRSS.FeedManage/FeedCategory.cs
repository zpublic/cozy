using Newtonsoft.Json;
using System.Collections.Generic;

namespace CozyRSS.FeedManage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FeedCategory
    {
        [JsonProperty]
        public string name;

        [JsonProperty]
        public List<FeedCategory> subCategories;

        [JsonProperty]
        public List<FeedNode> subNodes;

        public FeedCategory parent;
    }
}
