using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileIsDirectoryArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }
    }
}