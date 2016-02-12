using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Core.StartUp
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
            var path = Assembly.GetEntryAssembly().Location;
            using (RegistryKey rk = Registry.CurrentUser)
            {
                using (RegistryKey run = rk.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    var names = run.GetValueNames();
                    return names.Contains("CozyLauncher") && (string)run.GetValue("CozyLauncher") == path;
                }
            }
        }

        private void SetAutoStartUpStatus(bool status)
        {
            var path = Assembly.GetEntryAssembly().Location;
            using (RegistryKey rk = Registry.CurrentUser)
            {
                using (RegistryKey run = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (status)
                    {
                        run.SetValue("CozyLauncher", path);
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
