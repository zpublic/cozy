using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyBuild.Core.Config;

namespace CozyBuild.Core.Task
{
    public class NolmalTask : ITask
    {
        private List<ITask> taskList = new List<ITask>();

        public void AddTask(ITask task)
        {
            taskList.Add(task);
        }

        public bool Run(CozyBuildConfig config)
        {
            foreach (var t in taskList)
            {
                if (!t.Run(config))
                    return false;
            }
            return true;
        }
    }
}
