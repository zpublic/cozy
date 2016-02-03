using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var filelist = new List<string>
            {
                "CozyLauncher.exe",
                "CozyLauncher.Core.dll",
                "CozyLauncher.Infrastructure.dll",
                "CozyLauncher.PluginBase.dll",
                "NHotkey.dll",
                "NHotkey.Wpf.dll",
                "Newtonsoft.Json.dll",
                "YAMP.dll",
                "System.Windows.Interactivity.dll",
                "CozyLauncher.Plugin.Core.dll",
                "CozyLauncher.Plugin.Program.dll",
                "CozyLauncher.Plugin.Dirctory.dll",
                "CozyLauncher.Plugin.ManualRun.dll",
                "CozyLauncher.Plugin.WebSearch.dll",
                "CozyLauncher.Plugin.Sys.dll",
                "CozyLauncher.Plugin.Calculator.dll",
                "CozyLauncher.Plugin.MouseClick.dll",
            };
            Directory.CreateDirectory("./cozy_launcher");
            foreach (var f in filelist)
            {
                if (File.Exists("./cozy_launcher/" + f))
                {
                    File.Delete("./cozy_launcher/" + f);
                }
                File.Copy(f, "./cozy_launcher/" + f);
            }

            var updateFileList = new List<string>
            {
                "update/CozyLauncher.Tool.Update.exe",
                "update/CozyLauncher.Core.dll",
                "update/CozyLauncher.Infrastructure.dll",
                "update/CozyLauncher.PluginBase.dll",
                "update/NHotkey.dll",
                "update/NHotkey.Wpf.dll",
                "update/Newtonsoft.Json.dll",
            };
            Directory.CreateDirectory("./cozy_launcher/update");
            foreach (var f in updateFileList)
            {
                if (File.Exists("./cozy_launcher/" + f))
                {
                    File.Delete("./cozy_launcher/" + f);
                }
                File.Copy(f, "./cozy_launcher/" + f);
            }

            try
            {
                var gen = Path.Combine(Environment.CurrentDirectory, @"CozyLauncher.Tool.UpdateFeedGenerator.exe");
                var dir = Path.Combine(Environment.CurrentDirectory, @"cozy_launcher/");
                var dest = Path.Combine(Environment.CurrentDirectory, @"cozy_launcher/update/publish.json");
                Process.Start(gen, dir + " " + dest);
            }
            catch(Exception)
            {

            }
        }
    }
}
