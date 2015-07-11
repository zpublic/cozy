using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileMoveArgs : PluginCommandMethodArgs
    {
        public string SourcePath { get; set; }

        public string DestPath { get; set; }

        public override string Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (FilePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}