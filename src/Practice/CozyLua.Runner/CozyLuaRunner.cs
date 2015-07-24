using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLua.Plugin;

namespace CozyLua.Runner
{
    public class CozyLuaRunner
    {
        private CozyLuaCore mLua = new CozyLuaCore();

        public void AddPlugin(ICozyLuaPlugin plugin)
        {
            var methods = plugin.GetEnumerator();
            while (methods.MoveNext())
            {
                mLua.RegisterFunction(methods.Current.Key, methods.Current.Value);
            }
        }

        public int DoString(string sLua)
        {
            return (int)(double)mLua.DoString(sLua)[0];
        }

        public int DoFile(string sLuaFile)
        {
            return (int)(double)mLua.DoFile(sLuaFile)[0];
        }
    }
}
