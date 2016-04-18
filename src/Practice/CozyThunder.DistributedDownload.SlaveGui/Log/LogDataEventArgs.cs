using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.SlaveGui.Log
{
    public class LogDataEventArgs : EventArgs
    {
        public string Data { get; set; }

        public LogDataEventArgs(string data)
        {
            Data = data;
        }
    }
}
