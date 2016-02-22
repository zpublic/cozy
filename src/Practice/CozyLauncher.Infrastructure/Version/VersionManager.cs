using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CozyLauncher.Infrastructure.Version
{
    public class VersionManager
    {
        public static VersionManager Instance { get; set; } = new VersionManager();

        public bool IsExist { get; set; } = true;

        public string Version;

        public void Load(string str)
        {
            var info = JsonConvert.DeserializeObject<VersionInfo>(str);
            Version = info.Version;
        }

        public string Save()
        {
            var info = new VersionInfo() { Version = Version };
            return JsonConvert.SerializeObject(info);
        }

        public void LoadDefaullt()
        {
            Version = "0.5";
            IsExist = false;
        }
    }
}
