using CozyAnywhere.PluginBase;
using System;

namespace CozyAnywhere.Protocol.Commands
{
    public class FileEnumCommand : IPluginCommand
    {
        public delegate bool FileEnumFunc(IntPtr ptr, bool IsFolder);

        public uint Id
        {
            get { return CommandId.FileEnumCommand; }
        }

        public void Execute(BasePlugin plugin)
        {
            plugin.Shell(this);
        }

        public string Path { get; set; }

        public FileEnumFunc EnumFunc { get; set; }
    }
}