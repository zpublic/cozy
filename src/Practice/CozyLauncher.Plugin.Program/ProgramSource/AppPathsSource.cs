using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using Microsoft.Win32;
using System.IO;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public class AppPathsSource : ISource
    {
        public List<string> LoadProgram()
        {
            var list = new List<string>();
            ReadAppPaths(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", list);
            ReadAppPaths(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths", list);
            return list;
        }

        private void ReadAppPaths(string rootpath, List<string> list)
        {
            using (var root = Registry.LocalMachine.OpenSubKey(rootpath))
            {
                if (root == null) return;
                foreach (var item in root.GetSubKeyNames())
                {
                    try
                    {
                        using (var key = root.OpenSubKey(item))
                        {
                            string path = key.GetValue("") as string;
                            if (string.IsNullOrEmpty(path)) continue;

                            const int begin = 0;
                            int end = path.Length - 1;
                            const char quotationMark = '"';
                            if (path[begin] == quotationMark && path[end] == quotationMark)
                            {
                                path = path.Substring(begin + 1, path.Length - 2);
                            }

                            if (!File.Exists(path)) continue;
                            list.Add(path);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
