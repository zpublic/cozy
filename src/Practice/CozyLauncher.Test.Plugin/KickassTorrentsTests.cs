using NUnit.Framework;

namespace CozyLauncher.Plugin.KickassTorrents.Tests {

    [TestFixture()]
    public class KickassTorrentsTests {

        [Test()]
        public void KickassTorrentsQueryTest() {
            var main = new Main();
            var result = main.Query(new PluginBase.Query { RawQuery = "t javhd" });
            Assert.IsNotEmpty(result);
        }
    }
}