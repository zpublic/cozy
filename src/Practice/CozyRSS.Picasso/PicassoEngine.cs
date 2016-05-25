using CozyRSS.Syndication.Model;
using System.Collections.Generic;

namespace CozyRSS.Picasso
{
    public class PicassoEngine
    {
        CacheManage _Cache = new CacheManage();

        public bool Init(string storagePath, List<string> urls)
        {
            return _Cache.Init(storagePath, urls);
        }

        public SyndicationFeed Get(string url)
        {
            return _Cache.Get(url);
        }

        public bool IsReaded(SyndicationItem item)
        {
            return _Cache.IsReaded(item);
        }

        public void SetReaded(SyndicationItem item, bool readed = true)
        {
            _Cache.SetReaded(item, readed);
        }

        public SyndicationFeed Flush(string url)
        {
            return _Cache.Flush(url);
        }
    }
}
