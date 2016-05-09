using System.Collections.Generic;

namespace CozyRSS.FeedManage.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            FeedCategory c1 = new FeedCategory() { name = "c1" };
            c1.subNodes = new List<FeedNode>();
            c1.subNodes.Add(new FeedNode() { name = "1", url = "url1" });
            c1.subNodes.Add(new FeedNode() { name = "2", url = "url2" });

            FeedCategory c2 = new FeedCategory() { name = "c2" };
            c2.subNodes = new List<FeedNode>();
            c2.subNodes.Add(new FeedNode() { name = "3", url = "url3" });
            c2.subNodes.Add(new FeedNode() { name = "4", url = "url4" });
            c2.subCategories = new List<FeedCategory>();
            c2.subCategories.Add(c1);

            FeedCategory root = new FeedCategory() { name = "root" };
            root.subNodes = new List<FeedNode>();
            root.subNodes.Add(new FeedNode() { name = "5", url = "url5" });
            root.subNodes.Add(new FeedNode() { name = "6", url = "url6" });
            root.subCategories = new List<FeedCategory>();
            root.subCategories.Add(c1);
            root.subCategories.Add(c2);

            var json = JsonUtil.Obj2Json(root);
            var hehe = JsonUtil.Json2Obj(json);
        }
    }
}
