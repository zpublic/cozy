using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.Plugin.WinFile.Model
{
    public class WinFile
    {
        public string Name { get; set; }

        public bool IsFolder { get; set; }

        public uint Size { get; set; }

        public WinFileTime Tiles { get; set; }
    }
}
