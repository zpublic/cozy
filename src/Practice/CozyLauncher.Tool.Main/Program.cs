using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Tool.Main
{
    static class Program
    {
        private const string UpdatePath = @"update";

        private static void CheckUpdateFile()
        {
            if (Directory.Exists(UpdatePath))
            {
                var files = Directory.GetFiles(UpdatePath);
                var filelist = files.Where(x => x.EndsWith(".cozy_update"));
                foreach (var file in filelist)
                {
                    File.Copy(file, Path.GetFileName(file.Replace(".cozy_update", "")), true);
                    File.Delete(file);
                }
            }
        }

        private static void ShellApp()
        {
            Process.Start("CozyLauncher.exe");
        }

        static void Main()
        {
            CheckUpdateFile();
            ShellApp();
        }
    }
}
