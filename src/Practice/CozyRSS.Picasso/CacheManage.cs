using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using System.Collections.Generic;

namespace CozyRSS.Picasso
{
    class CacheManage
    {
        CalcKey _Calc = new CalcKey();
        StorageManage _Storage = new StorageManage();
        Dictionary<string, SyndicationFeed> _Feeds = new Dictionary<string, SyndicationFeed>();

        public bool Init(string storagePath, List<string> urls)
        {
            _Storage.SetPath(storagePath);
            foreach (var url in urls)
            {
                SyndicationFeed feed = _Storage.LoadFeed(_Calc.CalcFeedKey(url));
                _Feeds.Add(url, feed);
            }
            return true;
        }

        public SyndicationFeed Get(string url)
        {
            if (_Feeds.ContainsKey(url))
                return _Feeds[url];
            return null;
        }

        public bool IsReaded(SyndicationItem item)
        {
            return false;
        }

        public void SetReaded(SyndicationItem item, bool readed = true)
        {
            ;
        }

        public SyndicationFeed Flush(string url)
        {
            try
            {
                RssFormatter formatter = new RssFormatter();
                var result = formatter.Formatter(url);
                if (_Feeds.ContainsKey(url) && _Feeds[url]?.items?.Count > 0)
                {
                    if (result?.items?.Count > 0)
                    {
                        HashSet<string> itemsSet = new HashSet<string>();
                        _Feeds[url].items.InsertRange(0, result.items);
                        result.items.Clear();
                        foreach (var i in _Feeds[url].items)
                        {
                            var key = _Calc.CalcFeedItemKey(i);
                            if (itemsSet.Contains(key))
                            {
                                result.items.Add(i);
                                continue;
                            }
                            itemsSet.Add(key);
                        }
                        _Feeds[url].items = result.items;
                        _Storage.SaveFeed(_Calc.CalcFeedKey(url), _Feeds[url]);
                    }
                }
                else
                {
                    _Feeds[url] = result;
                    _Storage.SaveFeed(_Calc.CalcFeedKey(url), _Feeds[url]);
                }
                return _Feeds[url];
            }
            catch
            {
                return null;
            }
        }
    }
}
