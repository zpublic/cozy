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
    public class CardCollectMethod_Lua : CardCollectMethod
    {
        private string script_;
        public CardCollectMethod_Lua(string script)
        {
            script_ = script;
        }

        public CardCollect Run()
        {
            CozyLuaCore lua = new CozyLuaCore();
            lua.LoadCLRPackage();
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Model')");
            lua.DoString("import ('CozyPoker.Engine', 'CozyPoker.Engine.Util')");
            lua.DoFile(PathTransform.LuaScript(script_));
            //lua.DoString(@" import ('CozyPoker.Engine', 'CozyPoker.Engine.Model')");
            //lua.DoString(@"
            //    cc = CardCollect()
            //    c = Card(1,1)
            //    cc:Add(c)");
            return (CardCollect)lua["cc"];
        }
    }
}
