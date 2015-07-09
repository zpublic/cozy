using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileDeleteArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }
    }
}