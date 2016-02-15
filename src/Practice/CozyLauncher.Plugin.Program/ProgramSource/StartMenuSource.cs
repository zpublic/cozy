﻿using System;
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

        public List<string> LoadProgram()
        {
            var list = new List<string>();
            if (Directory.Exists(UserBasePath))
            {
                GetAppFromDirectory(UserBasePath, list, true);
            }
            if (Directory.Exists(CommBasePath))
            {
                GetAppFromDirectory(CommBasePath, list, true);
            }
            return list;
        }

        private void GetAppFromDirectory(string path, List<string> list, bool isRoot)
        {
            try
            {
                list.AddRange(Directory.GetFiles(path));
                list.AddRange(Directory.GetDirectories(path));

                if (isRoot)
                {
                    foreach (var subDirectory in Directory.GetDirectories(path))
                    {
                        GetAppFromDirectory(subDirectory, list, false);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
}
}
