using System;
using System.Collections.Generic;

namespace CozyAnywhere.ClientCore.EventArg
{
    public class PluginChangedEvnetArgs : EventArgs
    {
        public List<string> NewPlugins { get; set; }

        public PluginChangedEvnetArgs(List<string> collection)
        {
            NewPlugins = collection;
        }
    }
}