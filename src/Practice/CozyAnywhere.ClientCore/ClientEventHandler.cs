using CozyAnywhere.ClientCore.EventArg;
using System;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public event EventHandler<PluginChangedEvnetArgs> PluginChangedHandler;

        public event EventHandler<CaptureRefreshEventArgs> CaptureRefreshHandler;
    }
}