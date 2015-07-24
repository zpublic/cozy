using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileEnumArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }

        public bool EnumSize { get; set; }

        public bool EnumTime { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (FilePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}