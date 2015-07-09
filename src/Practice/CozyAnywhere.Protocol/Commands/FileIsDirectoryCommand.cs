using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileIsDirectoryCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FileIsDirectoryCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string Path { get; set; }
    }
}