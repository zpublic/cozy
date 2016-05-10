using System.Xml;
using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CozyRSS.Syndication.Tester.Parser {

    [TestClass()]
    public class Rss20FeedFormatterTests {

        [TestMethod()]
        public void ParseTest() {

            XmlReader reader = XmlReader.Create("http://www.peise.net/rss.php?rssid=32");
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            SyndicationFeed item = Rss20FeedFormatter.Parse(doc.SelectSingleNode("rss/channel"));
            Assert.IsTrue(item.items.Count > 0);
        }
    }
}