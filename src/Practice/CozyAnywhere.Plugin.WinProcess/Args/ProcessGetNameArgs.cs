using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinProcess.Args
{
    public class ProcessGetNameArgs : PluginCommandMethodArgs
    {
        public uint Pid { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (ProcessPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}