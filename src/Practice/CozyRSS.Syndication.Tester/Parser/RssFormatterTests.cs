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
        public void ParesetTest() {
            //item-item结构
            //var result = formatter.Formatter<SyndicationFeed>("rss/channel", new Uri("http://www.peise.net/rss.php?rssid=32"));


            //items>item结构(下载原来的xml手动修改成items>item结构做测试)
            var result = formatter.Formatter<SyndicationFeed>("rss/channel", File.ReadAllText("peise.xml"));
            Assert.IsTrue(result.items.Count > 0);
        }
    }
}