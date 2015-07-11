using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FilePathExistArgs : PluginCommandMethodArgs
    {
        public string Path { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (FilePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}