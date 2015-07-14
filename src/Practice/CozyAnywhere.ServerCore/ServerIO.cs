using System.Collections.Generic;
using System.IO;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public List<string> EnumPluginFolder()
        {
            List<string> result = new List<string>();
            string path = @"./Plugins/";
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                foreach (FileInfo file in TheFolder.GetFiles())
                {
                    if (file.Name.EndsWith(".dll"))
                        result.Add(file.Name);
                }
            }
            return result;
        }
    }
}