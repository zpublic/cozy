using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileGetTimesArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }
    }
}