using CozyRSS.Syndication.Model;
using System.Xml;

namespace CozyRSS.Syndication.Parser
{
    public class Rss20ItemFormatter
    {
        public static SyndicationItem Parse(XmlNode node)
        {
            SyndicationItem item = new SyndicationItem();
            if (node?.Name == "item")
            {
                item.title = node.SelectSingleNode("title")?.InnerText;
                item.description = node.SelectSingleNode("description")?.InnerText;
                item.link = node.SelectSingleNode("link")?.InnerText;
                item.pubDate = node.SelectSingleNode("pubDate")?.InnerText;
            }
            return item;
        }
    }
}
