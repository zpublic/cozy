using CozyRSS.FeedManage;
using System.Collections.Generic;

namespace CozyRSS.Services
{
    public class FeedManageService
    {
        public static readonly FeedManageService FeedManage = new FeedManageService();

        const string _cfgFile = "my_rss.json";
        FeedManageController _controller;

        public void Load()
        {
            if (_controller == null)
            {
                _controller = new FeedManageController();
            }
            _controller.ReadFromFile(_cfgFile);
        }

        public void Save()
        {
            _controller?.SaveToFile(_cfgFile);
        }

        public List<FeedNode> GetFeeds()
        {
            if (_controller == null)
            {
                _controller = new FeedManageController();
                Load();
            }
            var root = _controller.Feeds;
            return root?.subNodes??new List<FeedNode>();
        }

        public void RemoveFeed(FeedNode feed)
        {
            _controller?.RemoveNode(feed);
            Save();
        }

        public FeedNode AddFeed(string url, string name = null)
        {
            FeedNode f = _controller?.AddFeed(name ?? url, url);
            Save();
            return f;
        }

        public void RenameFeed(FeedNode feed, string name)
        {
            feed.name = name;
            Save();
        }
    }
}
