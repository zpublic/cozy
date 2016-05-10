using CozyRSS.FeedManage;
using System.Collections.Generic;

namespace CozyRSS.Services
{
    public class FeedManageService
    {
        public static readonly FeedManageService FeedManage = new FeedManageService();

        const string _cfgFile = "c:\\cozy_test.json";
        FeedManageController _controller = new FeedManageController();

        public void Load()
        {
            _controller.ReadFromFile(_cfgFile);
        }

        public void Save()
        {
            _controller.SaveToFile(_cfgFile);
        }

        public List<FeedNode> GetFeeds()
        {
            var root = _controller.Feeds;
            return root?.subNodes??new List<FeedNode>();
        }

        public void RemoveFeed(FeedNode feed)
        {
            _controller.RemoveNode(feed);
            Save();
        }
    }
}
