using System.Collections.Generic;
using System.IO;

namespace PluginHelper
{
    public static class PluginFileHelper
    {
        public static List<string> EnumPluginFolder(string path)
        {
            List<string> result = new List<string>();
           
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                foreach (FileInfo file in TheFolder.GetFiles())
                {
                    result.Add(file.Name);
                }
            }
            return result;
        }
    }
}
