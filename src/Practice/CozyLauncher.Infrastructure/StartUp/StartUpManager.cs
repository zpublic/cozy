using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.StartUp
{
    public class StartUpManager
    {
        public static StartUpManager Instance { get; private set; } = new StartUpManager();

        public bool IsAutoStartUp
        {
            get
            {
                return GetAutoStartUpStatus();
            }
            set
            {
                if(IsAutoStartUp != value)
                {
                    SetAutoStartUpStatus(value);
                }
            }
        }

        private bool GetAutoStartUpStatus()
        {
            using (RegistryKey rk = Registry.CurrentUser)
            {
                using (RegistryKey run = rk.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    var names = run.GetValueNames();
                    return names.Contains("CozyLauncher") && (string)run.GetValue("CozyLauncher") == Assembly.GetEntryAssembly().Location;
                }
            }
        }

        private void SetAutoStartUpStatus(bool status)
        {
            using (RegistryKey rk = Registry.CurrentUser)
            {
                using (RegistryKey run = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (status)
                    {
                        run.SetValue("CozyLauncher", Assembly.GetEntryAssembly().Location);
                    }
                    else
                    {
                        run.DeleteValue("CozyLauncher", false);
                    }
                }
            }
        }
    }
}
