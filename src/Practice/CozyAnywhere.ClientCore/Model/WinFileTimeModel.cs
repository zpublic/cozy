using System;

namespace CozyAnywhere.ClientCore.Model
{
    public class WinFileTimeModel
    {
        public DateTime CreationTime { get; set; }

        public DateTime LastAccessTime { get; set; }

        public DateTime LastWriteTime { get; set; }
    }
}