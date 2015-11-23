using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyApiPlayer.Core
{
    public class ApiPlayerEngine
    {
        private TaskLoader loader = new TaskLoader();
        private IResultOutput output = new ConsoleOutput();

        public bool Init(string taskfile)
        {
            return true;
        }

        public bool RunProject()
        {
            output.onResult("233");
            return true;
        }

        public bool RunModule(string moduleName)
        {
            return true;
        }

        public bool RunRequest(string moduleName, string requestName)
        {
            return true;
        }
    }
}
