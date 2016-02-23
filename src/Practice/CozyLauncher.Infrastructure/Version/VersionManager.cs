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

        public string Version { get; set; }
    }
}
