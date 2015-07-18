using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinProcess.Args
{
    public class ProcessTerminateWithTimeOutArgs : IPluginCommandMethodArgs
    {
        public uint Pid { get; set; }

        public uint TimeOut { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (ProcessPlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}