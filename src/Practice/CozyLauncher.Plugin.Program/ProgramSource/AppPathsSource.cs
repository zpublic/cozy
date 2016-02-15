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
        public List<Result> LoadProgram(Query query)
        {
            var list = new List<Result>();
            ReadAppPaths(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", list, query);
            ReadAppPaths(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths", list, query);
            return list;
        }

        private void ReadAppPaths(string rootpath, List<Result> list, Query query)
        {
            using (var root = Registry.LocalMachine.OpenSubKey(rootpath))
            {
                if (root == null) return;
                foreach (var item in root.GetSubKeyNames())
                {
                    if(Path.GetFileNameWithoutExtension(item).ToLower() == query.RawQuery)
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
                                list.Add(new Result()
                                {
                                    Title       = Path.GetFileNameWithoutExtension(item),
                                    SubTitle    = path,
                                    IcoPath     = "app",
                                    Score       = 100,
                                });
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
}
