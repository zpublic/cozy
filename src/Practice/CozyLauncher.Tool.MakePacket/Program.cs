using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Tool.MakePacket
{
    class Program
    {
        static void Main(string[] args)
        {
            KillCl();

            CozyMainFiles();
            CozyUpdateFiles();

            GenerateUpdateFeed();

            StartCl();
        }

        private static void GenerateUpdateFeed()
        {
            try
            {
                var gen = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"CozyLauncher.Tool.UpdateFeedGenerator.exe");
                var dir = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"cozy_launcher/");
                var dest = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"cozy_launcher/publish.json");
                Process.Start(gen, dir + " " + dest);
            }
            catch (Exception)
            {
            }
        }

        private static void CozyUpdateFiles()
        {
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
        }

        private static void CozyMainFiles()
        {
            var filelist = new List<string>
            {
                "CozyLauncher.exe",
                "CozyLauncher.Core.dll",
                "CozyLauncher.Infrastructure.dll",
                "CozyLauncher.PluginBase.dll",
                "CozyLauncher.CppPluginLoader.dll",

                "NHotkey.dll",
                "NHotkey.Wpf.dll",
                "Newtonsoft.Json.dll",
                "YAMP.dll",
                "Gma.QrCodeNet.Encoding.dll",
                "System.Windows.Interactivity.dll",
                "MaterialDesignColors.dll",
                "MaterialDesignThemes.MahApps.dll",
                "MaterialDesignThemes.Wpf.dll",
                "MahApps.Metro.dll",
                "SharpConfig.dll",
                "FluentScheduler.dll",

                "CozyLauncher.Plugin.Core.dll",
                "CozyLauncher.Plugin.Guide.dll",
                "CozyLauncher.Plugin.About.dll",
                "CozyLauncher.Plugin.Setting.dll",
                "CozyLauncher.Plugin.InfoCollect.dll",

                "CozyLauncher.Plugin.Program.dll",
                "CozyLauncher.Plugin.Dirctory.dll",
                "CozyLauncher.Plugin.ManualRun.dll",
                "CozyLauncher.Plugin.WebSearch.dll",
                "CozyLauncher.Plugin.Sys.dll",
                "CozyLauncher.Plugin.Calculator.dll",
                "CozyLauncher.Plugin.Ydfy.dll",
                "CozyLauncher.Plugin.Command.dll",

                "CozyLauncher.Plugin.MouseClick.dll",
                "CozyLauncher.Plugin.Ip.dll",
                "CozyLauncher.Plugin.Qrcode.dll",
                "CozyLauncher.Plugin.Fnl.dll",
                "CozyLauncher.Plugin.KickassTorrents.dll",
                "CozyLauncher.Plugin.Hardware.dll",
                "CozyLauncher.Plugin.Timestamp.dll",
                "CozyLauncher.Plugin.BreakReminder.dll",
                "CozyLauncher.Plugin.BrowserBookmark.dll",
                "CozyLauncher.Plugin.PasswordGenerator.dll",
                "CozyLauncher.Plugin.DailySentence.dll",
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
        }

        private static void StartCl()
        {
            var exePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                @"cozy_launcher/CozyLauncher.exe");
            Process.Start(exePath);
        }

        private static void KillCl()
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process item in ps)
            {
                if (item.ProcessName == "CozyLauncher")
                {
                    item.Kill();
                    item.WaitForExit();
                }
            }
        }
    }
}
