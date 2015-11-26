using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class DataReceivedEventArgs : EventArgsBase
    {
        public byte[] Data { get; set; }

        public DataReceivedEventArgs(string url, byte[] data)
            :base(url)
        {
            Data = data;
        }
    }
}
