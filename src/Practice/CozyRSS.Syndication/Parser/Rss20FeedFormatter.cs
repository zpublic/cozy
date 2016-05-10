using CozyRSS.Syndication.Model;
using System.Linq;

using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class Rss20FeedFormatter {

        public static SyndicationFeed Parse(XmlNode node) {

            var feed = RssFormatter.Parese<SyndicationFeed>(node);
            feed.items = node.SelectNodes("item")
                .Cast<XmlNode>()
                .Select(RssFormatter.Parese<SyndicationItem>)
                .Where(x => x.IsValid())
                .ToList();
            return feed;
        }
    }
}
