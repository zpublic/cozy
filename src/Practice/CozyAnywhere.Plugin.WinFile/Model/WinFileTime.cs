using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.Plugin.WinFile.Model
{
    public class WinFileTime
    {
        public ulong CreationTime { get; set; }

        public ulong LastAccessTime { get; set; }

        public ulong LastWriteTime { get; set; }
    }
}
