using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public interface ISource
    {
        List<Result> LoadProgram(Query query);
    }
}
