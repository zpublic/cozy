using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Tool.MakePacket
{
    class Program
    {
        static void Main(string[] args)
        {
            var filelist = new List<string> {
                "CozyLauncher.exe",
                "CozyLauncher.Core.dll",
                "CozyLauncher.Infrastructure.dll",
                "CozyLauncher.PluginBase.dll",

                "NHotkey.dll",
                "NHotkey.Wpf.dll",
                "System.Windows.Interactivity.dll",

                "CozyLauncher.Plugin.Core.dll",

                "CozyLauncher.Plugin.Program.dll",
                "CozyLauncher.Plugin.Dirctory.dll",
                "CozyLauncher.Plugin.ManualRun.dll",
                "CozyLauncher.Plugin.WebSearch.dll",

                "CozyLauncher.Plugin.MouseClick.dll",
            };

            Directory.CreateDirectory("./cozy_launcher");
            foreach (var f in filelist)
            {
                File.Replace(f, "./cozy_launcher/" + f, null);
            }
        }
    }
}
