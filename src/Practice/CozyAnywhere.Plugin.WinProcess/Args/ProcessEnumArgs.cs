using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinProcess.Args
{
    public class ProcessEnumArgs : PluginCommandMethodArgs
    {
        // No Args
        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (ProcessPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}