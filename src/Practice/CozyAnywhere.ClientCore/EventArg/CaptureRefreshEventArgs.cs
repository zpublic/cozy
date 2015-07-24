using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.ClientCore.EventArg
{
    public class CaptureRefreshEventArgs : EventArgs
    {
        public byte[] Data { get; set; }

        public CaptureRefreshEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}
