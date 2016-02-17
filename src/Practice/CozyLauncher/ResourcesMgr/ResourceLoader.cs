using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.ResourcesMgr
{
    public class ResourceLoader
    {
        private readonly Predicate<string> canLoad;
        private readonly Func<string, object> load;

        public ResourceLoader(Predicate<string> canLoad, Func<string, object> load)
        {
            this.load = load;
            this.canLoad = canLoad;
        }

        public object Load(string value)
        {
            return this.load(value);
        }
        public bool CanLoad(string value)
        {
            return this.canLoad(value);
        }
    }
}
