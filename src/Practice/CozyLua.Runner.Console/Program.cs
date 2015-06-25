using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Runner.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // 自动枚举 . 目录 和 ./plugin 下的dll，来加载插件
            // 自动执行 ./cozy.lua
            // cozy.lua 需包含一个main函数，返回int
        }
    }
}
