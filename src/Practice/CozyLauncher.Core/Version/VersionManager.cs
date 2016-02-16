using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Core.Version
{
    public class VersionManager
    {
        public static VersionManager Instance { get; set; } = new VersionManager();

        public bool IsExist
        {
            get
            {
                return string.IsNullOrEmpty(ReadInfo());
            }
        }

        public string Version
        {
            get
            {
                return ReadInfo();
            }
            set
            {
                WriteInfo(value);
            }
        }

        private string ReadInfo()
        {
            using (var rk = Registry.CurrentUser)
            {
                using (var exist = rk.OpenSubKey(@"Software\Cozy\CozyLauncher"))
                {
                    if(exist != null)
                    {
                        return exist.GetValue("version") as string;
                    }
                }
            }
            return null;
        }

        private void WriteInfo(string version)
        {
            using (var rk = Registry.CurrentUser)
            {
                using (var exist = rk.CreateSubKey(@"Software\Cozy\CozyLauncher"))
                {
                    exist.SetValue("version", version);
                }
            }
        }

        private void DeleteInfo()
        {
            using (var rk = Registry.CurrentUser)
            {
                using (var exist = rk.CreateSubKey(@"Software\Cozy\CozyLauncher"))
                {
                    rk.DeleteValue("version");
                }
            }
        }
    }
}
