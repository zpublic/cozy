using NUnit.Framework;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Fnl.Tests {

    [TestFixture()]
    public class FnlTests {

        [Test()]
        public void FnlQueryTest() {
            var main = new Main();
            var result = main.Query(new Query { RawQuery = "fnl" });
            Assert.IsNotNull(result);
        }
    }
}