using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public Collection<Tuple<string, bool>> FileCollection { get; set; }

        public Collection<Tuple<uint, string>> ProcessCollection { get; set; }

        public List<string> PluginNameCollection { get; set; }
    }
}