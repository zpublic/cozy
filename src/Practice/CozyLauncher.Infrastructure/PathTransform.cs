using System.IO;
using System.Reflection;

namespace CozyLauncher.Infrastructure
{
    public class PathTransform
    {
        public static string LocalFullPath(string file)
        {
            string cur = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(cur, file);
        }
    }
}
