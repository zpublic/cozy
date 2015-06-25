using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLua.Runner;
using CozyLua.Plugin.WinFile;

namespace CozyLua.Plugin.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyLuaRunner runner = new CozyLuaRunner();
            runner.AddPlugin(new WinFilePlugin());
            int n = runner.DoString(@"
                num = 0
                if (WinFile_IsExist('c:/asasa.ppp')) then
                    num = num+1
                end
                if (WinFile_IsExist('c:/Windows/regedit.exe')) then
                    num = num+1
                end
                if (WinFile_IsExist('c:/Windows/hh.exe')) then
                    num = num+1
                end
                return num");
            Console.WriteLine(n);
        }
    }
}
