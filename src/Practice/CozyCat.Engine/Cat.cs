using CozyCat.Plugin.Mouse;
using CozyLua.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCat.Engine
{
    public class Cat
    {
        public static void test()
        {
            CozyLuaRunner runner = new CozyLuaRunner();
            runner.AddPlugin(new MousePlugin());
            int n = runner.DoString(@"
                num = 0
                if (Mouse_IsExist('c:/asasa.ppp')) then
                    num = num+1
                end
                if (Mouse_IsExist('c:/Windows/regedit.exe')) then
                    num = num+1
                end
                if (Mouse_IsExist('c:/Windows/hh.exe')) then
                    num = num+1
                end
                return num");
            Console.WriteLine(n);
        }
    }
}
