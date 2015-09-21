using CozyLua.Core;
using CozyAdventure.Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CozyAdventure.Engine.Ext;

namespace CozyAdventure.Engine
{
    public enum ModuleTypeEnum : uint
    {
        Logic = 0,
        Data,
    }

    public abstract class ModuleBase
    {
        public abstract ModuleTypeEnum ModuleType { get; }

        protected Dictionary<string, CozyLuaFunction> mapMethod = new Dictionary<string, CozyLuaFunction>();

        private CozyLuaCore lua = new CozyLuaCore();

        public virtual bool Init(string script)
        {
            lua.InitEnvironment();

            lua.DoFile(PathTransform.LuaScript(script));

            var methods = lua.GetTable("methods");

            for(var em = methods.GetEnumerator(); em.MoveNext();)
            {
                var funcName = (string)em.Value;
                mapMethod.Add(funcName, lua.GetFunction(funcName));
            }
            return true;
        }

        public CozyLuaFunction GetTableFunction(string funcName)
        {
            if (mapMethod.ContainsKey(funcName))
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
