using CozyBuild.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBuild.Core.Task
{
    public interface ITask
    {
        bool Run(CozyBuildConfig config);
    }
}
