using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public class PathSource : ISource
    {
        public List<string> LoadProgram()
        {
            var res = new List<string>();
            var EnvVar = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach (var path in EnvVar)
            {
                if(Directory.Exists(path))
                    res.AddRange(Directory.GetFiles(path));
            }
            return res;
        }
    }
}
