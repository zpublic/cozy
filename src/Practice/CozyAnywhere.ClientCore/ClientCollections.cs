using System;
using System.Collections.ObjectModel;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public Collection<Tuple<string, bool>> FileCollection { get; set; }

        public Collection<Tuple<uint, string>> ProcessCollection { get; set; }

        public Collection<string> PluginNameCollection { get; set; }
    }
}