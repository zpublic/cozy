using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public interface ISource
    {
        List<string> LoadProgram();
    }
}
