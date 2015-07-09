using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileGetLengthCommand : IPluginCommand
    {
        public uint Id
        {
            get { return CommandId.FileGetLengthCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string Path { get; set; }
    }
}