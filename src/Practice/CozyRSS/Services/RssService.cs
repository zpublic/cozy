using CozyRSS.Picasso;
using CozyRSS.Syndication.Model;
using System.Collections.Generic;

namespace CozyRSS.Services
{
    public class RssService
    {
        public static readonly RssService RssSrv = new RssService();
        PicassoEngine _Picasso;

        public void Init(List<string> urls)
        {
            _Picasso = new PicassoEngine();
            _Picasso.Init("./Picasso", urls);
        }

        public SyndicationFeed GetRssFeed(string url)
        {
            return _Picasso.Get(url);
        }

        public SyndicationFeed FlushRssFeed(string url)
        {
            return _Picasso.Flush(url);
        }
    }
}
