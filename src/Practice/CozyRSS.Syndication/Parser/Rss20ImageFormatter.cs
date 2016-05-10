using CozyRSS.Syndication.Model;
using System.Xml;

namespace CozyRSS.Syndication.Parser
{
    public class Rss20ImageFormatter
    {
        public static SyndicationImage Parse(XmlNode node)
        {
            SyndicationImage image = new SyndicationImage();
            if (node?.Name == "image")
            {
                image.url = node.SelectSingleNode("url")?.InnerText;
                image.title = node.SelectSingleNode("title")?.InnerText;
                image.link = node.SelectSingleNode("link")?.InnerText;
            }
            return image;
        }
    }
}
