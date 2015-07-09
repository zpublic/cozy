using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileMoveCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FileMoveCommand; }
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