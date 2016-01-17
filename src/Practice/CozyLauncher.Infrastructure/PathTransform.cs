using System.IO;

namespace CozyLauncher.Infrastructure
{
    public class PathTransform
    {
        public static string LocalFullPath(string file)
        {
            string cur = System.Environment.CurrentDirectory;
            return Path.Combine(cur, file);
        }
    }
}
