using System;
using CozyAnywhere.Plugin.WinCapture;

namespace CozyAnywhere.ClientCore.EventArg
{
    public class CaptureRefreshEventArgs : EventArgs
    {
        public byte[] Data { get; set; }

        public Tuple<int, int, int, int> MetaData { get; set; }

        public CaptureRefreshEventArgs(Tuple<int, int, int, int> meta, byte[] data)
        {
            MetaData = meta;
            Data = data;
        }
    }
}
