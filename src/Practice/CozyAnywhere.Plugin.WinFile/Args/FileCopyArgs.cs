using CozyAnywhere.Protocol;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileCopyArgs : IPluginCommandMethodArgs
    {
        public string SourcePath { get; set; }

        public string DestPath { get; set; }

        public bool FailIfExists { get; set; }

        public PluginMethodReturnValueType Execute(IPluginCommandArgsDispatch dispatch)
        {
            var plugin = (FilePlugin)dispatch;
            return plugin.Shell(this);
        }
    }
}