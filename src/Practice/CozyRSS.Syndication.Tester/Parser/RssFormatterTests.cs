using System;
using System.IO;
using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CozyRSS.Syndication.Tester.Parser {

    [TestClass()]
    public class RssFormatterTests {

        private RssFormatter formatter;

        [TestInitialize]
        public void Init() {
            formatter = new RssFormatter();
        }

        [TestMethod()]
        public void ParseRssTest()
        {
            var result = formatter.Formatter("http://www.peise.net/rss.php?rssid=32");
            Assert.IsTrue(result.items.Count > 0);
        }

        [TestMethod()]
        public void ParseAtomTest() {
            var result = formatter.Formatter("http://feed.cnblogs.com/blog/u/132703/rss");
            Assert.IsTrue(result.items.Count > 0);
        }
    }
}