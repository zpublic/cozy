using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTester.Model
{
    public class FileInfo
    {
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public uint Size { get; set;  }
    }
}
