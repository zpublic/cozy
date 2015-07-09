using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FilePathExistCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FilePathExistCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string Path { get; set; }
    }
}