using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinProcess.Args
{
    public class ProcessCreateArgs : PluginCommandMethodArgs
    {
        public string Path { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (ProcessPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}