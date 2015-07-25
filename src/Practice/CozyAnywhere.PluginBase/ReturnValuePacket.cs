using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.PluginBase
{
    public class ReturnValuePacket
    {
        public string MetaData { get; set; }
        public byte[] Data { get; set; }
    }
}
