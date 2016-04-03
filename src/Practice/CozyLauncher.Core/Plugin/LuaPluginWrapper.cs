using CozyLauncher.PluginBase;
using System.Collections.Generic;

namespace CozyLauncher.Core.Plugin
{
    public class LuaPluginWrapper : IPlugin
    {
        public bool Load(string luafile)
        {
            return false;
        }

        public PluginInfo Init(PluginInitContext context)
        {
            return null;
        }

        public List<Result> Query(Query query)
        {
            return null;
        }

        public void ShowPanel(string command)
        {
        }

        public void RunCommand(string command)
        {
        }
    }
}
