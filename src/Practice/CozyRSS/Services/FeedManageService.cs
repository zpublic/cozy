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
            if (!_controller.ReadFromFile(_cfgFile))
            {
                _SetDefaultFeeds();
                Save();
            }
        }

        private void _SetDefaultFeeds()
        {
            _controller.AddFeed("博客园_老肉鸡", "http://feed.cnblogs.com/blog/u/132703/rss");
            _controller.AddFeed("知乎每日精选", "http://www.zhihu.com/rss");
            _controller.AddFeed("科学松鼠会", "http://songshuhui.net/feed");
            _controller.AddFeed("虎嗅网", "http://www.huxiu.com/rss/0.xml");
            _controller.AddFeed("36氪", "http://36kr.com/feed");
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
