using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLua.Core;
using CozyAdventure.Engine.Util;

namespace CozyAdventure.Engine.Ext
{
    public static class LuaExt
    {
        public static void InitEnvironment(this CozyLuaCore lua)
        {
            lua.LoadCLRPackage();

            lua.DoFile(PathTransform.LuaScript("init"));
        }
    }
}
