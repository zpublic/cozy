using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileCopyCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FileCopyCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string SourcePath { get; set; }

        public string DestPath { get; set; }

        public bool FailIfExists { get; set; }
    }
}