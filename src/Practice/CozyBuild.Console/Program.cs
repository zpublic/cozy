using CozyBuild.Core;
using CozyBuild.Core.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBuild.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyBuildRunner runner = new CozyBuildRunner();
            runner.config.VisualStudio.SetPath(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0");

            LuaTask luaTask1 = new LuaTask() { luaScriptPath = "./t1.lua" };
            LuaTask luaTask2 = new LuaTask() { luaScriptPath = "./t2.lua" };
            NolmalTask task = new NolmalTask();
            task.AddTask(luaTask1);
            task.AddTask(luaTask2);
            runner.Run(task);
        }
    }
}
