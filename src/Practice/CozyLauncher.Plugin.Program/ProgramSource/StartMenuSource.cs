using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.IO;
using System.Runtime.InteropServices;

namespace CozyLauncher.Plugin.Program.ProgramSource
{
    public class StartMenuSource : ISource
    {
        const int CSIDL_COMMON_PROGRAMS = 0x17;
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        private static string GetPath()
        {
            var commonStartMenuPath = new StringBuilder(560);
            SHGetSpecialFolderPath(IntPtr.Zero, commonStartMenuPath, CSIDL_COMMON_PROGRAMS, false);

            return commonStartMenuPath.ToString();
        }

        private readonly string UserBasePath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        private readonly string CommBasePath = GetPath();

        public List<Result> LoadProgram(Query query)
        {
            var list = new List<Result>();
            if (Directory.Exists(UserBasePath))
            {
                GetAppFromDirectory(UserBasePath, list, query, true);
            }
            if (Directory.Exists(CommBasePath))
            {
                GetAppFromDirectory(CommBasePath, list, query, true);
            }
            return list;
        }

        private void GetAppFromDirectory(string path, List<Result> list, Query query, bool isRoot)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    var filename = Path.GetFileNameWithoutExtension(file).ToLower();
                    if (filename == query.RawQuery)
                    {
                        var p = new Result()
                        {
                            IcoPath = "app",
                            Title = filename,
                            SubTitle = file,
                            Score = 100,
                        };

                        list.Add(p);
                    }
                }

                foreach (var subDirectory in Directory.GetDirectories(path))
                {
                    var fi = new DirectoryInfo(subDirectory);

                    var pathname = fi.Name.ToLower();
                    if (pathname == query.RawQuery)
                    {
                        var p = new Result()
                        {
                            IcoPath = "folder_open",
                            Title = pathname,
                            SubTitle = subDirectory,
                            Score = 100,
                        };

                        list.Add(p);
                    }

                    if (isRoot)
                    {
                        GetAppFromDirectory(subDirectory, list, query, false);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
