using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Plugin.WinFile
{
    static class WinFilePluginImpl
    {
        public static bool IsExist(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            return false;
        }
    }
}
