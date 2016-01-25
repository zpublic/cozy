using CozyLauncher.Core.Update;
using CozyLauncher.Infrastructure;
using System.Collections.Generic;
using System.IO;

namespace CozyLauncher.Tool.UpdateFeedGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileVersionInfo> filelist = new List<FileVersionInfo>();
            foreach (var file in Directory.GetFiles(args[1]))
            {
                filelist.Add(new FileVersionInfo
                {
                    Name = file,
                    Md5 = FileMd5.GetMD5HashFromFile(PathTransform.LocalFullPath(file)),
                });
            }
            // to json
        }
    }
}
