using CozyRSS.Syndication.Model;
using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class Rss20FeedFormatter {

        public static SyndicationFeed Parse(XmlNode node) {

            SyndicationFeed feed = RssFormatter.Parese<SyndicationFeed>(node);
            foreach (XmlNode i in node.SelectNodes("item")) {
                SyndicationItem item = RssFormatter.Parese<SyndicationItem>(i);
                if (item.IsValid())
                    feed.items.Add(item);
            }
            return feed;
        }
    }
}
