using CozyRSS.Syndication.Model;
using System.Xml;

namespace CozyRSS.Syndication.Parser
{
    public class Rss20FeedFormatter
    {
        public static SyndicationFeed Parse(XmlNode node)
        {
            SyndicationFeed feed = new SyndicationFeed();
            if (node?.Name == "channel")
            {
                feed.title = node.SelectSingleNode("title")?.InnerText;
                feed.description = node.SelectSingleNode("description")?.InnerText;
                feed.link = node.SelectSingleNode("link")?.InnerText;
                feed.pubDate = node.SelectSingleNode("pubDate")?.InnerText;
                feed.lastBuildDate = node.SelectSingleNode("lastBuildDate")?.InnerText;
                feed.image = Rss20ImageFormatter.Parse(node.SelectSingleNode("image"));
                foreach(XmlNode i in node.SelectNodes("item"))
                {
                    SyndicationItem item = Rss20ItemFormatter.Parse(i);
                    if (item.IsValid())
                        feed.items.Add(item);
                }
            }
            return feed;
        }
    }
}
