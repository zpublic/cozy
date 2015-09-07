using CozyLua.Core;
using CozyPoker.Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Pattern
{
    public class PatternBase
    {
        // 方法策略表
        protected Dictionary<string, string> mapMethod;

        virtual public bool Init(string script)
        {
            CozyLuaCore lua = new CozyLuaCore();
            lua.DoFile(PathTransform.LuaScript(script));
            mapMethod = new Dictionary<string, string>();
            var methods = lua.GetTable("methods");
            var em = methods.GetEnumerator();
            while (em.MoveNext())
            {
                mapMethod.Add((string)em.Key, (string)em.Value);
            }
            return true;
        }
    }
}
