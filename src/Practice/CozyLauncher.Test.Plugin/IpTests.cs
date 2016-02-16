using NUnit.Framework;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Ip.Tests {

    [TestFixture()]
    public class IpTests {

        [Test()]
        public void IpQueryTest() {
            var main = new Main();
            var result = main.Query(new Query { RawQuery = "ip" });
            Assert.IsNotNull(result);
        }
    }
}