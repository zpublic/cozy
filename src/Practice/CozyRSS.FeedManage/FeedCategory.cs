using System.Collections.Generic;

namespace CozyRSS.FeedManage
{
    public class FeedCategory
    {
        public string name;
        public List<FeedCategory> subCategories;
        public List<FeedNode> subNodes;
    }
}
