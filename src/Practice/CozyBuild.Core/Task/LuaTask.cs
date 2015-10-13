using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyBuild.Core.Config;

namespace CozyBuild.Core.Task
{
    public class LuaTask : ITask
    {
        public string luaScriptPath { get; set; }

        public bool Run(CozyBuildConfig config)
        {
            return true;
        }
    }
}
