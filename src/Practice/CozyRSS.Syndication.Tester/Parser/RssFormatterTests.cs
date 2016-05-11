using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CozyRSS.Syndication.Tester.Parser {

    [TestClass()]
    public class RssFormatterTests {

        private RssFormatter formatter;

        [TestInitialize]
        public void Init() {
            formatter = new RssFormatter("rss/channel", "http://www.peise.net/rss.php?rssid=32");
        }

        [TestMethod()]
        public void PareseTestTest() {
            var result = formatter.Parese<SyndicationFeed>();
            Assert.Fail();
        }
    }
}