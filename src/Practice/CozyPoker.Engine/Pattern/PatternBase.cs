using CozyLua.Core;
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
        Dictionary<string, string> mapMethod;

        public bool Init(string script)
        {
            CozyLuaCore lua = new CozyLuaCore();

            //lua.DoString(@"
            //    function ScriptFunc (val1, val2)
            //        if val1 > val2 then
            //            return val1 + 1
            //        else
            //            return val2 - 1
            //        end
            //    end");
            //var scriptFunc = lua.GetFunction("ScriptFunc");
            //var r3 = (double)scriptFunc.Call(3, 5).First();

            lua.DoString(@"
                methods = {
                    x=1,
                    y='hehe'
                }");
            var methods = lua.GetTable("methods");
            var em = methods.GetEnumerator();
            while (em.MoveNext())
            {
                Console.WriteLine(em.Key);
                Console.WriteLine(em.Value);
            }
            return true;
        }
    }
}
