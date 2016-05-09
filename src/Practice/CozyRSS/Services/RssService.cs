using System.ServiceModel.Syndication;
using System.Xml;

namespace CozyRSS.Services
{
    public class RssService
    {
        public static SyndicationFeed GetRssFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            Rss20FeedFormatter format = new Rss20FeedFormatter();
            format.ReadFrom(reader);
            return format.Feed;
        }
    }
}
