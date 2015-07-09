using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FilePathExistArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }
    }
}