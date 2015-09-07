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
    public class CalcCardsValueMethod_Lua : CalcCardsValueMethod
    {
        private string script_;
        public CalcCardsValueMethod_Lua(string script)
        {
            script_ = script;
        }

        public int Run(CardCollect cc)
        {
            CozyLuaCore lua = new CozyLuaCore();
            lua.LoadCLRPackage();
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Model')");
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Util')");
            lua.DoFile(PathTransform.LuaScript(script_));
            var f = lua.GetFunction("calc");
            return Convert.ToInt32(f.Call(cc).First());
        }
    }
}
