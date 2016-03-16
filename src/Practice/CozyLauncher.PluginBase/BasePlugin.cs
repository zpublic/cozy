using System;
using System.Collections.Generic;

namespace CozyLauncher.PluginBase
{
    public abstract class BasePlugin : IPlugin
    {
        public virtual PluginInfo Init(PluginInitContext context)
        {
            throw new NotImplementedException();
        }

        public virtual List<Result> Query(Query query)
        {
            return null;
        }

        public virtual void ShowPanel(string command)
        {
        }

        public virtual void RunCommand(string command)
        {
        }
    }
}
