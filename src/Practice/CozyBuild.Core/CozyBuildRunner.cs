using CozyBuild.Core.Config;
using CozyBuild.Core.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBuild.Core
{
    public class CozyBuildRunner
    {
        public CozyBuildConfig config { get; } = new CozyBuildConfig();

        public bool Run(ITask task)
        {
            return task.Run(config);
        }
    }
}
