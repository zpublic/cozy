using System;

namespace CozyAnywhere.Plugin.WinFile.Model
{
    public class WinFileTimeModel
    {
        public DateTime CreationTime { get; set; }

        public DateTime LastAccessTime { get; set; }

        public DateTime LastWriteTime { get; set; }
    }
}