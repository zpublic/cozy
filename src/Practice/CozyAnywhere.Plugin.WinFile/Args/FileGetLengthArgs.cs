using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileGetLengthArgs : IPluginCommandMethodArgs
    {
        public string Path { get; set; }
    }
}