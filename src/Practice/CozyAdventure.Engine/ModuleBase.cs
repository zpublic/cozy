using CozyLua.Core;
using CozyAdventure.Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine
{
    public abstract class ModuleBase
    {
        protected Dictionary<string, CozyLuaFunction> mapMethod = new Dictionary<string, CozyLuaFunction>();

        private CozyLuaCore lua = new CozyLuaCore();

        public virtual bool Init(string script)
        {
            lua.LoadCLRPackage();
            lua.DoString("import ('CozyAdventure.Engine', 'CozyPoker.CozyAdventure.Util')");
            lua.DoString("import ('CozyAdventure', 'CozyAdventure.Game.Model')");

            lua.DoFile(PathTransform.LuaScript(script));
            var methods = lua.GetTable("methods");

            for(var em = methods.GetEnumerator(); em.MoveNext();)
            {
                mapMethod.Add((string)em.Value, lua.GetFunction((string)em.Value));
            }
            return true;
        }

        public CozyLuaFunction GetTableFunction(string funcName)
        {
            if(mapMethod.ContainsKey(funcName))
            {
                return mapMethod[funcName];
            }
            return null;
        }

        public object[] CallFunc(string funcName, params object[] args)
        {
            var func = GetTableFunction(funcName);
            if (func != null)
            {
                return func.Call(args);
            }
            return null;
        }
    }
}
