using System.Collections.Generic;

namespace CozyRSS.FeedManage.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            FeedManageController c = new FeedManageController();
            c.AddCategory("c1");
            c.AddCategory("c2");
            c.AddFeed("f1", "hehe");
            c.SaveToFile("c:\\cozy_test.json");
        }
    }
}
