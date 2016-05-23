using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using System;

namespace CozyRSS.Services
{
    public class RssService
    {
        public static SyndicationFeed GetRssFeed(string url)
        {
            try
            {
                RssFormatter formatter = new RssFormatter();
                var result = formatter.Formatter(url);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
