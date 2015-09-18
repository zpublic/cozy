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
    public abstract class ModuleBase
    {
        protected Dictionary<string, CozyLuaFunction> mapMethod = new Dictionary<string, CozyLuaFunction>();

        private CozyLuaCore lua = new CozyLuaCore();

        private string ModuleName { get; set; }

        public virtual bool Init(string script)
        {
            ModuleName = this.GetType().Name;

            lua.InitEnvironment();
            lua.InitPath();

            lua.DoFile(PathTransform.LuaScript(script));

            var methods = lua.GetTable("methods");

            for(var em = methods.GetEnumerator(); em.MoveNext();)
            {
                var funcName = ModuleName + '.' + (string)em.Value;
                mapMethod.Add(funcName, lua.GetFunction(funcName));
            }
            return true;
        }

        public CozyLuaFunction GetTableFunction(string funcName)
        {
            var readName = ModuleName + '.' + funcName;
            if (mapMethod.ContainsKey(readName))
            {
                return mapMethod[readName];
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
