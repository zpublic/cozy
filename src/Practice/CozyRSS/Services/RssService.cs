using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using System.Xml;

namespace CozyRSS.Services
{
    public class RssService
    {
        public static SyndicationFeed GetRssFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            SyndicationFeed item = Rss20FeedFormatter.Parse(doc.SelectSingleNode("rss/channel"));
            return item;
        }
    }
}
