using NUnit.Framework;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Ydfy.Tests {

    [TestFixture()]
    public class YdfyTests {

        [Test()]
        public void YdfyQueryTest() {
            var main = new Main();
            var result = main.Query(new Query { RawQuery = "ydfy hello" });
            Assert.IsNotNull(result);
        }
    }
}