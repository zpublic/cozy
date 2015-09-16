using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyServer.Plugin
{
    public partial class PluginManager
    {
        private List<IPlugin> PluginSet { get; set; } = new List<IPlugin>();
        private object objLocker = new object();

        public Predicate<string> PluginFilter { get; set; }

        public void NotifyData(object server, object msg)
        {
            foreach (var obj in PluginSet)
            {
                obj.DataCallback(server, msg);
            }
        }

        public void NotifyStatus(object server, object msg)
        {
            foreach (var obj in PluginSet)
            {
                obj.StatusCallback(server, msg);
            }
        }

        public bool LoadPlugins(string path)
        {
            bool result = (TryLoadPlugins(path) != 0);
            foreach (var obj in PluginSet)
            {
                obj.OnEnter();
            }
            return result;
        }
    }
}
