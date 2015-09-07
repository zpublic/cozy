using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPoker.Engine.Model;
using CozyLua.Core;
using CozyPoker.Engine.Util;

namespace CozyPoker.Engine.Method
{
    public class ShuffleMethod_Lua : ShuffleMethod
    {
        private string script_;
        public ShuffleMethod_Lua(string script)
        {
            script_ = script;
        }

        public void Run(CardCollect cc)
        {
            CozyLuaCore lua = new CozyLuaCore();
            lua.LoadCLRPackage();
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Model')");
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Util')");
            lua.DoFile(PathTransform.LuaScript(script_));
            var f = lua.GetFunction("shuffle");
            f.Call(cc);
        }
    }
}
