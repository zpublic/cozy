using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileGetTimesCommand : IPluginCommand
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

        public ulong CreationTime { get; set; }

        public ulong LastAccessTime { get; set; }

        public ulong LastWriteTime { get; set; }
    }
}