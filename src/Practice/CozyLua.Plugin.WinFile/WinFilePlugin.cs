using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Plugin.WinFile
{
    public class WinFilePlugin : CozyLuaPluginBase
    {
        public WinFilePlugin()
        {
            mMethods.Add("WinFile_IsExist", typeof(WinFilePluginImpl).GetMethod(
                    "IsExist",
                    new Type[]{ typeof(string) }));
        }
    }
}
