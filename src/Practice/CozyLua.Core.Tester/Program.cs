using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLua.Core;

namespace CozyLua.Core.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyLuaCore lua = new CozyLuaCore();

            var r = (double)lua.DoString("return 10 + 3*(5 + 2)")[0];
            Console.WriteLine(r);

            lua["x"] = 12.0;
            Console.WriteLine(lua["x"]);

            var r2 = (double)lua.DoString("return 10 + x*(5 + 2)")[0];
            Console.WriteLine(r2);

            lua.DoString(@"
                function ScriptFunc (val1, val2)
                    if val1 > val2 then
                        return val1 + 1
                    else
                        return val2 - 1
                    end
                end");

            var scriptFunc = lua.GetFunction("ScriptFunc");
            var r3 = (double)scriptFunc.Call(3, 5).First();
            Console.WriteLine(r3);

            lua.DoString(@"
                tablex = {
                    x=1,
                    y='hehe'
                }");
            var tablex = lua.GetTable("tablex");
            foreach (var i1 in tablex.Keys)
            {
                Console.WriteLine(i1);
            }
            var i2 = tablex.GetEnumerator();
            while (i2.MoveNext())
            {
                Console.WriteLine(i2.Value);
            }
        }
    }
}
