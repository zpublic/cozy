using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CozyServer.Plugin
{
    public partial class PluginManager
    {
        public uint TryLoadPlugins(string path)
        {
            uint result = 0;
            if(Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach(var filename in files)
                {
                    if(TryAddPluginWithFilename(filename))
                    {
                        ++result;
                    }
                }
            }
            return result;
        }
    }
}
