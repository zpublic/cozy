using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileDeleteCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FileDeleteCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string Path { get; set; }
    }
}