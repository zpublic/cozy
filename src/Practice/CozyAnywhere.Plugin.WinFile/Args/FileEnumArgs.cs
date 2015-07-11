using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileEnumArgs : PluginCommandMethodArgs
    {
        public string Path { get; set; }

        public bool EnumSize { get; set; }

        public bool EnumTime { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (FilePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}