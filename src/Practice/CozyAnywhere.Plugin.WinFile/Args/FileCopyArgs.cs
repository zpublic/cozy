using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile.Args
{
    public class FileCopyArgs : IPluginCommandMethodArgs
    {
        public string SourcePath { get; set; }

        public string DestPath { get; set; }

        public bool FailIfExists { get; set; }
    }
}